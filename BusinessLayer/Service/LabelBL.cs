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
        public bool DeleteLabel(long labelId)
        {
            try
            {
                return iLabelRL.DeleteLabel(labelId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId)
        {
            try
            {
                return iLabelRL.RetrieveLabel(labelId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public LabelEntity UpdateLabel(string label_Name, long labelId)
        {
            try
            {
                return iLabelRL.UpdateLabel(label_Name, labelId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
