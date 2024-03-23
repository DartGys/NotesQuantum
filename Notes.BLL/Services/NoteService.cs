using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes.BLL.Interfaces;
using Notes.BLL.Models;
using Notes.DL.Data;
using Notes.DL.Data.Entities;

namespace Notes.BLL.Services
{
    public class NoteService : INoteService
    {
        private readonly NotesDbContext _context;
        private readonly IMapper _mapper;

        public NoteService(NotesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(NoteModel model)
        {
            var entity = _mapper.Map<Note>(model);

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid modelId)
        {
            var entity = new Note
            {
                Id = modelId
            };

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NoteModel>> GetAllAsync()
        {
            var entities = await _context.Notes.ToListAsync();
            
            var models = _mapper.Map<IEnumerable<NoteModel>>(entities);

            return models;
        }

        public async Task<NoteModel> GetByIdAsync(Guid id)
        {
            var entity = await _context.Notes.FindAsync(id);

            var model = _mapper.Map<NoteModel>(entity);

            return model;
        }

        public async Task UpdateAsync(NoteModel model)
        {
            var entity = _mapper.Map<Note>(model);

            _context.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
