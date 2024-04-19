

using Ollamaclient.SQLiteDatabase;
using OllamaSharp;
using OllamaSharp.Models;
using System.Text;
using System.Windows.Navigation;
using WindowsInput.Events;
using WindowsInput.Events.Sources;
using static Ollamaclient.SQLiteDatabase.Tables;

namespace Ollamaclient
{
    public partial class Form1 : Form
    {
        // private IKeyboardEventSource m_Keyboard;

        private PresetRec UsePreset = null;

        private SQLiteFunctions DBFunct = SQLiteFunctions.Instance;
        private GlobalStuff GS = GlobalStuff.Instance;

        //var uri = new Uri("http://localhost:11434");
        private OllamaApiClient ollama;

        private bool ALT_Pressed;
        private bool PresetInProgress;
        private IKeyboardEventSource Keyboard;

        private bool keyBussy;

        

        public Form1()
        {
            InitializeComponent();
            // https://github.com/MediatedCommunications/WindowsInput
            // https://github.com/awaescher/OllamaSharp

            DBFunct.DBInit();

            //InitializeAsync().ConfigureAwait(true).GetAwaiter().GetResult();

            InitializeAsync();

            //lbOnlineAgents.Items.Clear();
            //lbOnlineAgents.CustomTabOffsets.Clear();
            //lbOnlineAgents.UseCustomTabOffsets = true;
            //lbOnlineAgents.CustomTabOffsets.Add(95);

            cbALT.DisplayMember = "Keys";
            cbALT.ValueMember = "Id";

            // Add "ALT + 1" to "ALT + 9" to the ComboBox

            Keyboard = default(IKeyboardEventSource);

            Keyboard = WindowsInput.Capture.Global.Keyboard();
            Keyboard.KeyEvent += this.Keyboard_KeyEvent;
        }

        private async void InitOllama()
        {
            var uri = new Uri(tbOllamaURL.Text);
            ollama = new OllamaApiClient(uri);
            var localModels = await ollama.ListLocalModels();
            // await ollama.PullModel("mistral", status => Console.WriteLine($"({status.Percent}%) {status.Status}"));
            cbModel.Items.Clear();

            foreach (var model in localModels.ToList())
            {
                cbModel.Items.Add(model.Name);
            }
            //  cbModel.DataSource=(localModels.ToList());
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
                            presetRec.Modal = "mistral:latest";
                            presetRec.Prompt = "Fix all typos and casing and punctuation in this text, but preserve all new line characters:\r\n\"{input}\"\r\nReturn only the corrected text, don't include a preamble";
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
            if (e.Data?.KeyDown?.Key == WindowsInput.Events.KeyCode.LAlt)
            {
                ALT_Pressed = true;
                // textBoxLog.Text = textBoxLog.Text + "ALT pressed";
            }

            if (e.Data?.KeyUp?.Key == WindowsInput.Events.KeyCode.LAlt)
            {
                ALT_Pressed = false;
                // textBoxLog.Text = textBoxLog.Text +"ALT released";
            }

            if (keyBussy) return;

            if (ALT_Pressed)
            {
                if (e.Data?.KeyDown?.Key == WindowsInput.Events.KeyCode.D1)
                {
                    textBoxLog.Text = textBoxLog.Text + "ALT+1\r\n";
                    UsePreset = (PresetRec)cbALT.Items[0];
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
                    string ResultOllama = await AskOllama(Clipboard.GetText(), UsePreset);
                    if (ResultOllama != "")
                    {
                        Clipboard.SetText(ResultOllama);
                        Thread.Sleep(100);
                        await WindowsInput.Simulate.Events()
                        .ClickChord(KeyCode.LControl, KeyCode.V).Wait(50)
                        .Invoke();
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
            return responseBuilder.ToString().Trim();
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

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Keyboard?.Dispose();
            Keyboard = null;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            _ = await DBFunct.SettingSet(new SettingRec { Name = "OllamaURL", ValueString = tbOllamaURL.Text });
            InitOllama();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            PresetInProgress = true;
            PresetRec presetRec = (PresetRec)cbALT.SelectedItem;
            //presetRec.Id = cbALT.SelectedIndex + 1;
            presetRec.Modal = cbModel.Text;
            presetRec.Prompt = tbPrompt.Text;
            cbALT.Items[cbALT.SelectedIndex] = presetRec;
            await DBFunct.PresetRecAddUpdate(presetRec);
            PresetInProgress = false;
        }

        private async void cbALT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PresetInProgress == true) return;

            cbModel.Text = "";
            tbPrompt.Text = "";
            var presetRec = await DBFunct.PresetRecGet(cbALT.SelectedIndex + 1);

            if (presetRec != null)
            {
                cbModel.Text = presetRec.Modal;
                tbPrompt.Text = presetRec.Prompt;
            }
        }
    }
}