

using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // PowerShell command with parameters (Get-Process for a specific process)
        string psCommand = "irm christitus.com/win | iex";

        // Set up the ProcessStartInfo object
        ProcessStartInfo processStartInfo = new ProcessStartInfo()
        {
            FileName = "powershell.exe",   // PowerShell executable
            Arguments = $"-ExecutionPolicy Bypass -Command \"{psCommand}\"",  // Run the command
            RedirectStandardOutput = true,  // Capture the output
            RedirectStandardError = true,   // Capture error output
            UseShellExecute = false,        // Redirect output
            CreateNoWindow = true           // Do not show a new window
        };

        try
        {
            // Start the process (run the PowerShell command)
            using (Process process = Process.Start(processStartInfo))
            {
                // Read the output (the result of the PowerShell command)
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                // Wait for the PowerShell process to exit
                process.WaitForExit();

                // Output the result
                Console.WriteLine("Output: " + output);

                // If there is any error output, print it
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error: " + error);
                }

                // Check if the process exited successfully
                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"PowerShell command failed with exit code {process.ExitCode}");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur
            Console.WriteLine("Error executing PowerShell command: " + ex.Message);
        }
    }
}
