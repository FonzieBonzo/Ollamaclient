using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Ollamaclient.SQLiteDatabase.Tables;

namespace Ollamaclient
{
    public static class RagHelper
    {
        private static string PythonExe = @"python.exe"; // pas dit pad aan!

        public class RagResult
        {
            [JsonPropertyName("answer")]
            public string Answer { get; set; } = "";

            [JsonPropertyName("error")]
            public string? Error { get; set; }
        }

        public static string NormalizeNewLines(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            // alles eerst naar LF
            s = s.Replace("\r\n", "\n").Replace("\r", "\n");
            // en dan naar platform-standaard (Windows => CRLF)
            return s.Replace("\n", Environment.NewLine);
        }

        public static async Task<string> AskPython(
    string question,
    PresetRec preset,
    string indexDir,
    string ragAskPyPath)
        {
            var args = new StringBuilder();
            args.Append('"').Append(ragAskPyPath).Append('"');
            args.Append(" --index-dir ").Append('"').Append(indexDir).Append('"');
            args.Append(" --ollama-model ").Append('"').Append(preset.Modal).Append('"');
            args.Append(" --use-ollama");
            if (!string.IsNullOrWhiteSpace(preset.Prompt))
                args.Append(" --extra-system ").Append('"').Append(preset.Prompt).Append('"');
            args.Append(" --format json");
            args.Append(" --question ").Append('"').Append(question.Replace("\"", "\\\"")).Append('"');

            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = args.ToString(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8
            };

            using var proc = new Process { StartInfo = psi };
            proc.Start();

            // Hier jouw regels
            string stdout = await proc.StandardOutput.ReadToEndAsync();
            string stderr = await proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();

            try
            {
                // Probeer de JSON van Python te parsen
                using var doc = JsonDocument.Parse(stdout);
                return doc.RootElement.GetProperty("answer").GetString() ?? "";
            }
            catch (Exception ex)
            {
                // Hier dump je alles wat misging
                return "[FOUT parsing output] " + ex.Message
                     + "\nSTDOUT:\n" + stdout
                     + (string.IsNullOrWhiteSpace(stderr) ? "" : "\nSTDERR:\n" + stderr);
            }
        }

    }
}
