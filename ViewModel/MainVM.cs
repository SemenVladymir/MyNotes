using MyNotes.DBConnection;
using MyNotes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static Azure.Core.HttpHeader;

namespace MyNotes.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public ObservableCollection <MyNote> MyNotes { get; set; }
        private string shortText;
        private string description;
        private MyCommand addCommand;

        public MyCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new MyCommand(obj =>
                  {
                      if (!string.IsNullOrEmpty(ShortText))
                      {
                          MyNote note = new MyNote { Description = this.Description, ShortDescription = ShortText, NoteDateTime = DateTime.Now };
                          if (SaveNote(note))
                              MyNotes.Insert(0, note);
                      }
                      else
                          MessageBox.Show("You need input some text");
                  }));
            }
        }

        public string ShortText
        {
            get { return shortText; } 
            set {  shortText = value; OnPropertyChanged("ShortText"); }
        }

        public string Description
        {
            get => description; 
            set { description = value; OnPropertyChanged("Description"); }
        }


        public MainVM()
        {
            using (MyContext context = new MyContext())
            {
                MyNotes =  new ObservableCollection<MyNote>(context.Notes.ToList());
                MyNotes = new ObservableCollection < MyNote > (MyNotes.Reverse());
            }
        }

        public bool SaveNote(MyNote note)
        {
            using (MyContext context = new MyContext())
            {
                context.Add(note);
                if (context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
