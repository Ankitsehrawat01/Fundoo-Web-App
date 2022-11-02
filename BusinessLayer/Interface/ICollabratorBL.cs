using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabratorBL
    {
        public CollabratorEntity CreateCollabrator(string email, long noteId);
        public bool DeleteCollabrator(long collabratorId, long noteId);
        public IEnumerable<CollabratorEntity> RetrieveCollabrator(long noteId);
    }
}
