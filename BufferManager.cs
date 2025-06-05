using System.Collections.Concurrent;
using System.Text;

namespace Server
{
    internal class BufferManager
    {
        private ConcurrentDictionary<string, StringBuilder> _buffers = new ConcurrentDictionary<string, StringBuilder>();

        public void UpdateBuffer(string documentPath, StringBuilder buffer)
        {
            _buffers.AddOrUpdate(documentPath, buffer, (k, v) => buffer);
        }

        public StringBuilder? GetBuffer(string documentPath)
        {
            return _buffers.TryGetValue(documentPath, out var buffer) ? buffer : null;
        }
    }
}
