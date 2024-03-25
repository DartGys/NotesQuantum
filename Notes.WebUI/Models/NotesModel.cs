namespace Notes.WebUI.Models
{
    public class NotesModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
