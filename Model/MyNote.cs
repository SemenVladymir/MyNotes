using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Model
{
    public class MyNote : INotifyPropertyChanged
    {
        private int id;
        private DateTime noteDateTime = DateTime.Now;
        private string? description;
        private string shortDescription;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public DateTime NoteDateTime
        {
            get { return noteDateTime; }
            set { noteDateTime = value; OnPropertyChanged("NoteDateTime"); }
        }


        public string Description
        {
            get { return description ?? ""; }
            set { description = value; OnPropertyChanged("Description"); }
        }


        public string ShortDescription
        {
            get { return shortDescription; }
            set { shortDescription = value; OnPropertyChanged("ShortDescription"); }
        }


        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return $"{NoteDateTime}\n{ShortDescription}\n{Description}";
        }

    }
}
