using Notes.WebUI.Interfaces;
using Notes.WebUI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Notes.WebUI.Data
{
    public class NoteService : INoteService
    {
        private readonly HttpClient _httpClient;
        private const string url = "api/notes";

        public NoteService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("NoteApiClient");
        }

        public async Task<Guid> CreateNote(string title, string text)
        {
            var model = new NotesModel
            {
                Text = text,
                Title = title,
                CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
            };

            var response = await _httpClient.PostAsJsonAsync(url, model);

            if (response.IsSuccessStatusCode)
            {
                var noteId = await response.Content.ReadFromJsonAsync<Guid>();

                return noteId;
            }
            else
            {
                return Guid.Empty;
            }
        }

        public async Task DeleteNote(Guid id)
        {
            await _httpClient.DeleteAsync(url + $"/{id}"); 
        }

        public async Task<NotesModel> GetNoteById()
        {
            return await _httpClient.GetFromJsonAsync<NotesModel>(url);
        }

        public async Task<IEnumerable<NotesModel>> GetNotes()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<NotesModel>>(url);
        }

        public async Task<NotesModel> UpdateNote(NotesModel model)
        {
            var response = await _httpClient.PutAsJsonAsync(url, model);

            if (response.IsSuccessStatusCode)
            {
                var note = await response.Content.ReadFromJsonAsync<NotesModel>();

                return note;
            }
            else
            {
                return null;
            }
        }
    }
}
