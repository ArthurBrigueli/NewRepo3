using Microsoft.EntityFrameworkCore;
using pimfo.Models;

namespace pimfo.data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<Folha_pagamento> Folha_pagamento { get; set; }
        public DbSet<Desconto> Desconto { get; set;}

        public DbSet<Relatorio> Relatorio { get; set; }

        public DbSet<Login> Login { get; set; }

    }
}
