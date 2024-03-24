using Notes.BLL.Models;

namespace Notes.WebAPI.Validation
{
    public static class NoteValidator
    {
        public static string Validation(NoteModel model)
        {
            string error = string.Empty;

            if (model == null)
            {
                error = "Note model is null";
            }

            else if(string.IsNullOrWhiteSpace(model.Text))
            {
                error = "Note text is empty or whitespace";
            }

            else if (string.IsNullOrWhiteSpace(model.Title))
            {
                error = "Note title is empty or whitespace";
            }

            return error;
        }
    }
}
