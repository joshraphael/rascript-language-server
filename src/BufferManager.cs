using System.Collections.Concurrent;
using System.Text;
using RASharp;
using RASharp.Models;

namespace RAScriptLanguageServer
{
    internal class BufferManager
    {
        private ConcurrentDictionary<string, RAScript> _buffers = new ConcurrentDictionary<string, RAScript>();
        private RetroAchievements? _retroachievements;

        public async Task UpdateBufferAsync(string documentPath, StringBuilder buffer, Parser p)
        {
            bool updateCodeNotes = true;
            RAScript? oldScript = this.GetBuffer(documentPath);
            if (oldScript != null)
            {
                int oldGameID = oldScript.GetParser().GetGameID();
                int newGameID = p.GetGameID();
                if (oldGameID == newGameID)
                {
                    updateCodeNotes = false;
                }
            }
            GetCodeNotes? codeNotes = oldScript?.GetParser().GetCodeNotes();
            if (updateCodeNotes)
            {
                if (this._retroachievements != null && p.GetGameID() > 0)
                {
                    try
                    {
                        codeNotes = await this._retroachievements.GetCodeNotes(p.GetGameID());
                    }
                    catch (Exception)
                    {
                        codeNotes = null;
                    }
                }
            }
            p.loadCodeNotes(codeNotes);
            RAScript rascript = new RAScript(documentPath, buffer, p);
            _buffers.AddOrUpdate(documentPath, rascript, (k, v) => rascript);
        }

        public void SetRAClient(RetroAchievements retroAchievements)
        {
            this._retroachievements = retroAchievements;
        }

        public RAScript? GetBuffer(string documentPath)
        {
            return _buffers.TryGetValue(documentPath, out var buffer) ? buffer : null;
        }
    }
}
