using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL iLabelRL;
        public LabelBL(ILabelRL iLabelRL)
        {
            this.iLabelRL = iLabelRL;
        }
        public LabelEntity CreateLabel(string label_Name, long noteId, long userId)
        {
            try
            {
                return iLabelRL.CreateLabel(label_Name, noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
