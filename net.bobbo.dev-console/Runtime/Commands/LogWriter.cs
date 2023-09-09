using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using Zenject;

namespace BobboNet.ConsoleCommands
{
    /// <summary>
    /// A command that allows writing the application logs to a text file, as soon as they come in. This
    /// is really handy for debugging purposes.
    /// </summary>
    public class LogWriter : DevConsoleCommandBase
    {
        public override string CommandName => "log_writer";

        /// <summary>
        /// The stream writer that is currently being used to write logs to a text file.
        /// </summary>
        private StreamWriter currentWriter = null;

        /// <summary>
        /// The name of the text file that is currently being written to when logging.
        /// </summary>
        private string currentFileName = null;

        //
        //  Object Methods
        //

        ~LogWriter()
        {
            // If a log writer is still around when we destroy this object, close it!
            CloseLogWriter();
        }

        //
        //  Interface Methods
        //

        public override string ExecuteCommand(string[] args)
        {
            if (args.Length < 2) return ExecuteSubCommandHelp(args);

            switch (args[1].ToLower())
            {
                case "start":
                    return ExecuteSubCommandStart(args);
                case "stop":
                    return ExecuteSubCommandStop(args);
                case "help":
                default:
                    return ExecuteSubCommandHelp(args);
            }
        }

        //
        //  Sub-Commands
        //

        /// <summary>
        /// Execute the command `log_writer help`. This prints a basic help dialog giving info about other commands.
        /// </summary>
        /// <param name="args">The array of string arguments passed in by the console.</param>
        /// <returns>What to have the console print.</returns>
        private string ExecuteSubCommandHelp(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("`log_writer` allows you to manage writing application logs to a text file.");
            sb.AppendLine("You can use it by using the following commands:");
            sb.AppendLine("* `log_writer help` - Displays this help text");
            sb.AppendLine("* `log_writer start [file_path]` - Starts writing logs to a text file. Optionally accepts a file path to write to. Otherwise, creates a file with the current timestamp in the current directory.");
            sb.AppendLine("* `log_writer stop` - Stops writing logs, if started.");
            return sb.ToString();
        }

        /// <summary>
        /// Execute the command `log_writer start [file_path]`. This starts writing logs to a text file.
        /// </summary>
        /// <param name="args">The array of string arguments passed in by the console.</param>
        /// <returns>What to have the console print.</returns>
        private string ExecuteSubCommandStart(string[] args)
        {
            // Create a path to the new log file to create. If we have a desired path, use that instead
            string filePath = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
            if (args.Length >= 3) filePath = args[2];
            filePath = Path.GetFullPath(filePath);

            try
            {
                // Open the log writer. This may throw if there's an IO problem!
                OpenLogWriter(filePath);
                return $"Now writing logs to '{filePath}'";
            }
            catch (Exception exception)
            {
                return $"Failed to start: '{exception}'";
            }
        }

        /// <summary>
        /// Execute the command `log_writer stop`. This stops writing logs to a text file.
        /// </summary>
        /// <param name="args">The array of string arguments passed in by the console.</param>
        /// <returns>What to have the console print.</returns>
        private string ExecuteSubCommandStop(string[] args)
        {
            if (currentWriter == null) return "There are no log writers running.";

            // Hold on to the current log name for logging purposes
            string cachedFilePath = currentFileName;

            // Close the log writer, then tell us about it!
            CloseLogWriter();
            return $"Stopped writing logs to '{cachedFilePath}'";
        }

        //
        //  Logging Methods
        //

        private void OpenLogWriter(string filePath)
        {
            // If there's already a stream writer, CLOSE IT
            if (currentWriter != null) currentWriter.Close();

            // Open up a new writer at the desired path
            currentWriter = File.AppendText(filePath);
            currentFileName = filePath;

            // Tell unity to write any new logs to the current stream writer
            Application.logMessageReceived += WriteUnityLogToWriter;
        }

        private void CloseLogWriter()
        {
            // If there are no writers at the moment, exit early.
            if (currentWriter == null) return;

            // Tell unity to STOP writing any new logs to the current stream writer
            Application.logMessageReceived -= WriteUnityLogToWriter;

            // Close the stream writer, and mark as null
            currentWriter.Close();
            currentWriter = null;
            currentFileName = null;
        }

        private void WriteUnityLogToWriter(string logString, string stackTrace, LogType type)
        {
            currentWriter.WriteLine($"[{type}] {logString}");
            currentWriter.Flush();
        }
    }
}

