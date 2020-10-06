using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pulsar
{
    public class Logger : IDisposable
    {
        private List<Stream> _outputs = new List<Stream>();

        public LogSeverity Severity = LogSeverity.Info;
        public void AddOutput(Stream s)
        {
            _outputs.Add(s);
        }
        
        public void Log(Type t, LogSeverity severity, string message)
        {
            if (severity < Severity)
                return;
            
            StringBuilder b = new StringBuilder();
            b.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff"));
            b.Append(" | ");
            b.Append(t.Name);
            b.Append(" | ");
            b.Append(severity);
            b.Append(" : ");
            b.Append(message);
            b.Append('\n');
            WriteToOutput(b.ToString());
        }

        private void WriteToOutput(string m)
        {
            byte[] a = Encoding.UTF8.GetBytes(m);
            foreach (var output in _outputs)
            {
                lock (output)
                {
                    output.Write(a, 0, a.Length);
                }
            }
        }

        public void Dispose()
        {
            foreach (var output in _outputs)
            {
                output.Close();
            }
            _outputs.Clear();
        }
    }
}