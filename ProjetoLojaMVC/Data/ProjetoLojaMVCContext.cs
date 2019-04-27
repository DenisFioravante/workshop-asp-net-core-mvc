using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoLojaMVC.Models
{
    public class ProjetoLojaMVCContext : DbContext
    {
        public ProjetoLojaMVCContext (DbContextOptions<ProjetoLojaMVCContext> options)
            : base(options)
        {
        }

        //SEMPRE QUE CRIAR NOVOS MODELOS
        //ADICIONAR DbSet
        public DbSet<Departament> Departament { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
