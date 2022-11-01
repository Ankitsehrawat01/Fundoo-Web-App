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

        private readonly IConfiguration iconfiguration;
        public CollabratorRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }
        public CollabratorEntity CreateCollabrator(CollabratorModel collabratorModel,long userId ,long noteId)
        {
            try
            {
                CollabratorEntity collabratorEntityobj = new CollabratorEntity();
                var result = fundooContext.collabratorTable.Where(x => x.UserId == userId && x.NoteId==noteId);
                if (result != null)
                {
                    collabratorEntityobj.NoteId = noteId;
                    collabratorEntityobj.UserId = userId;   
                    collabratorEntityobj.Email = collabratorModel.Email;
                    fundooContext.collabratorTable.Add(collabratorEntityobj);
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
