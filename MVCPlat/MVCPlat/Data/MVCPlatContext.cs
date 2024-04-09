using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCPlat.Models;

namespace MVCPlat.Data
{
    public class MVCPlatContext : DbContext
    {
        public MVCPlatContext (DbContextOptions<MVCPlatContext> options)
            : base(options)
        {
        }

        public DbSet<MVCPlat.Models.Cosplays> Cosplay { get; set; } = default!;
    }
}
