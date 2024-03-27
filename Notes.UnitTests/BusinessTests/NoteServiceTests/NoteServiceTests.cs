using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;
using Notes.BLL.Models;
using Notes.BLL.Services;
using Notes.DL.Data;
using Notes.DL.Data.Entities;

namespace Notes.UnitTests.BusinessTests.NoteServiceTests
{
    public class NoteServiceTests
    {
        private readonly Mock<NotesDbContext> mockContext;
        private readonly Mock<IMapper> mockMapper;

        public NoteServiceTests()
        {
            mockContext = new Mock<NotesDbContext>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddAsync_ValidNote_ReturnsValidId()
        {
            // Arrange
            var noteId = Guid.NewGuid();
            var noteModel = new NoteModel(noteId, "Test Note", "This is a test note", DateTime.Now);

            var expectedId = Guid.NewGuid();

            mockMapper.Setup(m => m.Map<Note>(It.IsAny<NoteModel>())).Returns(new Note { Id = expectedId });

            var noteService = new NoteService(mockContext.Object, mockMapper.Object);

            // Act
            var resultId = await noteService.AddAsync(noteModel);

            // Assert
            Assert.Equal(expectedId, resultId); // Перевіряємо, чи повернувся очікуваний ідентифікатор
            mockContext.Verify(c => c.AddAsync(It.IsAny<Note>(), CancellationToken.None), Times.Once); // Перевіряємо, чи викликався метод AddAsync у контексті бази даних один раз
            mockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once); // Перевіряємо, чи викликався метод SaveChangesAsync у контексті бази даних один раз
        }

        [Fact]
        public async Task DeleteAsync_RemovesNoteFromContext()
        {
            // Arrange
            var noteId = Guid.NewGuid();
            var service = new NoteService(mockContext.Object, mockMapper.Object); 

            // Act
            await service.DeleteAsync(noteId); 

            // Assert
            mockContext.Verify(c => c.Remove(It.IsAny<Note>()), Times.Once); // Перевіряємо, чи викликався метод Remove у контексті бази даних один раз
            mockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once); // Перевіряємо, чи викликався метод SaveChangesAsync у контексті бази даних один раз
        }


        [Fact]
        public async Task GetByIdAsync_ShouldReturnNoteModelById()
        {
            // Arrange
            var noteId = Guid.NewGuid();
            var noteEntity = new Note { Id = noteId, Title = "Test Note", Text = "Test Content", CreateDate = DateTime.UtcNow };
            var noteModel = new NoteModel(noteId, "Test Note", "Test Content", DateTime.UtcNow);

            var mockSet = new Mock<DbSet<Note>>();
            mockSet.Setup(m => m.FindAsync(noteId)).ReturnsAsync(noteEntity);

            mockContext.Setup(c => c.Notes).Returns(mockSet.Object);

            mockMapper.Setup(m => m.Map<NoteModel>(noteEntity)).Returns(noteModel);

            var service = new NoteService(mockContext.Object, mockMapper.Object);

            // Act
            var result = await service.GetByIdAsync(noteId);

            // Assert
            result.Should().BeEquivalentTo(noteModel);
        }
    }
}   
