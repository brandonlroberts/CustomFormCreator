using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomFormCreator.Models.Entities.Base
{
    public class EntityBase
    {
        public EntityBase()
        {
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Timestamp]
        public byte[] Rowversion { get; set; }
        [DisplayName("Created By")]
        public string CreatedBy { get; set; }
        [DisplayName("Date Created")]
        public DateTime Created { get; set; }
        [DisplayName("Modified By")]
        public string ModifiedBy { get; set; }
        [DisplayName("Date Modified ")]
        public DateTime Modified { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

    }
}
