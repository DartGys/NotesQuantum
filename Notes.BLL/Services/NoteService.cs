using Notes.BLL.Interfaces;
using Notes.BLL.Models;
using Notes.DL.Data;

namespace Notes.BLL.Services
{
    public class NoteService : INoteService
    {
        private readonly NotesDbContext _context;

        public NoteService(NotesDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NoteModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NoteModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<NoteModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(NoteModel model)
        {
            throw new NotImplementedException();
        }
    }
}
