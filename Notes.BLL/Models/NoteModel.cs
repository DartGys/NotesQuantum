using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.BLL.Models
{
    public class NoteModel
    {
        public NoteModel(Guid id, string title, string text, DateTime createDate)
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
    }
}
