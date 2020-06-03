using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new InMemDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<InMemDBContext>>()))
            {
                // Look for any Job.
                if (context.Job.Any())
                {
                    return;   // Data was already seeded
                }
                //Adding Jobs to the Inmemory Dataset
                context.Job.AddRange(
                    new Job
                    {
                        IdJob = 1,
                        JobName = "Architect",
                        JobTitle = "Data Architect Coordinator",
                        JobDescription = "Define The RoadMap of SaonGroup for DB and Analytics",
                        CreatedAt = DateTime.Now,
                        ExpiresAt = DateTime.Now.AddDays(60),
                       
                    },
                      new Job
                      {
                          IdJob = 2,
                          JobName = "Developer",
                          JobTitle = "Developer Leader",
                          JobDescription = "Coordinate the team of developers",
                          CreatedAt = DateTime.Now,
                          ExpiresAt = DateTime.Now.AddDays(60),

                      },
                        new Job
                        {
                            IdJob = 3,
                            JobName = "Developer",
                            JobTitle = "Developer",
                            JobDescription = "Develop Software that add value to the business",
                            CreatedAt = DateTime.Now,
                            ExpiresAt = DateTime.Now.AddDays(60),

                        },
                          new Job
                          {
                              IdJob = 4,
                              JobName = "BA",
                              JobTitle = "Business Analyst",
                              JobDescription = "Take the business requirements and ensure that are met ",
                              CreatedAt = DateTime.Now,
                              ExpiresAt = DateTime.Now.AddDays(60),

                          },
                            new Job
                            {
                                IdJob = 5,
                                JobName = "Manager",
                                JobTitle = "Software Manager",
                                JobDescription = "Ensure that the team achive the goals of the area",
                                CreatedAt = DateTime.Now,
                                ExpiresAt = DateTime.Now.AddDays(60),

                            });

                context.SaveChanges();
            }
        }
    }
}
