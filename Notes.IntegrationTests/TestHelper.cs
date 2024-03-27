using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes.BLL.Common;
using Notes.DL.Data;
using Notes.DL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.IntegrationTests
{
    internal static class TestHelper
    {
        public static DateTime createDate = DateTime.Now;
        public static DbContextOptions<NotesDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new NotesDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        public static IMapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }

        public static void SeedData(NotesDbContext _dbContext)
        {
            var notes = new List<Note>
            {
                new Note { Id = new Guid("d4545d7d-aa74-4285-a131-cf0deca727a0"), Title = "Test Note 1", Text = "Test Content 1", CreateDate = createDate },
                new Note { Id = new Guid("c38af979-eaaa-45ee-b628-da3b5e4fe0bb"), Title = "Test Note 2", Text = "Test Content 2", CreateDate = createDate },
                new Note { Id = new Guid("66b8c7b5-941c-40e4-82f6-03cf9c1ddbea"), Title = "Test Note 3", Text = "Test Content 3", CreateDate = createDate },
                new Note { Id = new Guid("a5aa21f3-0769-490a-9479-7f6c4fdac753"), Title = "Test Note 4", Text = "Test Content 4", CreateDate = createDate },
                new Note { Id = new Guid("aa0b4fe2-2596-470f-8b93-e13a4ce1eb1f"), Title = "Test Note 5", Text = "Test Content 5", CreateDate = createDate },
            };

            _dbContext.Notes.AddRange(notes);
            _dbContext.SaveChanges();
        }
    }
}
