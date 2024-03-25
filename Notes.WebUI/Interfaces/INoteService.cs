using Notes.WebUI.Models;

namespace Notes.WebUI.Interfaces
{
    public interface INoteService
    {
        public Task<IEnumerable<NotesModel>> GetNotes();
        public Task<NotesModel> GetNoteById();
        public Task<Guid> AddNote(NotesModel model);
        public Task DeleteNote(Guid id);
        public Task<NotesModel> UpdateNote(NotesModel model);
    }
}
