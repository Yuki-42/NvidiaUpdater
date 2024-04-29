using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nvidia_Updater_Framework
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            
            // Get a list of folders in C:\ProgramData\NVIDIA Corporation\Downloader matching the a 32 character string (the folder name)
            const string baseDir = @"C:\ProgramData\NVIDIA Corporation\Downloader";
            // Replace with your own Regex.
            Regex dirNames = new Regex("[\\d\\w]{32}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string[] dirsFiltered = Directory.EnumerateDirectories(baseDir).Where(dir => dirNames.IsMatch(dir)).ToArray();
            
            Console.WriteLine(dirsFiltered.Length + " folders found.");
            
            // Get the latest folder
            string latestFolder = dirsFiltered.OrderByDescending(Directory.GetLastWriteTime).First();
            Console.WriteLine("Latest folder: " + latestFolder);
            
            // Run the executable in the latest folder
            string latestExe = Directory.EnumerateFiles(latestFolder, "*.exe").First();
            Console.WriteLine("Latest executable: " + latestExe);
            
            // TODO: Add error handling for the case where the user doesn't have permission to run the executable or clicks "No" on the UAC prompt.
            Console.WriteLine("Running executable...");
            System.Diagnostics.Process.Start(latestExe);
            
            Console.WriteLine("Done.");
        }
    }
}