using Microsoft.EntityFrameworkCore;
using PassengerPortal.Shared.Models;
using Route = PassengerPortal.Shared.Models.Route;

namespace PassengerPortal.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Delay> Delays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacja Route -> StartStation
            modelBuilder.Entity<Route>()
                .HasOne(r => r.StartStation)
                .WithMany(s => s.Routes)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Route -> EndStation
            modelBuilder.Entity<Route>()
                .HasOne(r => r.EndStation)
                .WithMany() // Brak kolekcji w EndStation
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Timetable -> Route
            modelBuilder.Entity<Timetable>()
                .HasOne(t => t.Route)
                .WithMany(r => r.Timetables)
                .HasForeignKey(t => t.RouteId)
                .OnDelete(DeleteBehavior.Cascade);


            // Relacja wiele-do-wielu między Ticket a Route
            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.Routes)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "TicketRoute",
                    tr => tr.HasOne<Route>().WithMany().HasForeignKey("RouteId"),
                    tr => tr.HasOne<Ticket>().WithMany().HasForeignKey("TicketId"),
                    tr =>
                    {
                        tr.HasKey("TicketId", "RouteId");
                        tr.ToTable("TicketRoutes");
                    });

            // Konfiguracja encji User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique(); // Login musi być unikalny

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Email również musi być unikalny

            //konfiguracja encji Delay
            modelBuilder.Entity<Delay>(entity =>
            {
                entity.ToTable("Delays"); // Nazwa tabeli w bazie danych
                entity.HasKey(d => d.Id); // Klucz główny

                entity.Property(d => d.TrainNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(d => d.Route)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(d => d.DepartureTime)
                    .IsRequired();

                entity.Property(d => d.DelayInMinutes)
                    .IsRequired();
            });

            
        }
    }
}
