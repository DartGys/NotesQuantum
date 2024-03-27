using Notes.DL.Data;
using Notes.DL.Data.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Notes.WebAPI.Controllers;
using Notes.BLL.Models;
using Notes.BLL.Services;
using Notes.BLL.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Notes.IntegrationTests.NotesWebApiTests
{
    public class NotesApiIntegrationTests
    {

        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private const string RequestUri = "api/notes/";

        public NotesApiIntegrationTests()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task NotesController_Get_ReturnsAllNotes()
        {
            // Arrange
            var expected = ExpectedNotesModel.ToList();

            // Act
            var response = await _client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<NoteModel>>(content);

            // Assert
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task NotesController_GetById_ReturnsNoteById()
        {
            //arrange
            var expected = ExpectedNotesModel.First();
            var noteId = new Guid("d4545d7d-aa74-4285-a131-cf0deca727a0");

            //act
            var response = await _client.GetAsync(RequestUri + noteId);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<NoteModel>(content);

            //assert
            Assert.NotNull(actual);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task NotesController_Add_AddNote()
        {
            // arrange
            var note = new NoteModel(Guid.NewGuid(), "Test Title", "Test Text", DateTime.UtcNow);

            var content = new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8, "application/json");

            //act
            var response = await _client.PostAsync(RequestUri, content);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var noteIdInResponse = JsonConvert.DeserializeObject<Guid>(stringResponse);
            await CheckNotesInfoIntoDb(note, noteIdInResponse, 6);
        }

        [Fact]
        public async Task NotesController_Add_ThrowsExeptionIfModelIsInvalid()
        {
            // title is empty
            var note = new NoteModel(Guid.NewGuid(), string.Empty, "Text", DateTime.UtcNow);
            await CheckExceptionWhileAddNewModel(note);

            note.Title = "Title";
            note.Text = string.Empty;
            await CheckExceptionWhileAddNewModel(note);
        }

        [Fact]
        public async Task NotesController_Update_UpdateNote()
        {
            //arrange
            var note = new NoteModel(new Guid("d4545d7d-aa74-4285-a131-cf0deca727a0"), "Test Update Note", "Test Content Update", DateTime.UtcNow);
            var content = new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8, "application/json");

            //act
            var response = await _client.PutAsync(RequestUri, content);

            //assert
            response.EnsureSuccessStatusCode();
            await CheckNotesInfoIntoDb(note, note.Id, 5);
        }

        [Fact]
        public async Task NotesController_Update_ThrowsExeptionIfModelIsInvalid()
        {
            // title is empty
            var note = new NoteModel(new Guid("d4545d7d-aa74-4285-a131-cf0deca727a0"), string.Empty, "Text Update", DateTime.UtcNow);
            await CheckExceptionWhileUpdateModel(note);

            note.Title = "Title Update";
            note.Text = string.Empty;
            await CheckExceptionWhileUpdateModel(note);
        }

        [Fact]
        public async Task NotesController_Delete_DeleteNote()
        {
            //arrange 
            var noteId = ExpectedNotesModel.First().Id;
            var expectedLength = ExpectedNotesModel.Count() - 1;

            //act
            var response = await _client.DeleteAsync(RequestUri + noteId);
            response.EnsureSuccessStatusCode();

            //assert
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<NotesDbContext>();
                context.Notes.Should().HaveCount(expectedLength);
            }
        }


        private static readonly IEnumerable<NoteModel> ExpectedNotesModel =
            new List<NoteModel>()
            {
                new NoteModel( new Guid("d4545d7d-aa74-4285-a131-cf0deca727a0"), "Test Note 1", "Test Content 1", TestHelper.createDate),
                new NoteModel( new Guid("c38af979-eaaa-45ee-b628-da3b5e4fe0bb"), "Test Note 2", "Test Content 2", TestHelper.createDate),
                new NoteModel( new Guid("66b8c7b5-941c-40e4-82f6-03cf9c1ddbea"), "Test Note 3", "Test Content 3", TestHelper.createDate),
                new NoteModel( new Guid("a5aa21f3-0769-490a-9479-7f6c4fdac753"), "Test Note 4", "Test Content 4", TestHelper.createDate),
                new NoteModel( new Guid("aa0b4fe2-2596-470f-8b93-e13a4ce1eb1f"), "Test Note 5", "Test Content 5", TestHelper.createDate),
            };

        private async Task CheckNotesInfoIntoDb(NoteModel note, Guid noteId, int expectedLength)
        {
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<NotesDbContext>();
                context.Notes.Should().HaveCount(expectedLength);

                var dbCustomer = await context.Notes.FindAsync(noteId);
                dbCustomer.Should().NotBeNull();

                var dbPerson = await context.Notes.FindAsync(noteId);
                dbPerson.Should().NotBeNull().And.BeEquivalentTo(note, options => options
                    .Including(x => x.Title)
                    .Including(x => x.Text)
                    .Including(x => x.CreateDate)
                );
            }
        }

        private async Task CheckExceptionWhileAddNewModel(NoteModel note)
        {
            var context = new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(RequestUri, context);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task CheckExceptionWhileUpdateModel(NoteModel note)
        {
            var context = new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(RequestUri, context);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
