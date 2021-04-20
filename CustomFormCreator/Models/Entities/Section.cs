using CustomFormCreator.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomFormCreator.Models.Entities
{
    public class Section : EntityBase
    {
        public int? FormId { get; set; }
        [ForeignKey(nameof(FormId))]
        public Form FormNavigation { get; set; }

        public string Name { get; set; }
        public int? Order { get; set; }

        [InverseProperty(nameof(Column.SectionNavigation))]
        public IEnumerable<Column> Columns { get; set; } = new List<Column>();

    }
}
