using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HelmClientWrapper
{
    public static class HelmClient
    {
        public static async Task<T> RunCommandAsync<T>(HelmCommand<T> command)
        {
            Console.WriteLine(command);

            var cmd = new Process
            {
                EnableRaisingEvents = true,
                StartInfo =
                {
                    FileName = "helm",
                    Arguments = typeof(T) == typeof(string)
                        ? command.ToString()
                        : $"{command} --output json",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            cmd.Start();
            cmd.WaitForExit();

            if (cmd.ExitCode != 0)
            {
                throw new Exception(
                    $"{command.GetType().FullName} failed. [{nameof(cmd.ExitCode)}={cmd.ExitCode} {nameof(cmd.StandardError)}={await cmd.StandardError.ReadToEndAsync()}]");
            }

            return JsonConvert.DeserializeObject<T>(await cmd.StandardOutput.ReadToEndAsync());
        }
    }
}