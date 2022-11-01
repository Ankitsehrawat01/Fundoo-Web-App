using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabratorRL
    {
        public CollabratorEntity CreateCollabrator(CollabratorModel collabratorModel, long userId, long noteId);
    }
}
