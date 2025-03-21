using NUnit.Framework;
using PassengerPortal.Shared.Models;

namespace TestProject1.SharedTests
{
    public class ModelsTests
    {
        [Test]
        public void Admin_ShouldHaveAdminRole()
        {
            // Arrange & Act
            var admin = new Admin();

            // Assert
            Assert.IsTrue(admin.Roles.Exists(role => role.RoleName == "Admin"));
        }
        
        
        [Test]
        public void Connection_TotalDuration_ShouldReturnCorrectDuration()
        {
            // Arrange
            var connection = new Connection
            {
                Routes = new List<Route>
                {
                    new Route { Duration = TimeSpan.FromMinutes(30) },
                    new Route { Duration = TimeSpan.FromMinutes(45) }
                }
            };

            // Act
            var totalDuration = connection.TotalDuration;

            // Assert
            Assert.AreEqual(TimeSpan.FromMinutes(75), totalDuration);
        }

        [Test]
        public void Connection_TotalPrice_ShouldReturnCorrectPrice()
        {
            // Arrange
            var connection = new Connection
            {
                Routes = new List<Route>
                {
                    new Route { Price = 50m },
                    new Route { Price = 75m }
                }
            };

            // Act
            var totalPrice = connection.TotalPrice;

            // Assert
            Assert.AreEqual(125m, totalPrice);
        }
        
        
        [Test]
        public void Delay_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var delay = new Delay
            {
                Id = 1,
                TrainNumber = "12345",
                Route = "Route1",
                DepartureTime = new DateTime(2023, 10, 1, 12, 0, 0),
                DelayInMinutes = 15
            };

            // Assert
            Assert.AreEqual(1, delay.Id);
            Assert.AreEqual("12345", delay.TrainNumber);
            Assert.AreEqual("Route1", delay.Route);
            Assert.AreEqual(new DateTime(2023, 10, 1, 12, 0, 0), delay.DepartureTime);
            Assert.AreEqual(15, delay.DelayInMinutes);
        }
        
        
        [Test]
        public void Passenger_ShouldHavePassengerRole()
        {
            // Arrange & Act
            var passenger = new Passenger();

            // Assert
            Assert.IsTrue(passenger.Roles.Exists(role => role.RoleName == "Passenger"));
        }
        
        
        [Test]
        public void Refund_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var ticket = new Ticket { Id = 1 };
            var passenger = new Passenger { Username = "testuser" };
            var refund = new Refund
            {
                Id = 1,
                Ticket = ticket,
                RefundAmount = 50m,
                RefundDate = new DateTime(2023, 10, 1),
                Reason = "Test Reason",
                RequestedBy = passenger
            };

            // Assert
            Assert.AreEqual(1, refund.Id);
            Assert.AreEqual(ticket, refund.Ticket);
            Assert.AreEqual(50m, refund.RefundAmount);
            Assert.AreEqual(new DateTime(2023, 10, 1), refund.RefundDate);
            Assert.AreEqual("Test Reason", refund.Reason);
            Assert.AreEqual(passenger, refund.RequestedBy);
        }
        
        
        [Test]
        public void Role_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var role = new Role
            {
                Id = 1,
                RoleName = "Admin"
            };

            // Assert
            Assert.AreEqual(1, role.Id);
            Assert.AreEqual("Admin", role.RoleName);
        }
        
        
        [Test]
        public void Route_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var startStation = new Station { Id = 1, Name = "Start", City = "CityA" };
            var endStation = new Station { Id = 2, Name = "End", City = "CityB" };
            var route = new Route
            {
                Id = 1,
                StartStation = startStation,
                EndStation = endStation,
                Duration = TimeSpan.FromHours(1),
                TrainType = TrainType.Express,
                AvailableSeats = 100,
                Price = 50m
            };

            // Assert
            Assert.AreEqual(1, route.Id);
            Assert.AreEqual(startStation, route.StartStation);
            Assert.AreEqual(endStation, route.EndStation);
            Assert.AreEqual(TimeSpan.FromHours(1), route.Duration);
            Assert.AreEqual(TrainType.Express, route.TrainType);
            Assert.AreEqual(100, route.AvailableSeats);
            Assert.AreEqual(50m, route.Price);
        }
        
        
        [Test]
        public void Station_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var station = new Station
            {
                Id = 1,
                Name = "Central",
                City = "Metropolis"
            };

            // Assert
            Assert.AreEqual(1, station.Id);
            Assert.AreEqual("Central", station.Name);
            Assert.AreEqual("Metropolis", station.City);
        }

        [Test]
        public void Station_ShouldInitializeRoutesList()
        {
            // Arrange & Act
            var station = new Station();

            // Assert
            Assert.IsNotNull(station.Routes);
            Assert.IsInstanceOf<List<Route>>(station.Routes);
        }
        
        
        [Test]
        public void Ticket_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var route = new Route { Id = 1 };
            var ticket = new Ticket
            {
                Id = 1,
                DepartureDateTime = new DateTime(2023, 10, 1, 12, 0, 0),
                Routes = new List<Route> { route },
                PurchaseTime = DateTime.Now,
                Price = 100m,
                BuyerId = "user1"
            };

            // Assert
            Assert.AreEqual(1, ticket.Id);
            Assert.AreEqual(new DateTime(2023, 10, 1, 12, 0, 0), ticket.DepartureDateTime);
            Assert.Contains(route, (System.Collections.ICollection)ticket.Routes);
            Assert.AreEqual(100m, ticket.Price);
            Assert.AreEqual("user1", ticket.BuyerId);
        }
        
        
        [Test]
        public void Timetable_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var route = new Route { Id = 1 };
            var timetable = new Timetable
            {
                Id = 1,
                RouteId = route.Id,
                Route = route,
                DepartureTime = new TimeSpan(8, 0, 0),
                ArrivalTime = new TimeSpan(10, 0, 0)
            };

            // Assert
            Assert.AreEqual(1, timetable.Id);
            Assert.AreEqual(route.Id, timetable.RouteId);
            Assert.AreEqual(route, timetable.Route);
            Assert.AreEqual(new TimeSpan(8, 0, 0), timetable.DepartureTime);
            Assert.AreEqual(new TimeSpan(10, 0, 0), timetable.ArrivalTime);
        }
        
        
        [Test]
        public void Train_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var route = new Route { Id = 1 };
            var train = new Train
            {
                Id = 1,
                TrainNumber = "12345",
                RoutesCovered = new List<Route> { route }
            };

            // Assert
            Assert.AreEqual(1, train.Id);
            Assert.AreEqual("12345", train.TrainNumber);
            Assert.Contains(route, (System.Collections.ICollection)train.RoutesCovered);
        }
        
        
        [Test]
        public void TrainRanking_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var trainRanking = new TrainRanking
            {
                TrainId = 1,
                Name = "Express",
                ImagePath = "/images/express.png",
                Votes = 100
            };

            // Assert
            Assert.AreEqual(1, trainRanking.TrainId);
            Assert.AreEqual("Express", trainRanking.Name);
            Assert.AreEqual("/images/express.png", trainRanking.ImagePath);
            Assert.AreEqual(100, trainRanking.Votes);
        }
        
        
        [Test]
        public void TrainType_ShouldContainAllValues()
        {
            // Arrange & Act
            var values = Enum.GetValues(typeof(TrainType));

            // Assert
            Assert.Contains(TrainType.Local, values);
            Assert.Contains(TrainType.Express, values);
            Assert.Contains(TrainType.InterCity, values);
            Assert.Contains(TrainType.Night, values);
        }
        
        
        [Test]
        public void Transaction_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var passenger = new Passenger { Username = "testuser" };
            var ticket = new Ticket { Id = 1 };
            var refund = new Refund { Id = 1 };
            var transaction = new Transaction
            {
                Id = 1,
                Passenger = passenger,
                Ticket = ticket,
                Refund = refund,
                Amount = 100m,
                TransactionDate = new DateTime(2023, 10, 1),
                TransactionType = "Purchase"
            };

            // Assert
            Assert.AreEqual(1, transaction.Id);
            Assert.AreEqual(passenger, transaction.Passenger);
            Assert.AreEqual(ticket, transaction.Ticket);
            Assert.AreEqual(refund, transaction.Refund);
            Assert.AreEqual(100m, transaction.Amount);
            Assert.AreEqual(new DateTime(2023, 10, 1), transaction.TransactionDate);
            Assert.AreEqual("Purchase", transaction.TransactionType);
        }

        [Test]
        public void Transaction_ShouldHandleNullProperties()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = 1,
                Passenger = null,
                Ticket = null,
                Refund = null,
                Amount = 0m,
                TransactionDate = DateTime.MinValue,
                TransactionType = null
            };

            // Assert
            Assert.AreEqual(1, transaction.Id);
            Assert.IsNull(transaction.Passenger);
            Assert.IsNull(transaction.Ticket);
            Assert.IsNull(transaction.Refund);
            Assert.AreEqual(0m, transaction.Amount);
            Assert.AreEqual(DateTime.MinValue, transaction.TransactionDate);
            Assert.IsNull(transaction.TransactionType);
        }
        
        
        [Test]
        public void User_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                Email = "testuser@example.com",
                Roles = new List<Role> { new Role { RoleName = "Passenger" } }
            };

            // Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("testuser", user.Username);
            Assert.AreEqual("testuser@example.com", user.Email);
            Assert.IsTrue(user.Roles.Exists(role => role.RoleName == "Passenger"));
        }
    }
}