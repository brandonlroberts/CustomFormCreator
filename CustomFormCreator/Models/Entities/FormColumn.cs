using CustomFormCreator.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomFormCreator.Models.Entities
{
    public class FormColumn : EntityBase
    {
        public int? SectionId { get; set; }
        [ForeignKey(nameof(SectionId))]
        public FormSection SectionNavigation { get; set; }

        public string Name { get; set; }
        public int? Order { get; set; }

        public int? FormColumnTypeId { get; set; }
        [ForeignKey(nameof(FormColumnTypeId))]
        public FormColumnType FormColumnTypeNavigation { get; set; }

        public string Data { get; set; }
    }
}
