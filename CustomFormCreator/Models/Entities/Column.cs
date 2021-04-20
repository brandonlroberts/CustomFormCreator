using CustomFormCreator.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomFormCreator.Models.Entities
{
    public class Column : EntityBase
    {
        public int? SectionId { get; set; }
        [ForeignKey(nameof(SectionId))]
        public Section SectionNavigation { get; set; }

        public string Name { get; set; }
        public int? Order { get; set; }
        public string ColumnType { get; set; }
    }
}
