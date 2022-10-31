using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL iNotesRL;
        public NotesBL(INotesRL iNotesRL)
        {
            this.iNotesRL = iNotesRL;
        }
        public NotesEntity createNotes(NotesModel notesModel, long userId)
        {
            try
            {
                return iNotesRL.createNotes(notesModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NotesEntity> RetrieveNotesbyUserID(long userId)
        {
            try
            {
                return iNotesRL.RetrieveNotesbyUserID(userId);
            }
            catch (Exception )
            {

                throw;
            }
        }
        public IEnumerable<NotesEntity> RetrieveNotesbyNoteID(long userId, long noteId)
        {
            {
                try
                {
                    return iNotesRL.RetrieveNotesbyNoteID(userId, noteId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public bool DeleteNotes(int noteId)
        {
            try
            {
                if (iNotesRL.DeleteNotes(noteId))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public NotesEntity UpdateNotesData(long userId, long noteId, NotesModel notesModel)
        {
            try
            {
                return iNotesRL.UpdateNotesData(userId, noteId, notesModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool PinNotes(long noteId)
        {
            try
            {
                return iNotesRL.PinNotes(noteId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public bool ArchiveNotes(long noteId)
        {
            try
            {
                return iNotesRL.ArchiveNotes(noteId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
