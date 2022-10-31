using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;

        private readonly IConfiguration iconfiguration;
        public NotesRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }
        public NotesEntity createNotes(NotesModel notesModel, long userId)
        {
            try
            {
                NotesEntity notesEntityobj = new NotesEntity();
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId);
                if (result != null)
                {
                    notesEntityobj.UserId = userId;
                    notesEntityobj.Title = notesModel.Title;
                    notesEntityobj.Discription = notesModel.Discription;
                    notesEntityobj.Reminder = notesModel.Reminder;
                    notesEntityobj.Backgroundcolor = notesModel.Backgroundcolor;
                    notesEntityobj.Image = notesModel.Image;
                    notesEntityobj.Archive = notesModel.Archive;
                    notesEntityobj.Pin = notesModel.Pin;
                    notesEntityobj.Trash = notesModel.Trash;
                    notesEntityobj.Created = notesModel.Created;
                    notesEntityobj.Edited = notesModel.Edited;

                    fundooContext.NotesTable.Add(notesEntityobj);
                    fundooContext.SaveChanges();
                    return notesEntityobj;
                }
                else
                {
                    return null;
                }
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

                var result = fundooContext.NotesTable.Where(x => x.UserId == userId);

                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<NotesEntity> RetrieveNotesbyNoteID(long userId, long noteId)
        {
            try
            {

                var result = fundooContext.NotesTable.Where(x => x.NoteId == noteId);

                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteNotes(int noteId)
        {
            NotesEntity note = fundooContext.NotesTable.FirstOrDefault(e => e.NoteId == noteId);
            if (note != null)
            {
                fundooContext.NotesTable.Remove(note);
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public NotesEntity UpdateNotesData(long userId, long noteId, NotesModel notesModel)
        {
            try
            {
                var notesEntity = fundooContext.NotesTable.FirstOrDefault(e => e.NoteId == noteId);
                if (notesEntity != null)
                {
                    notesEntity.Title = notesModel.Title;
                    notesEntity.Discription = notesModel.Discription;
                    notesEntity.Archive = notesModel.Archive;
                    notesEntity.Backgroundcolor = notesModel.Backgroundcolor;
                    notesEntity.Pin = notesModel.Pin;
                    notesEntity.Reminder = notesModel.Reminder;
                    notesEntity.Trash = notesModel.Trash;
                    notesEntity.Created = notesModel.Created;
                    notesEntity.Edited = notesModel.Edited;

                    fundooContext.SaveChanges();
                    return notesEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool PinNotes(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId && x.UserId==userId);
                if (result.Pin == false)
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ArchiveNotes(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);

                if (result.Archive != true)
                {
                    result.Archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Archive = false;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool TrashNotes(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);

                if (result.Trash != true)
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return true;
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity ChangeBackgroundColor(long userId, long noteId, string Backgroundcolor)
        {
            try
            {
                var notesEntityobj = fundooContext.NotesTable.FirstOrDefault(e => e.NoteId == noteId);
                if (notesEntityobj != null)
                {
                    notesEntityobj.Backgroundcolor = Backgroundcolor;

                    fundooContext.SaveChanges();
                    return notesEntityobj;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw e;
            }


        }
    }
}
