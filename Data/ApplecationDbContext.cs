using GameZone.Models;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {      
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // add composite key to the table  GameDevice
            modelBuilder.Entity<GameDevice>().HasKey(x => new { x.GameId, x.DeviceId });

            modelBuilder.Entity<Category>().HasData(new Category[]
                {
                    new Category {id = 1 ,Name = "Sports" },
                    new Category {id = 2 ,Name = "Action" },
                    new Category {id = 3 ,Name = "Adventure" },
                    new Category {id = 4 ,Name = "Racing" },
                    new Category {id = 5 ,Name = "Fighting" },
                    new Category {id = 6 ,Name = "Film" }
                });

            modelBuilder.Entity<Device>().HasData(new Device[]
            {
                new Device {id = 1 , Name ="PlayStation" , Icon ="bi bi-playstation"},
                new Device {id = 2 , Name ="Xbox" , Icon ="bi bi-xbox"},
                new Device {id = 3 , Name ="Nintendo Switch" , Icon ="bi bi-nintendo-switch"},
                new Device {id = 4 , Name ="PC" , Icon ="bi bi-pc-display"}
            });

        }
    }
}
