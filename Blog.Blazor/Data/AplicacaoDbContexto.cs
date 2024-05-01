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
                new Usuario { Id = Guid.NewGuid(), Email = "dpelissari1", Tipo = Enums.TipoUsuario.Administrador, SenhaHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855" }
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
