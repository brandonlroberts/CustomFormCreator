using CustomFormCreator.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomFormCreator.Models.Entities
{
    public class FormSection : EntityBase
    {
        [Display(Name="Form Name")]
        public int? FormId { get; set; }
        [ForeignKey(nameof(FormId))]
        public Form FormNavigation { get; set; }

        public string Name { get; set; }

        public int? Order { get; set; }

        [InverseProperty(nameof(FormColumn.SectionNavigation))]
        public List<FormColumn> FormColumns { get; set; } = new List<FormColumn>();

        [NotMapped]
        public string FormSectionName => $"Form: {FormNavigation.Name} - Section: {Name}"; 

    }
}
