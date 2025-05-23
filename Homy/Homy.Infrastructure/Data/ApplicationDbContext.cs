using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homy.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> villaNumbers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber
                {
                    Villa_Number = 101,
                    VillaId = 1
                },
                                new VillaNumber
                                {
                                    Villa_Number = 102,
                                    VillaId = 1
                                },
                                                new VillaNumber
                                                {
                                                    Villa_Number = 103,
                                                    VillaId = 1
                                                },
                                                                new VillaNumber
                                                                {
                                                                    Villa_Number = 201,
                                                                    VillaId = 2
                                                                },
                                new VillaNumber
                                {
                                    Villa_Number = 202,
                                    VillaId = 2
                                },
                                                new VillaNumber
                                                {
                                                    Villa_Number = 203,
                                                    VillaId = 2
                                                },
                                                                                                                new VillaNumber
                                                                                                                {
                                                                                                                    Villa_Number = 301,
                                                                                                                    VillaId = 3
                                                                                                                },
                                new VillaNumber
                                {
                                    Villa_Number = 302,
                                    VillaId = 3
                                },
                                                new VillaNumber
                                                {
                                                    Villa_Number = 303,
                                                    VillaId = 3
                                                }




                );
        }
    }
}
