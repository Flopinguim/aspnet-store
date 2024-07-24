using aspnet_store.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace aspnet_store.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<OrdemCompra> OrdensCompra { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>()
                .Property(p => p.PrecoUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.Property(f => f.InscricaoEstadual)
                    .HasDefaultValue(string.Empty);

                entity.Property(f => f.InscricaoMunicipal)
                    .HasDefaultValue(string.Empty);
            });

            // Configuração para Usuario
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Departamento)
                .WithMany()
                .HasForeignKey(u => u.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração para Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.UsuarioSolicitante)
                .WithMany()
                .HasForeignKey(p => p.UsuarioSolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Departamento)
                .WithMany()
                .HasForeignKey(p => p.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Produto)
                .WithMany()
                .HasForeignKey(p => p.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}