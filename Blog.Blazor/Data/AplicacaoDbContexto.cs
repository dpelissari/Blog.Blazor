using Blog.Blazor.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Blazor.Data
{
    public class AplicacaoDbContexto : DbContext
    {
        public AplicacaoDbContexto(DbContextOptions<AplicacaoDbContexto> options) : base(options) { }

        public DbSet<Post>? Post { get; set; }
        public DbSet<Autor>? Autor { get; set; }
        public DbSet<Categoria>? Categoria { get; set; }
        public DbSet<Usuario>? Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // mapeia propriedades
            modelBuilder.Entity<Post>().HasKey(m => m.Id);
            modelBuilder.Entity<Autor>().HasKey(m => m.Id);
            modelBuilder.Entity<Categoria>().HasKey(m => m.Id);

            // configura a relação onde um autor pode ter varios posts e um post um unico autor
            modelBuilder.Entity<Autor>()
            .HasMany(g => g.Posts)
            .WithOne(s => s.Autor)
            .HasForeignKey(s => s.IdAutor)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = Guid.NewGuid(), Email = "root", Tipo = Enums.TipoUsuario.Administrador, SenhaHash = "0ee484c7d01b93fe9ff6dec76671dda6e08d5d8c8a052f83db29fa898b8bacb1" }
            );

            // configura a relação onde uma categoria pode pertencer a varios posts, mas um post só pode ter uma categoria
            modelBuilder.Entity<Categoria>()
            .HasMany(c => c.Posts)
            .WithOne(f => f.Categoria)
            .HasForeignKey(f => f.IdCategoria)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
