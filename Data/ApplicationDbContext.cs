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
        public DbSet<PedidoProduto> PedidoProdutos { get; set; }
        public DbSet<PedidoServico> PedidoServicos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<OrdemCompra> OrdensCompra { get; set; }
        public DbSet<EntradaProduto> EntradasProduto { get; set; }
        public DbSet<SaidaProduto> SaidasProduto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>()
                .Property(p => p.PrecoUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Servico>()
                .Property(s => s.Preco)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.Property(f => f.InscricaoEstadual)
                    .HasDefaultValue(string.Empty);

                entity.Property(f => f.InscricaoMunicipal)
                    .HasDefaultValue(string.Empty);
            });

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Departamento)
                .WithMany()
                .HasForeignKey(u => u.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasDiscriminator<string>("PedidoType")
                .HasValue<PedidoProduto>("Produto")
                .HasValue<PedidoServico>("Servico");

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
                .HasOne(p => p.OrdemCompra)
                .WithMany(o => o.Pedidos)
                .HasForeignKey(p => p.OrdemCompraId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PedidoProduto>()
                .HasOne(p => p.Produto)
                .WithMany()
                .HasForeignKey(p => p.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PedidoServico>()
                .HasOne(p => p.Servico)
                .WithMany()
                .HasForeignKey(p => p.ServicoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PedidoServico>()
                .HasOne(p => p.Fornecedor)
                .WithMany()
                .HasForeignKey(p => p.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar precisão das datas
            modelBuilder.Entity<OrdemCompra>()
                .Property(o => o.DataCadastro)
                .HasColumnType("datetime2(0)");

            modelBuilder.Entity<EntradaProduto>()
                .Property(e => e.DataCadastro)
                .HasColumnType("datetime2(0)");

            modelBuilder.Entity<SaidaProduto>()
                .Property(s => s.DataCadastro)
                .HasColumnType("datetime2(0)");

            modelBuilder.Entity<Pedido>()
                .Property(p => p.DataCadastro)
                .HasColumnType("datetime2(0)");
        }
    }
}
