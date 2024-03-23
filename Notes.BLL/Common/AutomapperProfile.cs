using AutoMapper;
using Notes.BLL.Models;
using Notes.DL.Data.Entities;

namespace Notes.BLL.Common
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() 
        {
            CreateMap<Note, NoteModel>()
                .ReverseMap();
        }
    }
}
