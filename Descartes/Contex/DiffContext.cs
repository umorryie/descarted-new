using Descartes.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Descartes.Contex
{
    public class DiffContext : DbContext
    {
        public DbSet<DifferenceObject> DifferenceObject { get; set; }

        public DiffContext(DbContextOptions<DiffContext> options)
        : base(options)
        {
        }
    }
}
