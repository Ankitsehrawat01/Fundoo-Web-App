using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity CreateLabel(string label_Name, long noteId, long userId);
        public bool DeleteLabel(long labelId);
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId);
    }
}
