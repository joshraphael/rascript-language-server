using System.Collections.Concurrent;
using System.Text;

namespace RAScriptLanguageServer
{
    internal class BufferManager
    {
        private ConcurrentDictionary<string, RAScript> _buffers = new ConcurrentDictionary<string, RAScript>();

        public void UpdateBuffer(string documentPath, StringBuilder buffer, Parser p)
        {
            RAScript rascript = new RAScript(documentPath, buffer, p);
            _buffers.AddOrUpdate(documentPath, rascript, (k, v) => rascript);
        }

        public RAScript? GetBuffer(string documentPath)
        {
            return _buffers.TryGetValue(documentPath, out var buffer) ? buffer : null;
        }
    }
}
