﻿using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public LabelEntity CreateLabel(string label_Name, long noteId, long userId)
        {
            try
            {
                var NoteResult = fundooContext.NotesTable.Where(x => x.NoteId == noteId).FirstOrDefault();

                if (NoteResult != null)
                {
                    LabelEntity labelEntityobj = new LabelEntity();
                    labelEntityobj.Label_Name = label_Name;
                    labelEntityobj.NoteId = NoteResult.NoteId;
                    labelEntityobj.UserId = userId;

                    fundooContext.LabelTable.Add(labelEntityobj);
                    fundooContext.SaveChanges();
                    return labelEntityobj;
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
        public bool DeleteLabel(long labelId)
        {
            try
            {
                var result = fundooContext.LabelTable.FirstOrDefault(x => x.LabelId == labelId);

                fundooContext.LabelTable.Remove(result);

                fundooContext.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}