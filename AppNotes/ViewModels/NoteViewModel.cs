using AppNotes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppNotes.ViewModels
{
    public partial class NoteViewModel : INotifyPropertyChanged
    {

        // Fields
        private string _noteTitle;
        private string _noteDescription;
        private Note _selectedNote;
        
        // Get and Set
        public string NoteTitle
        {
            get => _noteTitle;
            set
            {
                if (value != _noteTitle)
                {
                    _noteTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NoteDescription
        {
            get => _noteDescription;
            set
            {
                if (value != _noteDescription)
                {
                    _noteDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                if (value != _selectedNote)
                {
                    _selectedNote = value;
                    NoteTitle = _selectedNote.Title;
                    NoteDescription = _selectedNote.Description;
                    OnPropertyChanged();
                }
            }
        }

        // Property
        public ObservableCollection <Note> NoteCollection { get; set; }
        public ICommand AddNoteCommand { get;  }
        public ICommand EditNoteCommand { get;  }
        public ICommand RemoveNoteCommand { get;  }

        public NoteViewModel()
        {
            NoteCollection = new ObservableCollection<Note>();
            AddNoteCommand = new Command(AddNote);
            RemoveNoteCommand = new Command(RemoveNote);
            EditNoteCommand = new Command(EditNote);
        }

        private void RemoveNote(object obj)
        {
            if(SelectedNote != null)
            {
                NoteCollection.Remove(SelectedNote);
                // Rest Value
                NoteTitle = string.Empty;
                NoteDescription = string.Empty;
            }
        }

        private void EditNote(object obj)
        {
            if (SelectedNote != null)
            {
                foreach (Note note in NoteCollection.ToList())
                {
                    if(note == SelectedNote)
                    {
                        var newNote = new Note
                        {
                            // Set New Note
                            Id = note.Id,
                            Title = NoteTitle,
                            Description = NoteDescription
                        };

                        // Remove note
                        NoteCollection.Remove(note);
                        NoteCollection.Add(newNote);
                    }
                }
            }
        }

        private void AddNote(object obj)
        {
            // Generate a unique ID for the new person
            int newId = NoteCollection.Count > 0 ? NoteCollection.Max(p => p.Id) + 1 : 1;
            // Set New Note
            var note = new Note
            {
                Id = newId,
                Title = NoteTitle,
                Description = NoteDescription,
            };
            NoteCollection.Add(note);
            // Rest Value
            NoteTitle = string.Empty;
            NoteDescription = string.Empty;
        }

        // PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
