using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity createNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> RetrieveNotesbyUserID(long userId);
        public IEnumerable<NotesEntity> RetrieveNotesbyNoteID(long userId, long noteId);
        public bool DeleteNotes(int noteId);
    }
}
