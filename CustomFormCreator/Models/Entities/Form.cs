using CustomFormCreator.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomFormCreator.Models.Entities
{
    public class Form : EntityBase
    {
        public string Name { get; set; }

        [InverseProperty(nameof(Section.FormNavigation))]
        public IEnumerable<Section> Sections { get; set; } = new List<Section>();
    }
}
