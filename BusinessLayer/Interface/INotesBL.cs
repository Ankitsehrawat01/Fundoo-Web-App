using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public bool PinNotes(long userId, long noteId);
        public bool ArchiveNotes(long userId, long noteId);
        public bool TrashNotes(long userId, long noteId);
        public NotesEntity ChangeBackgroundColor(long userId, long noteId, string Backgroundcolor);
        public string Image(IFormFile image, long noteId, long userId);
    }
}
