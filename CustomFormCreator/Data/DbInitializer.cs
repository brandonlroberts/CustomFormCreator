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

            var forms = new Form[]
            {
                new Form
                {
                    Name = "Roberts Death Cert.",
                    Sections = new List<Section>()
                    {
                        new Section
                        {
                            Name = "Personal",
                            Order = 0,
                            Columns = new List<Column>()
                            {
                                new Column { Name = "First Name", Order = 0 },
                                new Column { Name = "Middle Name", Order = 0 },
                                new Column { Name = "Last Name", Order = 0 }
                            }
                        },
                        new Section
                        {
                            Name = "Address",
                            Order = 1,
                            Columns = new List<Column>()
                            {
                                new Column { Name = "Address 1", Order = 0, ColumnType = "String" },
                                new Column { Name = "Address 2", Order = 0, ColumnType = "String" },
                                new Column { Name = "Address 3", Order = 0, ColumnType = "String" }
                            }
                        },
                    }
                },
            };
            foreach (Form f in forms)
            {
                context.Forms.Add(f);
            }
            context.SaveChanges();
        }
    }
}
