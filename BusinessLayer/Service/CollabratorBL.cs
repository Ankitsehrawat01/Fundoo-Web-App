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
    public class CollabratorBL : ICollabratorBL
    {
        private readonly ICollabratorRL iCollabratorRL;
        public CollabratorBL(ICollabratorRL iCollabratorRL)
        {
            this.iCollabratorRL = iCollabratorRL;
        }
        public CollabratorEntity CreateCollabrator(string email, long noteId)
        {
            try
            {
                return iCollabratorRL.CreateCollabrator(email, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteCollabrator(long collabratorId, long noteId)
        {
            try
            {
                return iCollabratorRL.DeleteCollabrator(collabratorId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
