using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace net_il_mio_fotoalbum.Models
{
    public class FotoContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Foto> Fotos { get; set; }

        public DbSet<Categoria> Categorie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = foto_db; Integrated Security = True; TrustServerCertificate=True");
        }
        
        public void Seed()
        {
            string path = "/img/";

            if (!Fotos.Any())
            {
                var seed = new Foto[]
                {



                new Foto
                {
                    Title =  "Barca",
                    Description = "Una barca persa in mare",
                    Url = path+"hd-wallpaper-3605547_1920.jpg",
                    Visible = false,
                    
                },

                new Foto
                {
                Title =  "Poopies",
                Description = " Fiori in giro ",
                Url =path+"poppies-174276_1920.jpg",
                Visible = false,
                
                },

                new Foto
                {
                Title = "Mare",
                Description = " Mare",
                Url = path+"sea-164989.jpg",
                Visible = false,
                },

                new Foto
                {
                Title = "Albero",
                Description = " Un albero immerso nel viola ",
                Url = path+"tree-736885.jpg",
                Visible = false,
                },

                   };

                Fotos.AddRange(seed);

            }

            if (!Categorie.Any())
            {
                var seed = new Categoria[]
                {
                        new()
                        {
                            Name = "Natura",
                        },
                        new()
                        {
                            Name = "Astratte"
                        },
                        new()
                        {
                            Name = "Città"
                        }
                };

                Categorie.AddRange(seed);

                
            }

            SaveChanges();
        }
    }
}
