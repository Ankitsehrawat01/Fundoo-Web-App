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
    }
}
