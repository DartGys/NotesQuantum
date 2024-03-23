using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.BLL.Models
{
    public class NoteModel
    {
        private NoteModel(Guid id, string title, string text, DateTime createDate)
        {
            Id = id;
            Title = title;
            Text = text;
            CreateDate = createDate;
        }

        public Guid Id { get; }
        public string Title { get; }
        public string Text { get; }
        public DateTime CreateDate { get; }

        public static (NoteModel, string Error) Create(Guid id, string title, string text, DateTime createDate)
        {
            string error = string.Empty;

            if(id == Guid.Empty)
            {
                error = "Note Id error";
            }

            if(string.IsNullOrWhiteSpace(title))
            {
                error = "Note title empty or whitespace";
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                error = "Note text empty or whitespace";
            }

            var note = new NoteModel(id, title, text, createDate);

            return (note, error);
        }
    }
}
