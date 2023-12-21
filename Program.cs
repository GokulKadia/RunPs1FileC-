using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IGCCFix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //invokePS();
            ExecutePowerShellScript();
            //runBat();
        }
        static void ExecutePowerShellScript()
        {
            try
            {
                // Path to PowerShell executable
                string powerShellExe = "C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe";

                // Path to the PowerShell script
                string scriptPath = "C:\\Windows\\Setup\\IGCC.ps1";

                // Create the PowerShell process start info
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = powerShellExe,
                    Arguments = $"-File \"{scriptPath}\"",
                    Verb = "runas", // Run as administrator
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }; 
                using (Process process = new Process { StartInfo = psi })
                {
                    // Start the process
                    process.Start();

                    // Read the output and error streams
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    // Display the output and error (you can modify this part based on your needs)
                    Console.WriteLine("Output:\n" + output);
                    //Console.WriteLine("Error:\n" + error);

                    // Wait for the process to exit
                    process.WaitForExit();
                }
                               
                System.Threading.Thread.Sleep(5000);

                ServiceController service = new ServiceController("Intel(R) Graphics Command Center Service");
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
