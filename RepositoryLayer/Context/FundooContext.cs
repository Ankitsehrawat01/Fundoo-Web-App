using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<UserEntity> UserTable { get; set; }
        public DbSet<NotesEntity> NotesTable { get; set; }
        public DbSet<CollabratorEntity> collabratorTable { get; set; }
        public DbSet<LabelEntity> LabelTable { get; set; }
    }
}
