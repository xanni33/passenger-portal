/*using System.Text.Json.Serialization;
using Route = PassengerPortal.Shared.Models.Route;
using Microsoft.EntityFrameworkCore;
using PassengerPortal.Server.Data;
using PassengerPortal.Shared.Models;
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Server.Repositories;
using PassengerPortal.Server.Services;
using Microsoft.EntityFrameworkCore;
using PassengerPortal.Server.Data;
using PassengerPortal.Shared.Models;
using PassengerPortal.Server.Data;
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Server.Repositories;
using PassengerPortal.Server.Services;
using PassengerPortal.Server.Builders;
using PassengerPortal.Shared.Representations;
using PassengerPortal.Shared.Strategies;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<RankingService>();//new
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//new

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 32; 
        options.JsonSerializerOptions.WriteIndented = true; 
    });

// Dodajemy kontekst bazy danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Rejestracja repozytoriów
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketRepresentation, EmailRepresentation>();


builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); // rejestracja IUserRepository
builder.Services.AddScoped<ILoginService, LoginProxy>(); // rejestracja ILoginService

builder.Services.AddScoped<ITicketBuilder, TicketBuilder>();

builder.Services.AddScoped<ISearchStrategy, FewestTransfersStrategy>();

// Konfiguracja CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7148")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PassengerPortal API v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowFrontend");

app.UseRouting();

app.MapRazorPages();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Rozpoczynam migrację bazy danych...");
        context.Database.Migrate();

        if (!context.Stations.Any())
        {
            logger.LogInformation("Inicjalizowanie danych dla stacji...");
            context.Stations.AddRange(
                new Station { Name = "Jaslo", City = "Jaslo" },
                new Station { Name = "Krakow", City = "Krakow" },
                new Station { Name = "Warszawa", City = "Warszawa" }
            );
            context.SaveChanges();
            logger.LogInformation("Stacje zostały zainicjalizowane.");
        }

        if (!context.Routes.Any())
        {
            logger.LogInformation("Inicjalizowanie danych dla tras...");
            var stations = context.Stations.ToList();

            var jasloStation = stations.FirstOrDefault(s => s.Name == "Jaslo");
            var krakowStation = stations.FirstOrDefault(s => s.Name == "Krakow");
            var warszawaStation = stations.FirstOrDefault(s => s.Name == "Warszawa");

            if (jasloStation == null || krakowStation == null || warszawaStation == null)
            {
                logger.LogWarning("Nie można znaleźć wymaganych stacji: 'Jaslo', 'Krakow', 'Warszawa'.");
            }
            else
            {
                context.Routes.AddRange(
                    new Route
                    {
                        StartStation = jasloStation,
                        EndStation = krakowStation,
                        Duration = TimeSpan.FromHours(3),
                        TrainType = TrainType.InterCity,    
                        Price = 50.00m,                      
                        AvailableSeats = 100,               
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(6), // 6:00 rano
                                ArrivalTime = TimeSpan.FromHours(9)   // 9:00 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(15), // 15:00
                                ArrivalTime = TimeSpan.FromHours(18)   // 18:00
                            }
                        }
                    },
                    new Route
                    {
                        StartStation = krakowStation,
                        EndStation = warszawaStation,
                        Duration = TimeSpan.FromHours(2.5),
                        TrainType = TrainType.InterCity,    
                        Price = 70.00m,                      
                        AvailableSeats = 100,               
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(10), // 10:00 rano
                                ArrivalTime = TimeSpan.FromHours(12.5) // 12:30
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(20), // 20:00
                                ArrivalTime = TimeSpan.FromHours(22.5) // 22:30
                            }
                        }
                    }
                );
                context.SaveChanges();
                logger.LogInformation("Trasy zostały zainicjalizowane.");
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Wystąpił błąd podczas inicjalizacji bazy danych.");
        throw;
    }
}

app.Run();*/
using System.Text.Json.Serialization;
using Route = PassengerPortal.Shared.Models.Route;
using Microsoft.EntityFrameworkCore;
using PassengerPortal.Server.Data;
using PassengerPortal.Shared.Models;
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Server.Repositories;
using PassengerPortal.Server.Services;
using PassengerPortal.Server.Builders;
using PassengerPortal.Shared.Representations;
using PassengerPortal.Shared.Strategies;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<RankingService>(); //new
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//new

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 32;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Dodajemy kontekst bazy danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Rejestracja repozytoriów
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketRepresentation, EmailRepresentation>();

builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); // rejestracja IUserRepository
builder.Services.AddScoped<ILoginService, LoginProxy>(); // rejestracja ILoginService

builder.Services.AddScoped<ITicketBuilder, TicketBuilder>();

builder.Services.AddScoped<ISearchStrategy, FewestTransfersStrategy>();

// Konfiguracja CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7148")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PassengerPortal API v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowFrontend");

app.UseRouting();

app.MapRazorPages();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Rozpoczynam migrację bazy danych...");
        context.Database.Migrate();

        if (!context.Stations.Any())
        {
            logger.LogInformation("Inicjalizowanie danych dla stacji...");
            context.Stations.AddRange(
                new Station { Name = "Jaslo", City = "Jaslo" },
                new Station { Name = "Krakow", City = "Krakow" },
                new Station { Name = "Warszawa", City = "Warszawa" },
                new Station { Name = "Rzeszów", City = "Rzeszów" },     // Nowa stacja
                new Station { Name = "Olkusz", City = "Olkusz" },       // Nowa stacja
                new Station { Name = "Chełm", City = "Chełm" }          // Nowa stacja
            );
            context.SaveChanges();
            logger.LogInformation("Stacje zostały zainicjalizowane.");
        }

        if (!context.Routes.Any())
        {
            logger.LogInformation("Inicjalizowanie danych dla tras...");
            var stations = context.Stations.ToList();

            var jasloStation = stations.FirstOrDefault(s => s.Name == "Jaslo");
            var krakowStation = stations.FirstOrDefault(s => s.Name == "Krakow");
            var warszawaStation = stations.FirstOrDefault(s => s.Name == "Warszawa");
            var rzeszowStation = stations.FirstOrDefault(s => s.Name == "Rzeszów"); // Nowa stacja
            var olkuszStation = stations.FirstOrDefault(s => s.Name == "Olkusz");       // Nowa stacja
            var chelmStation = stations.FirstOrDefault(s => s.Name == "Chełm");          // Nowa stacja

            if (jasloStation == null || krakowStation == null || warszawaStation == null ||
                rzeszowStation == null || olkuszStation == null || chelmStation == null)
            {
                logger.LogWarning("Nie można znaleźć wszystkich wymaganych stacji.");
            }
            else
            {
                // Istniejące trasy
                context.Routes.AddRange(
                    new Route
                    {
                        StartStation = jasloStation,
                        EndStation = krakowStation,
                        Duration = TimeSpan.FromHours(3),
                        TrainType = TrainType.InterCity,
                        Price = 50.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(6), // 6:00 rano
                                ArrivalTime = TimeSpan.FromHours(9)   // 9:00 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(15), // 15:00
                                ArrivalTime = TimeSpan.FromHours(18)   // 18:00
                            }
                        }
                    },
                    new Route
                    {
                        StartStation = krakowStation,
                        EndStation = warszawaStation,
                        Duration = TimeSpan.FromHours(2.5),
                        TrainType = TrainType.InterCity,
                        Price = 70.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(10), // 10:00 rano
                                ArrivalTime = TimeSpan.FromHours(12.5) // 12:30
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(20), // 20:00
                                ArrivalTime = TimeSpan.FromHours(22.5) // 22:30
                            }
                        }
                    }
                );

                // Dodanie nowych tras
                context.Routes.AddRange(
                    // Warszawa <-> Chełm
                    new Route
                    {
                        StartStation = warszawaStation,
                        EndStation = chelmStation,
                        Duration = TimeSpan.FromHours(1.5),
                        TrainType = TrainType.InterCity,
                        Price = 30.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(7), // 7:00 rano
                                ArrivalTime = TimeSpan.FromHours(8.5) // 8:30 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(17), // 17:00
                                ArrivalTime = TimeSpan.FromHours(18.5) // 18:30
                            }
                        }
                    },
                    new Route
                    {
                        StartStation = chelmStation,
                        EndStation = warszawaStation,
                        Duration = TimeSpan.FromHours(1.5),
                        TrainType = TrainType.InterCity,
                        Price = 30.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(9), // 9:00 rano
                                ArrivalTime = TimeSpan.FromHours(10.5) // 10:30 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(19), // 19:00
                                ArrivalTime = TimeSpan.FromHours(20.5) // 20:30
                            }
                        }
                    },

                    // Kraków <-> Chełm
                    new Route
                    {
                        StartStation = krakowStation,
                        EndStation = chelmStation,
                        Duration = TimeSpan.FromHours(2),
                        TrainType = TrainType.InterCity,
                        Price = 40.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(8), // 8:00 rano
                                ArrivalTime = TimeSpan.FromHours(10) // 10:00 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(18), // 18:00
                                ArrivalTime = TimeSpan.FromHours(20) // 20:00
                            }
                        }
                    },
                    new Route
                    {
                        StartStation = chelmStation,
                        EndStation = krakowStation,
                        Duration = TimeSpan.FromHours(2),
                        TrainType = TrainType.InterCity,
                        Price = 40.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(11), // 11:00 rano
                                ArrivalTime = TimeSpan.FromHours(13) // 13:00
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(21), // 21:00
                                ArrivalTime = TimeSpan.FromHours(23) // 23:00
                            }
                        }
                    },

                    // Kraków <-> Jasło
                    new Route
                    {
                        StartStation = krakowStation,
                        EndStation = jasloStation,
                        Duration = TimeSpan.FromHours(3),
                        TrainType = TrainType.InterCity,
                        Price = 50.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(6), // 6:00 rano
                                ArrivalTime = TimeSpan.FromHours(9) // 9:00 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(15), // 15:00
                                ArrivalTime = TimeSpan.FromHours(18) // 18:00
                            }
                        }
                    },
                    new Route
                    {
                        StartStation = jasloStation,
                        EndStation = krakowStation,
                        Duration = TimeSpan.FromHours(3),
                        TrainType = TrainType.InterCity,
                        Price = 50.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(7), // 7:00 rano
                                ArrivalTime = TimeSpan.FromHours(10) // 10:00 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(16), // 16:00
                                ArrivalTime = TimeSpan.FromHours(19) // 19:00
                            }
                        }
                    },

                    // Kraków <-> Olkusz
                    new Route
                    {
                        StartStation = krakowStation,
                        EndStation = olkuszStation,
                        Duration = TimeSpan.FromHours(1),
                        TrainType = TrainType.InterCity,
                        Price = 20.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(5), // 5:00 rano
                                ArrivalTime = TimeSpan.FromHours(6) // 6:00 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(14), // 14:00
                                ArrivalTime = TimeSpan.FromHours(15) // 15:00
                            }
                        }
                    },
                    new Route
                    {
                        StartStation = olkuszStation,
                        EndStation = krakowStation,
                        Duration = TimeSpan.FromHours(1),
                        TrainType = TrainType.InterCity,
                        Price = 20.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(7), // 7:00 rano
                                ArrivalTime = TimeSpan.FromHours(8) // 8:00 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(17), // 17:00
                                ArrivalTime = TimeSpan.FromHours(18) // 18:00
                            }
                        }
                    },

                    // Jasło <-> Rzeszów
                    new Route
                    {
                        StartStation = jasloStation,
                        EndStation = rzeszowStation,
                        Duration = TimeSpan.FromHours(2.5),
                        TrainType = TrainType.InterCity,
                        Price = 35.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(8), // 8:00 rano
                                ArrivalTime = TimeSpan.FromHours(10.5) // 10:30 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(18), // 18:00
                                ArrivalTime = TimeSpan.FromHours(20.5) // 20:30
                            }
                        }
                    },
                    new Route
                    {
                        StartStation = rzeszowStation,
                        EndStation = jasloStation,
                        Duration = TimeSpan.FromHours(2.5),
                        TrainType = TrainType.InterCity,
                        Price = 35.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(9), // 9:00 rano
                                ArrivalTime = TimeSpan.FromHours(11.5) // 11:30 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(19), // 19:00
                                ArrivalTime = TimeSpan.FromHours(21.5) // 21:30
                            }
                        }
                    },

                    // Rzeszów <-> Kraków
                    new Route
                    {
                        StartStation = rzeszowStation,
                        EndStation = krakowStation,
                        Duration = TimeSpan.FromHours(2),
                        TrainType = TrainType.InterCity,
                        Price = 40.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(6), // 6:00 rano
                                ArrivalTime = TimeSpan.FromHours(8) // 8:00 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(16), // 16:00
                                ArrivalTime = TimeSpan.FromHours(18) // 18:00
                            }
                        }
                    },
                    new Route
                    {
                        StartStation = krakowStation,
                        EndStation = rzeszowStation,
                        Duration = TimeSpan.FromHours(2),
                        TrainType = TrainType.InterCity,
                        Price = 40.00m,
                        AvailableSeats = 100,
                        Timetables = new List<Timetable>
                        {
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(7), // 7:00 rano
                                ArrivalTime = TimeSpan.FromHours(9) // 9:00 rano
                            },
                            new Timetable
                            {
                                DepartureTime = TimeSpan.FromHours(17), // 17:00
                                ArrivalTime = TimeSpan.FromHours(19) // 19:00
                            }
                        }
                    }
                );

                context.SaveChanges();
                logger.LogInformation("Trasy zostały zainicjalizowane.");
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Wystąpił błąd podczas inicjalizacji bazy danych.");
        throw;
    }
}

app.Run();


