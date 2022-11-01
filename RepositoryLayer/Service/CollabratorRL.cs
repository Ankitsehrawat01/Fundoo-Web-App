using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabratorRL: ICollabratorRL
    {
        private readonly FundooContext fundooContext;

        public CollabratorRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public CollabratorEntity CreateCollabrator(string email, long noteId)
        {
            try
            {
                var NoteResult = fundooContext.NotesTable.Where(x => x.NoteId == noteId).FirstOrDefault();
                var EmailResult = fundooContext.UserTable.Where(x => x.Email == email).FirstOrDefault();

                if (EmailResult != null && NoteResult != null)
                {
                    CollabratorEntity collabratorEntityobj = new CollabratorEntity();
                    collabratorEntityobj.Email = EmailResult.Email;
                    collabratorEntityobj.NoteId = NoteResult.NoteId;
                    collabratorEntityobj.UserId = EmailResult.UserId;

                    fundooContext.Add(collabratorEntityobj);
                    fundooContext.SaveChanges();
                    return collabratorEntityobj;
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
    }
}
