using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CollabratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabratorId { get; set; }
        public string Email { get; set; }

        [ForeignKey("User, Notes")]
        public long UserId { get; set; }
        public long NoteId { get; set; }

        [JsonIgnore]
        public virtual UserEntity User { get; set; }
        public virtual NotesEntity Notes { get; set; }
    }
}
