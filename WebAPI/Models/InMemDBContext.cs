using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class InMemDBContext: DbContext
    {
        public InMemDBContext(DbContextOptions<InMemDBContext> options)
        : base(options) { }

        public DbSet<Models.Job> Job { get; set; }

    }
}
