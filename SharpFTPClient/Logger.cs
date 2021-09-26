/*
 * https://stackoverflow.com/questions/2196097/elegant-log-window-in-winforms-c-sharp 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SharpFTPClient
{
    class Logger
    {
        private readonly Queue<LogEntry> _log;
        private uint _entryNumber;
        private readonly uint _maxEntries;
        private readonly object _logLock = new object();
        private readonly Color _defaultColor = Color.White;

        private class LogEntry
        {
            public uint EntryId;
            public DateTime EntryTime;
            public string EntryText;
            public Color EntryColor;
        }

        public Logger(uint maximumEntries)
        {
            _log = new Queue<LogEntry>();
            _maxEntries = maximumEntries;
        }

        private struct ColorTableItem
        {
            public uint Index;
            public string RichColor;
        }

        public string GetLogAsRichText(bool includeEntryNumbers)
        {
            lock (_logLock)
            {
                var sb = new StringBuilder();

                var uniqueColors = BuildRichTextColorTable();
                sb.AppendLine($@"{{\rtf1{{\colortbl;{ string.Join("", uniqueColors.Select(d => d.Value.RichColor)) }}}");

                foreach (var entry in _log)
                {
                    if (includeEntryNumbers)
                        sb.Append($"\\cf1 { entry.EntryId }. ");

                    sb.Append($"\\cf1 { entry.EntryTime.ToShortDateString() } { entry.EntryTime.ToShortTimeString() }: ");

                    var richColor = $"\\cf{ uniqueColors[entry.EntryColor].Index + 1 }";
                    sb.Append($"{ richColor } { entry.EntryText }\\par").AppendLine();
                }
                return sb.ToString();
            }
        }

        public void AddToLog(string text)
        {
            AddToLog(text, _defaultColor);
        }

        public void AddToLog(string text, Color entryColor)
        {
            lock (_logLock)
            {
                if (_entryNumber >= uint.MaxValue)
                    _entryNumber = 0;
                _entryNumber++;
                var logEntry = new LogEntry { EntryId = _entryNumber, EntryTime = DateTime.Now, EntryText = text, EntryColor = entryColor };
                _log.Enqueue(logEntry);

                while (_log.Count > _maxEntries)
                    _log.Dequeue();
            }
        }

        public void Clear()
        {
            lock (_logLock)
            {
                _log.Clear();
            }
        }

        private Dictionary<Color, ColorTableItem> BuildRichTextColorTable()
        {
            var uniqueColors = new Dictionary<Color, ColorTableItem>();
            var index = 0u;

            uniqueColors.Add(_defaultColor, new ColorTableItem() { Index = index++, RichColor = ColorToRichColorString(_defaultColor) });

            foreach (var c in _log.Select(l => l.EntryColor).Distinct().Where(c => c != _defaultColor))
                uniqueColors.Add(c, new ColorTableItem() { Index = index++, RichColor = ColorToRichColorString(c) });

            return uniqueColors;
        }

        private string ColorToRichColorString(Color c)
        {
            return $"\\red{c.R}\\green{c.G}\\blue{c.B};";
        }
    }
}
