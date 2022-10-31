using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity createNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> RetrieveNotesbyUserID(long userId);
        public IEnumerable<NotesEntity> RetrieveNotesbyNoteID(long userId, long noteId);
        public bool DeleteNotes(int noteId);
        public NotesEntity UpdateNotesData(long userId, long noteId, NotesModel notesModel);
        public bool PinNotes(long noteId);
        public bool ArchiveNotes(long noteId);
        public bool TrashNotes(long noteId);
    }
}
