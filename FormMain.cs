using Ollamaclient.SQLiteDatabase;
using OllamaSharp;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using WindowsInput.Events;
using WindowsInput.Events.Sources;
using WindowsInput.Native;
using static Ollamaclient.SQLiteDatabase.Tables;

namespace Ollamaclient
{
    public partial class FormMain : Form
    {
        // private IKeyboardEventSource m_Keyboard;

        private HttpListener? _listener;

        private PresetRec UsePreset = null;

        private SQLiteFunctions DBFunct = SQLiteFunctions.Instance;

        private Dictionary<WindowsInput.Events.KeyCode, int> keyMap;

        public static string DatabasePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "OllamaClient\\");

        //var uri = new Uri("http://localhost:11434");
        private OllamaApiClient ollama;

        private bool ALT_Pressed;
        private bool PresetInProgress;
        private IKeyboardEventSource Keyboard;

        private bool keyBussy;

        public FormMain()
        {
            InitializeComponent();
            // https://github.com/MediatedCommunications/WindowsInput
            // https://github.com/awaescher/OllamaSharp
            InitializeAsync();

            LoadRagIndexes();

            keyMap = new Dictionary<WindowsInput.Events.KeyCode, int>
            {
                {WindowsInput.Events.KeyCode.D1, 0},
                {WindowsInput.Events.KeyCode.D2, 1},
                {WindowsInput.Events.KeyCode.D3, 2},
                {WindowsInput.Events.KeyCode.D4, 3},
                {WindowsInput.Events.KeyCode.D5, 4},
                {WindowsInput.Events.KeyCode.D6, 5},
                {WindowsInput.Events.KeyCode.D7, 6},
                {WindowsInput.Events.KeyCode.D8, 7},
                {WindowsInput.Events.KeyCode.D9, 8}
            };

            cbALT.DisplayMember = "Keys";
            cbALT.ValueMember = "Id";

            Keyboard = default(IKeyboardEventSource);

            Keyboard = WindowsInput.Capture.Global.Keyboard();
            Keyboard.KeyEvent += this.Keyboard_KeyEvent;

            textBoxLog.Text += "Databasedir: " + DatabasePath + "\r\n";
        }

        private void LoadRagIndexes()
        {
            // Zorg dat de map bestaat
            Directory.CreateDirectory(DatabasePath + "rag_indexs");

            // Leeg eerst de combobox
            cbRag_index.Items.Clear();

            // Voeg "None" toe als eerste optie
            cbRag_index.Items.Add("None");

            // Lees alle subfolders
            string[] folders = Directory.GetDirectories(DatabasePath + "rag_indexs");

            foreach (string folder in folders)
            {
                // Haal alleen de foldernaam eruit, niet het hele pad
                string folderName = Path.GetFileName(folder);
                cbRag_index.Items.Add(folderName);
            }

            // Standaard selecteren op "None"
            cbRag_index.SelectedIndex = 0;
        }

        private async Task InitOllama()
        {
            var uri = new Uri(tbOllamaURL.Text);
            try
            {
                ollama = new OllamaApiClient(uri);
                var localModels = await ollama.ListLocalModels();
                cbModel.Items.Clear();
                foreach (var model in localModels.ToList())
                {
                    cbModel.Items.Add(model.Name);
                }
            }
            catch (Exception ex)
            {
                textBoxLog.Text += "Error connecting to Ollama server with message \"" + ex.Message + Environment.NewLine + "\"\r\n";
                return;
            }

            textBoxLog.Text += "Connected to Ollama server\r\n";
        }

        private async Task InitializeAsync()
        {
            tbOllamaURL.Text = (await DBFunct.SettingGet("OllamaURL", new Tables.SettingRec { ValueString = "http://localhost:11434" }))?.ValueString ?? string.Empty;
            InitOllama();

            for (int i = 1; i <= 9; i++)
            {
                var presetRec = await DBFunct.PresetRecGet(i);
                if (presetRec == null)
                {
                    presetRec = new PresetRec();
                    // some defaults when starting the first time
                    switch (i)
                    {
                        case 1:
                            presetRec.Id = i;
                            presetRec.Modal = "mistral:latest";
                            presetRec.Prompt = "Fix all typos and casing and punctuation in this text, but preserve all new line characters:\r\n\"{input}\"\r\nReturn only the corrected text, don't include a preamble";
                            presetRec.Keys = "ALT + " + i.ToString();
                            await DBFunct.PresetRecAddUpdate(presetRec);
                            break;

                        case 2:
                            presetRec.Id = i;
                            presetRec.Modal = "codegemma:latest";
                            presetRec.Prompt = "Fix all typos and casing and punctuation in this text in dutch, but preserve all new line characters:\r\n\"{input}\"\r\nReturn only the corrected text, don't include a preamble";
                            presetRec.Keys = "ALT + " + i.ToString();
                            await DBFunct.PresetRecAddUpdate(presetRec);
                            break;

                        case 3:
                            presetRec.Id = i;
                            presetRec.Modal = "codegemma:latest";
                            presetRec.Prompt = "Optimize and fix if needed the code folowing code \"{input}\"\r\ndon't include a preamble and give only the code as plain text";
                            presetRec.Keys = "ALT + " + i.ToString();
                            await DBFunct.PresetRecAddUpdate(presetRec);
                            break;

                        default:
                            break;
                    }
                }
                presetRec.Keys = "ALT + " + i.ToString();
                cbALT.Items.Add(presetRec);
            }
            cbALT.SelectedIndex = 0;
        }

        private async void Keyboard_KeyEvent(object sender, EventSourceEventArgs<KeyboardEvent> e)
        {
            if (e.Data?.KeyDown?.Key == WindowsInput.Events.KeyCode.LAlt) ALT_Pressed = true;
            if (e.Data?.KeyUp?.Key == WindowsInput.Events.KeyCode.LAlt) ALT_Pressed = false;

            if (keyBussy) return;

            if (ALT_Pressed)
            {
                if (e.Data?.KeyDown?.Key != null && keyMap.ContainsKey((KeyCode)(e.Data?.KeyDown?.Key)))
                {
                    var index = keyMap[e.Data.KeyDown.Key];
                    textBoxLog.Text += $"ALT+{index + 1}\r\n";
                    UsePreset = (PresetRec)cbALT.Items[index];
                }
            }

            if (UsePreset != null && ALT_Pressed == false)
            {
                keyBussy = true;
                Thread.Sleep(600);
                await WindowsInput.Simulate.Events()
                .ClickChord(KeyCode.LControl, KeyCode.C).Wait(50)
                .Invoke();
                String clipboard = Clipboard.GetText();
                if (clipboard != null && clipboard != "")
                {
                    string ResultOllama = "";

                    if (UsePreset.Rag_index != "None" && UsePreset.Rag_index != "")
                    {
                        //Clipboard.GetText() bevat de vraag,
                        //DatabasePath + "rag_indexs\\EasyFactuur" is het pad van de RAG
                        //DatabasePath + "rag_ask.py" is het pad van de python script die de RAG index gebruikt
                        //UsePreset is een class van
                        // public class PresetRec
                        //{
                        //    [PrimaryKey]
                        //    public long Id { get; set; }

                        //    public string Modal { get; set; }
                        //    public string Rag_index { get; set; }
                        //    public string Prompt { get; set; }

                        //    public string Keys { get; set; }
                        //    public bool Result { get; set; }
                        //}

                        ResultOllama = await RagHelper.AskPython(Clipboard.GetText(), UsePreset, DatabasePath + "rag_indexs\\EasyFActuur", DatabasePath + "rag_ask.py");
                    }
                    else
                    {
                        ResultOllama = await AskOllama(Clipboard.GetText(), UsePreset);
                    }

                    if (ResultOllama != "")
                    {
                        if (UsePreset.Result)
                        {
                            textBoxLog.Text += "Result: " + "\r\n" + RagHelper.NormalizeNewLines(ResultOllama) + "\r\n";
                        }
                        else
                        {
                            Clipboard.SetText(ResultOllama);
                            Thread.Sleep(100);
                            await WindowsInput.Simulate.Events()
                            .ClickChord(KeyCode.LControl, KeyCode.V).Wait(50)
                            .Invoke();
                        }
                    }
                }
                UsePreset = null;
                keyBussy = false;
            }
            Log(e);
        }

        private async Task<string> AskOllama(string TheInput, PresetRec presetRec)
        {
            ollama.SelectedModel = presetRec.Modal;

            string ThePrompt = presetRec.Prompt.Replace("{input}", TheInput);

            StringBuilder responseBuilder = new StringBuilder();
            ConversationContext context = null;

            var OllamaResult = await ollama.StreamCompletion(ThePrompt, context, stream =>
            {
                responseBuilder.Append(stream.Response);
            });

            return RemoveFirstAndLastQuotes(responseBuilder.ToString().Trim());
        }

        private void Log<T>(EventSourceEventArgs<T> e, string Notes = "") where T : InputEvent
        {
            if (cbLog.Checked == false) return;
            var NewContent = "";
            NewContent += $"{e.Timestamp}: {Notes}\r\n";
            foreach (var item in e.Data.Events)
            {
                NewContent += $"  {item}\r\n";
            }
            NewContent += "\r\n";

            textBoxLog.Text = NewContent + textBoxLog.Text;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Keyboard?.Dispose();
            Keyboard = null;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            PresetInProgress = true;
            PresetRec presetRec = (PresetRec)cbALT.SelectedItem;
            presetRec.Id = cbALT.SelectedIndex + 1;
            presetRec.Modal = cbModel.Text;
            presetRec.Rag_index = cbRag_index.Text;
            presetRec.Prompt = tbPrompt.Text;
            presetRec.Result = cbResult.Checked;
            cbALT.Items[cbALT.SelectedIndex] = presetRec;
            await DBFunct.PresetRecAddUpdate(presetRec);
            PresetInProgress = false;
        }

        private async void cbALT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PresetInProgress == true) return;

            cbModel.Text = "";
            cbRag_index.Text = "";
            cbResult.Checked = false;
            tbPrompt.Text = "";
            var presetRec = await DBFunct.PresetRecGet(cbALT.SelectedIndex + 1);

            if (presetRec != null)
            {
                cbModel.Text = presetRec.Modal;
                cbRag_index.Text = presetRec.Rag_index;
                tbPrompt.Text = presetRec.Prompt;
                cbResult.Checked = presetRec.Result;
            }
        }

        private async void btnSaveURL_Click(object sender, EventArgs e)
        {
            _ = await DBFunct.SettingSet(new SettingRec { Name = "OllamaURL", ValueString = tbOllamaURL.Text });
            await InitOllama();
        }

        private string RemoveFirstAndLastQuotes(string input)
        {
            string TheReturn = input;

            TheReturn = TheReturn.Replace("```csharp", "");
            TheReturn = TheReturn.Replace("```", "");

            if (TheReturn.Length >= 2 && TheReturn[0] == '"' && TheReturn[^1] == '"')
            {
                // Remove leading and trailing quotes
                TheReturn = TheReturn[1..^1];
            }
            return TheReturn.Trim();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            StartHttpServer(80); // start op poort 8085
            //textBoxLog.Font = new Font("Courier New", 10);
            textBoxLog.Font = new Font("Consolas", 10, FontStyle.Regular);
        }

        private async void StartHttpServer(int port)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://+:{port}/ask/");
            // HIER voeg je de prefix toe:
            //  _listener.Prefixes.Add("http://+:8085/");   // of "http://localhost:8085/" voor alleen lokaal

            _listener.Start();
            try
            {
                if (textBoxLog != null && !textBoxLog.IsDisposed)
                {
                    textBoxLog.AppendText($"[INFO] Luistert op http://:{port}/ask{Environment.NewLine}");
                }
            }
            catch (Exception)
            {

                // throw;
            }


            while (_listener.IsListening)
            {
                try
                {
                    var ctx = await _listener.GetContextAsync();
                    _ = Task.Run(() => HandleRequest(ctx));
                }
                catch (Exception ex)
                {
                    if (textBoxLog != null && !textBoxLog.IsDisposed)
                    {
                        textBoxLog.AppendText($"[ERR] {ex.Message}{Environment.NewLine}");
                    }
                }
            }
        }

        private async Task HandleRequest(HttpListenerContext ctx)
        {
            // ===== CORS headers altijd meesturen =====
            // Zet hier liever je eigen domein i.p.v. "*" als je wilt beperken: "https://jouwdomein.nl"
            ctx.Response.Headers["Access-Control-Allow-Origin"] = "*";
            ctx.Response.Headers["Access-Control-Allow-Methods"] = "POST, OPTIONS, GET";
            ctx.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type, X-Api-Key";

            string path = ctx.Request.Url.AbsolutePath ?? "/";
            bool isAskPath = path.Equals("/ask", StringComparison.OrdinalIgnoreCase)
                          || path.Equals("/ask/", StringComparison.OrdinalIgnoreCase);

            try
            {
                // --- Preflight (CORS) ---
                if (ctx.Request.HttpMethod == "OPTIONS")
                {
                    ctx.Response.StatusCode = 204; // No Content
                    ctx.Response.OutputStream.Close();
                    return;
                }

                // --- Healthcheck (handig om in browser te testen) ---
                if (ctx.Request.HttpMethod == "GET" && path.Equals("/health", StringComparison.OrdinalIgnoreCase))
                {
                    var payload = Encoding.UTF8.GetBytes("{\"status\":\"ok\"}");
                    ctx.Response.ContentType = "application/json; charset=utf-8";
                    ctx.Response.ContentLength64 = payload.Length;
                    await ctx.Response.OutputStream.WriteAsync(payload, 0, payload.Length);
                    ctx.Response.OutputStream.Close();
                    return;
                }
                var apiKey = "";
                // --- De echte endpoint ---
                if (ctx.Request.HttpMethod == "POST" && isAskPath)
                {
                    // (optioneel) simpele API-key check
                    apiKey = ctx.Request.Headers["X-Api-Key"];
                    // if (apiKey != "GEHEIME_KEY") { ctx.Response.StatusCode = 401; ... }

                    // Content-Type sanity
                    var ct = ctx.Request.ContentType ?? "";
                    if (!ct.Contains("application/json", StringComparison.OrdinalIgnoreCase))
                    {
                        ctx.Response.StatusCode = 415; // Unsupported Media Type
                        var badCt = Encoding.UTF8.GetBytes("{\"error\":\"Content-Type moet application/json zijn\"}");
                        ctx.Response.ContentType = "application/json; charset=utf-8";
                        ctx.Response.ContentLength64 = badCt.Length;
                        await ctx.Response.OutputStream.WriteAsync(badCt, 0, badCt.Length);
                        ctx.Response.OutputStream.Close();
                        return;
                    }

                    // Body lezen
                    string body;
                    using (var reader = new StreamReader(ctx.Request.InputStream, ctx.Request.ContentEncoding))
                        body = await reader.ReadToEndAsync();

                    // JSON parse
                    string question = "";
                    string api_key = "";
                    try
                    {
                        using var doc = JsonDocument.Parse(body);
                        question = doc.RootElement.GetProperty("question").GetString() ?? "";
                        question = doc.RootElement.GetProperty("question").GetString() ?? "";
                    }
                    catch
                    {
                        ctx.Response.StatusCode = 400;
                        var badJson = Encoding.UTF8.GetBytes("{\"error\":\"Ongeldige JSON\"}");
                        ctx.Response.ContentType = "application/json; charset=utf-8";
                        ctx.Response.ContentLength64 = badJson.Length;
                        await ctx.Response.OutputStream.WriteAsync(badJson, 0, badJson.Length);
                        ctx.Response.OutputStream.Close();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(question))
                    {
                        ctx.Response.StatusCode = 400;
                        var empty = Encoding.UTF8.GetBytes("{\"error\":\"Lege vraag\"}");
                        ctx.Response.ContentType = "application/json; charset=utf-8";
                        ctx.Response.ContentLength64 = empty.Length;
                        await ctx.Response.OutputStream.WriteAsync(empty, 0, empty.Length);
                        ctx.Response.OutputStream.Close();
                        return;
                    }

                    this.Invoke((MethodInvoker)(() =>
                    {
                        var ip = ctx.Request.RemoteEndPoint?.Address.ToString() ?? "onbekend";


                        LogMessage($"[{apiKey}] incoming question: {question}");

                    }));

                    // Preset → pas aan naar jouw realistische waardes/variabelen
                    var preset = await DBFunct.PresetRecGet(1);

                    string result;
                    try
                    {
                        result = await RagHelper.AskPython(
                            question,
                            preset,
                            DatabasePath + "rag_indexs\\EasyFActuur",
                            DatabasePath + "rag_ask.py"
                        );
                    }
                    catch (Exception ex)
                    {
                        // Python/oproep ging mis
                        ctx.Response.StatusCode = 500;
                        var err = Encoding.UTF8.GetBytes("{\"error\":\"Serverfout: " + ex.Message.Replace("\"", "\\\"") + "\"}");
                        ctx.Response.ContentType = "application/json; charset=utf-8";
                        ctx.Response.ContentLength64 = err.Length;
                        await ctx.Response.OutputStream.WriteAsync(err, 0, err.Length);
                        ctx.Response.OutputStream.Close();
                        return;
                    }

                    // Succes terug
                    textBoxLog.Invoke((Action)(() =>
                    {
                        LogMessage($"Answer: {Environment.NewLine}{result}");
                    }));


                    var respJson = System.Text.Json.JsonSerializer.Serialize(new { answer = result });

                    var buf = Encoding.UTF8.GetBytes(respJson);
                    ctx.Response.ContentType = "application/json; charset=utf-8";
                    ctx.Response.ContentLength64 = buf.Length;
                    await ctx.Response.OutputStream.WriteAsync(buf, 0, buf.Length);
                    ctx.Response.OutputStream.Close();
                    return;
                }

                // Niet gevonden / niet toegestaan
                ctx.Response.StatusCode = 404;
                var notFound = Encoding.UTF8.GetBytes("{\"error\":\"Niet gevonden\"}");
                ctx.Response.ContentType = "application/json; charset=utf-8";
                ctx.Response.ContentLength64 = notFound.Length;
                await ctx.Response.OutputStream.WriteAsync(notFound, 0, notFound.Length);
                ctx.Response.OutputStream.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    ctx.Response.StatusCode = 500;
                    var err = Encoding.UTF8.GetBytes("{\"error\":\"Serverfout: " + ex.Message.Replace("\"", "\\\"") + "\"}");
                    ctx.Response.ContentType = "application/json; charset=utf-8";
                    ctx.Response.ContentLength64 = err.Length;
                    await ctx.Response.OutputStream.WriteAsync(err, 0, err.Length);
                    ctx.Response.OutputStream.Close();
                }
                catch
                {
                    // laatste redmiddel: niets meer doen
                }
            }
        }


        private void LogMessage(string message)
        {
            message = RagHelper.NormalizeNewLines(message);

            // Datum/tijd toevoegen in formaat dd-MM HH:mm:ss
            string timestamp = DateTime.Now.ToString("dd-MM HH:mm:ss");
            message = $"[{timestamp}] {message}";

            if (textBoxLog.InvokeRequired)
            {
                textBoxLog.Invoke((Action)(() => LogMessage(message)));
            }
            else
            {
                textBoxLog.AppendText(message + Environment.NewLine);
                textBoxLog.SelectionStart = textBoxLog.Text.Length;  // caret naar einde
                textBoxLog.ScrollToCaret();                          // en scrollen
            }
        }





        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_listener != null && _listener.IsListening)
            {
                _listener.Stop();
                _listener.Close();
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            textBoxLog.Width = Width - (textBoxLog.Left + 50);
            textBoxLog.Height = Height - (textBoxLog.Top + 70);
        }



        private async void btnAsk_Click(object sender, EventArgs e)
        {
            labelProcessing.Visible = true;

            // var index = keyMap[e.Data.KeyDown.Key];
            //textBoxLog.Text += $"ALT+{index + 1}\r\n";
            UsePreset = (PresetRec)cbALT.Items[cbALT.SelectedIndex];



            string ResultOllama = "";

            if (UsePreset.Rag_index != "None" && UsePreset.Rag_index != "")
            {
                //Clipboard.GetText() bevat de vraag,
                //DatabasePath + "rag_indexs\\EasyFactuur" is het pad van de RAG
                //DatabasePath + "rag_ask.py" is het pad van de python script die de RAG index gebruikt
                //UsePreset is een class van
                // public class PresetRec
                //{
                //    [PrimaryKey]
                //    public long Id { get; set; }

                //    public string Modal { get; set; }
                //    public string Rag_index { get; set; }
                //    public string Prompt { get; set; }

                //    public string Keys { get; set; }
                //    public bool Result { get; set; }
                //}

                ResultOllama = await RagHelper.AskPython(tbAsk.Text, UsePreset, DatabasePath + "rag_indexs\\EasyFActuur", DatabasePath + "rag_ask.py");
            }
            else
            {
                ResultOllama = await AskOllama(tbAsk.Text, UsePreset);
            }

            if (ResultOllama != "")
            {
               
                    textBoxLog.Text += "Result: " + "\r\n" + RagHelper.NormalizeNewLines(ResultOllama) + "\r\n";
               
            }
            labelProcessing.Visible = false;
        }
    }
}