using Notes.WebUI.Models;

namespace Notes.WebUI.Interfaces
{
    public interface INoteService
    {
        public Task<IEnumerable<NotesModel>> GetNotes();
        public Task<NotesModel> GetNoteById();
        public Task<Guid> CreateNote(string title, string text);
        public Task DeleteNote(Guid id);
        public Task<NotesModel> UpdateNote(Guid id, string title, string text);
    }
}
