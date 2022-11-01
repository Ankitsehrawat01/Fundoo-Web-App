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
        public CollabratorEntity CreateCollabrator(CollabratorModel collabratorModel, long userId, long noteId)
        {
            try
            {
                return iCollabratorRL.CreateCollabrator(collabratorModel, userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
