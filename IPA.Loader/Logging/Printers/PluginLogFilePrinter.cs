﻿using IPA.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPA.Logging.Printers
{
    /// <summary>
    /// Prints log messages to the file specified by the name.
    /// </summary>
    public class PluginLogFilePrinter : GZFilePrinter
    {
        /// <summary>
        /// Provides a filter for this specific printer.
        /// </summary>
        public override Logger.LogLevel Filter { get; set; } = Logger.LogLevel.All;

        private string name;

        /// <summary>
        /// Gets the <see cref="FileInfo"/> for the target file.
        /// </summary>
        /// <returns></returns>
        protected override FileInfo GetFileInfo()
        {
            var logsDir = new DirectoryInfo(Path.Combine("Logs",name));
            logsDir.Create();
            var finfo = new FileInfo(Path.Combine(logsDir.FullName, $"{DateTime.Now:yyyy.MM.dd.HH.mm}.log"));
            return finfo;
        }

        /// <summary>
        /// Creates a new printer with the given name.
        /// </summary>
        /// <param name="name">the name of the logger</param>
        public PluginLogFilePrinter(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Prints an entry to the associated file.
        /// </summary>
        /// <param name="level">the <see cref="Logger.Level"/> of the message</param>
        /// <param name="time">the <see cref="DateTime"/> the message was recorded at</param>
        /// <param name="logName">the name of the log that sent the message</param>
        /// <param name="message">the message to print</param>
        public override void Print(Logger.Level level, DateTime time, string logName, string message)
        {
            foreach (var line in message.Split(new string[] { "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                fileWriter.WriteLine(string.Format("[{2} @ {1:HH:mm:ss}] {0}", line, time, level.ToString().ToUpper()));
        }
    }
}