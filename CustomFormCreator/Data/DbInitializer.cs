using CustomFormCreator.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CustomFormCreator.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Forms.ToList().Any())
            {
                return;   // DB has been seeded
            }

            var formTypes = new FormColumnType[]
            {
                new FormColumnType { IsActive = true, Name = "String"   , Type = "text"},
                new FormColumnType { IsActive = true, Name = "Int"      , Type = "number"},
                new FormColumnType { IsActive = true, Name = "Double"   , Type = "number"},
                new FormColumnType { IsActive = true, Name = "Bool"     , Type = "checkbox"},
                new FormColumnType { IsActive = true, Name = "DateTime" , Type = "datetime"},
                new FormColumnType { IsActive = true, Name = "Date" , Type = "date"},
                new FormColumnType { IsActive = true, Name = "Email" , Type = "email"},
                new FormColumnType { IsActive = true, Name = "Password" , Type = "password"},
                new FormColumnType { IsActive = true, Name = "Radio" , Type = "radio"},
                new FormColumnType { IsActive = true, Name = "Telephone" , Type = "tel"},
                new FormColumnType { IsActive = true, Name = "Time" , Type = "time"},
            };
            context.FormColumnTypes.AddRange(formTypes);
            context.SaveChanges();

            var forms = new Form[]
{
                new Form
                {
                    Name = "Demo Form",
                    FormSections = new List<FormSection>()
                    {
                        new FormSection
                        {
                            Name = "Personal",
                            Order = 0,
                            IsActive = true,
                            FormColumns = new List<FormColumn>()
                            {
                                new FormColumn { IsActive = true, Name = "First Name",  Order = 0 , FormColumnTypeId = 1},
                                new FormColumn { IsActive = true, Name = "Middle Name", Order = 1 , FormColumnTypeId = 1},
                                new FormColumn { IsActive = true, Name = "Last Name",   Order = 2 , FormColumnTypeId = 1},
                                new FormColumn { IsActive = true, Name = "DOB",         Order = 3 , FormColumnTypeId = 6},
                                new FormColumn { IsActive = true, Name = "Height",      Order = 4 , FormColumnTypeId = 3},
                                new FormColumn { IsActive = true, Name = "Weight",      Order = 5 , FormColumnTypeId = 3}
                            }
                        },
                        new FormSection
                        {
                            Name = "Address",
                            Order = 1,
                            IsActive = true,
                            FormColumns = new List<FormColumn>()
                            {
                                new FormColumn { IsActive = true, Name = "Address 1", Order = 0, FormColumnTypeId = 1 },
                                new FormColumn { IsActive = true, Name = "Address 2", Order = 1, FormColumnTypeId = 1 },
                                new FormColumn { IsActive = true, Name = "Address 3", Order = 2, FormColumnTypeId = 1 },
                                new FormColumn { IsActive = true, Name = "City",      Order = 3, FormColumnTypeId = 1 },
                                new FormColumn { IsActive = true, Name = "State",     Order = 4, FormColumnTypeId = 2 },
                                new FormColumn { IsActive = true, Name = "Zip",       Order = 5, FormColumnTypeId = 2 }
                            }
                        },
                    }
                },
            };
            context.Forms.AddRange(forms);
            context.SaveChanges();
        }
    }
}
