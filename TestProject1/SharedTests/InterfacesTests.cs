using NUnit.Framework;
using Moq;
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1.SharedTests
{
    public class InterfacesTests
    {
        [Test]
        public void IConnectionFilter_Apply_ShouldFilterConnections()
        {
            // Arrange
            var mockConnectionFilter = new Mock<IConnectionFilter>();
            var connections = new List<Connection>
            {
                new Connection { Id = 1 },
                new Connection { Id = 2 }
            };
            var filteredConnections = connections.Where(c => c.Id == 1);

            mockConnectionFilter.Setup(cf => cf.Apply(connections)).Returns(filteredConnections);

            // Act
            var result = mockConnectionFilter.Object.Apply(connections);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }
        
        
        [Test]
        public void IDiscountStrategy_ApplyDiscount_ShouldReturnDiscountedPrice()
        {
            // Arrange
            var mockDiscountStrategy = new Mock<IDiscountStrategy>();
            var basePrice = 100m;
            var discountedPrice = 80m;

            mockDiscountStrategy.Setup(ds => ds.ApplyDiscount(basePrice)).Returns(discountedPrice);

            // Act
            var result = mockDiscountStrategy.Object.ApplyDiscount(basePrice);

            // Assert
            Assert.AreEqual(discountedPrice, result);
        }
        
        
        [Test]
        public void ILoginService_Authenticate_ShouldReturnTrueForValidCredentials()
        {
            // Arrange
            var mockLoginService = new Mock<ILoginService>();
            var username = "testuser";
            var password = "password";

            mockLoginService.Setup(ls => ls.Authenticate(username, password)).Returns(true);

            // Act
            var result = mockLoginService.Object.Authenticate(username, password);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ILoginService_Authenticate_ShouldReturnFalseForInvalidCredentials()
        {
            // Arrange
            var mockLoginService = new Mock<ILoginService>();
            var username = "testuser";
            var password = "wrongpassword";

            mockLoginService.Setup(ls => ls.Authenticate(username, password)).Returns(false);

            // Act
            var result = mockLoginService.Object.Authenticate(username, password);

            // Assert
            Assert.IsFalse(result);
        }
        
        
        [Test]
        public void IObserver_Update_ShouldBeCalled()
        {
            // Arrange
            var mockObserver = new Mock<IObserver>();

            // Act
            mockObserver.Object.Update();

            // Assert
            mockObserver.Verify(o => o.Update(), Times.Once);
        }
        
        
        [Test]
        public void IRankingObserver_UpdateRanking_ShouldBeCalledWithCorrectParameters()
        {
            // Arrange
            var mockRankingObserver = new Mock<IRankingObserver>();
            var rankings = new List<TrainRanking>
            {
                new TrainRanking { TrainId = 1, Votes = 1 },
                new TrainRanking { TrainId = 2, Votes = 2 }
            };

            // Act
            mockRankingObserver.Object.UpdateRanking(rankings);

            // Assert
            mockRankingObserver.Verify(ro => ro.UpdateRanking(rankings), Times.Once);
        }
        
        
        [Test]
        public void IRankingSubject_Attach_ShouldAddObserver()
        {
            // Arrange
            var mockRankingSubject = new Mock<IRankingSubject>();
            var mockObserver = new Mock<IRankingObserver>();

            // Act
            mockRankingSubject.Object.Attach(mockObserver.Object);

            // Assert
            mockRankingSubject.Verify(rs => rs.Attach(mockObserver.Object), Times.Once);
        }

        [Test]
        public void IRankingSubject_Detach_ShouldRemoveObserver()
        {
            // Arrange
            var mockRankingSubject = new Mock<IRankingSubject>();
            var mockObserver = new Mock<IRankingObserver>();

            // Act
            mockRankingSubject.Object.Detach(mockObserver.Object);

            // Assert
            mockRankingSubject.Verify(rs => rs.Detach(mockObserver.Object), Times.Once);
        }

        [Test]
        public void IRankingSubject_Notify_ShouldNotifyAllObservers()
        {
            // Arrange
            var mockRankingSubject = new Mock<IRankingSubject>();

            // Act
            mockRankingSubject.Object.Notify();

            // Assert
            mockRankingSubject.Verify(rs => rs.Notify(), Times.Once);
        }
        
        
        [Test]
        public void IRouteRepository_GetById_ShouldReturnRoute()
        {
            // Arrange
            var mockRouteRepository = new Mock<IRouteRepository>();
            var route = new Route { Id = 1 };

            mockRouteRepository.Setup(rr => rr.GetById(1)).Returns(route);

            // Act
            var result = mockRouteRepository.Object.GetById(1);

            // Assert
            Assert.AreEqual(route, result);
        }

        [Test]
        public void IRouteRepository_GetAll_ShouldReturnAllRoutes()
        {
            // Arrange
            var mockRouteRepository = new Mock<IRouteRepository>();
            var routes = new List<Route>
            {
                new Route { Id = 1 },
                new Route { Id = 2 }
            };

            mockRouteRepository.Setup(rr => rr.GetAll()).Returns(routes);

            // Act
            var result = mockRouteRepository.Object.GetAll();

            // Assert
            Assert.AreEqual(routes, result);
        }

        [Test]
        public void IRouteRepository_GetRoutesFromStation_ShouldReturnRoutes()
        {
            // Arrange
            var mockRouteRepository = new Mock<IRouteRepository>();
            var routes = new List<Route>
            {
                new Route { Id = 1, StartStation = new Station { Id = 1 } },
                new Route { Id = 2, StartStation = new Station { Id = 1 } }
            };

            mockRouteRepository.Setup(rr => rr.GetRoutesFromStation(1)).Returns(routes);

            // Act
            var result = mockRouteRepository.Object.GetRoutesFromStation(1);

            // Assert
            Assert.AreEqual(routes, result);
        }

        [Test]
        public void IRouteRepository_GetRoutesBetweenStations_ShouldReturnRoutes()
        {
            // Arrange
            var mockRouteRepository = new Mock<IRouteRepository>();
            var routes = new List<Route>
            {
                new Route { Id = 1, StartStation = new Station {Id = 1}, EndStation = new Station { Id = 2 } },
                new Route { Id = 2, StartStation = new Station {Id = 1}, EndStation = new Station { Id = 2 }  }
            };

            mockRouteRepository.Setup(rr => rr.GetRoutesBetweenStations(1, 2)).Returns(routes);

            // Act
            var result = mockRouteRepository.Object.GetRoutesBetweenStations(1, 2);

            // Assert
            Assert.AreEqual(routes, result);
        }

        [Test]
        public void IRouteRepository_Update_ShouldUpdateRoute()
        {
            // Arrange
            var mockRouteRepository = new Mock<IRouteRepository>();
            var route = new Route { Id = 1 };

            // Act
            mockRouteRepository.Object.Update(route);

            // Assert
            mockRouteRepository.Verify(rr => rr.Update(route), Times.Once);
        }

        [Test]
        public void IRouteRepository_Save_ShouldSaveChanges()
        {
            // Arrange
            var mockRouteRepository = new Mock<IRouteRepository>();

            // Act
            mockRouteRepository.Object.Save();

            // Assert
            mockRouteRepository.Verify(rr => rr.Save(), Times.Once);
        }
        
        
        [Test]
        public void ISearchStrategy_SearchConnections_ShouldReturnConnections()
        {
            // Arrange
            var mockSearchStrategy = new Mock<ISearchStrategy>();
            var startStation = new Station { Id = 1, Name = "Start" };
            var endStation = new Station { Id = 2, Name = "End" };
            var departureTime = DateTime.Now;
            var connections = new List<Connection>
            {
                new Connection { Id = 1 },
                new Connection { Id = 2 }
            };

            mockSearchStrategy.Setup(ss => ss.SearchConnections(startStation, endStation, departureTime, 5, null))
                .Returns(connections);

            // Act
            var result = mockSearchStrategy.Object.SearchConnections(startStation, endStation, departureTime);

            // Assert
            Assert.AreEqual(connections, result);
        }
        
        
        [Test]
        public void IStationRepository_GetById_ShouldReturnStation()
        {
            // Arrange
            var mockStationRepository = new Mock<IStationRepository>();
            var station = new Station { Id = 1, Name = "Station1" };

            mockStationRepository.Setup(sr => sr.GetById(1)).Returns(station);

            // Act
            var result = mockStationRepository.Object.GetById(1);

            // Assert
            Assert.AreEqual(station, result);
        }

        [Test]
        public void IStationRepository_GetAll_ShouldReturnAllStations()
        {
            // Arrange
            var mockStationRepository = new Mock<IStationRepository>();
            var stations = new List<Station>
            {
                new Station { Id = 1, Name = "Station1" },
                new Station { Id = 2, Name = "Station2" }
            };

            mockStationRepository.Setup(sr => sr.GetAll()).Returns(stations);

            // Act
            var result = mockStationRepository.Object.GetAll();

            // Assert
            Assert.AreEqual(stations, result);
        }

        [Test]
        public void IStationRepository_GetByName_ShouldReturnStation()
        {
            // Arrange
            var mockStationRepository = new Mock<IStationRepository>();
            var station = new Station { Id = 1, Name = "Station1" };

            mockStationRepository.Setup(sr => sr.GetByName("Station1")).Returns(station);

            // Act
            var result = mockStationRepository.Object.GetByName("Station1");

            // Assert
            Assert.AreEqual(station, result);
        }
        
        
        [Test]
        public void ISubject_Attach_ShouldAddObserver()
        {
            // Arrange
            var mockSubject = new Mock<ISubject>();
            var mockObserver = new Mock<IObserver>();

            // Act
            mockSubject.Object.Attach(mockObserver.Object);

            // Assert
            mockSubject.Verify(s => s.Attach(mockObserver.Object), Times.Once);
        }

        [Test]
        public void ISubject_Detach_ShouldRemoveObserver()
        {
            // Arrange
            var mockSubject = new Mock<ISubject>();
            var mockObserver = new Mock<IObserver>();

            // Act
            mockSubject.Object.Detach(mockObserver.Object);

            // Assert
            mockSubject.Verify(s => s.Detach(mockObserver.Object), Times.Once);
        }

        [Test]
        public void ISubject_Notify_ShouldNotifyAllObservers()
        {
            // Arrange
            var mockSubject = new Mock<ISubject>();

            // Act
            mockSubject.Object.Notify();

            // Assert
            mockSubject.Verify(s => s.Notify(), Times.Once);
        }
        
        
        [Test]
        public void ITicketBuilder_SetPassenger_ShouldSetPassenger()
        {
            // Arrange
            var mockTicketBuilder = new Mock<ITicketBuilder>();
            var passenger = new Passenger { Id = 1 };

            // Act
            mockTicketBuilder.Object.SetPassenger(passenger);

            // Assert
            mockTicketBuilder.Verify(tb => tb.SetPassenger(passenger), Times.Once);
        }

        [Test]
        public void ITicketBuilder_SetConnection_ShouldSetConnection()
        {
            // Arrange
            var mockTicketBuilder = new Mock<ITicketBuilder>();
            var connection = new Connection { Id = 1 };

            // Act
            mockTicketBuilder.Object.SetConnection(connection);

            // Assert
            mockTicketBuilder.Verify(tb => tb.SetConnection(connection), Times.Once);
        }

        [Test]
        public void ITicketBuilder_SetDiscountStrategy_ShouldSetDiscountStrategy()
        {
            // Arrange
            var mockTicketBuilder = new Mock<ITicketBuilder>();
            var discountStrategy = new Mock<IDiscountStrategy>().Object;

            // Act
            mockTicketBuilder.Object.SetDiscountStrategy(discountStrategy);

            // Assert
            mockTicketBuilder.Verify(tb => tb.SetDiscountStrategy(discountStrategy), Times.Once);
        }

        [Test]
        public void ITicketBuilder_SetRepresentation_ShouldSetRepresentation()
        {
            // Arrange
            var mockTicketBuilder = new Mock<ITicketBuilder>();
            var representation = new Mock<ITicketRepresentation>().Object;

            // Act
            mockTicketBuilder.Object.SetRepresentation(representation);

            // Assert
            mockTicketBuilder.Verify(tb => tb.SetRepresentation(representation), Times.Once);
        }

        [Test]
        public void ITicketBuilder_SetDepartureDateTime_ShouldSetDepartureDateTime()
        {
            // Arrange
            var mockTicketBuilder = new Mock<ITicketBuilder>();
            var departureDateTime = DateTime.Now;

            // Act
            mockTicketBuilder.Object.SetDepartureDateTime(departureDateTime);

            // Assert
            mockTicketBuilder.Verify(tb => tb.SetDepartureDateTime(departureDateTime), Times.Once);
        }

        [Test]
        public void ITicketBuilder_Build_ShouldReturnTicket()
        {
            // Arrange
            var mockTicketBuilder = new Mock<ITicketBuilder>();
            var ticket = new Ticket { Id = 1 };

            mockTicketBuilder.Setup(tb => tb.Build()).Returns(ticket);

            // Act
            var result = mockTicketBuilder.Object.Build();

            // Assert
            Assert.AreEqual(ticket, result);
        }
        
        
        [Test]
        public void ITicketRenderer_Render_ShouldRenderTicket()
        {
            // Arrange
            var mockTicketRenderer = new Mock<ITicketRenderer>();
            var ticket = new Ticket { Id = 1 };

            // Act
            mockTicketRenderer.Object.Render(ticket);

            // Assert
            mockTicketRenderer.Verify(tr => tr.Render(ticket), Times.Once);
        }
        
        
        [Test]
        public void ITicketRepository_Add_ShouldAddTicket()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var ticket = new Ticket { Id = 1 };

            // Act
            mockTicketRepository.Object.Add(ticket);

            // Assert
            mockTicketRepository.Verify(tr => tr.Add(ticket), Times.Once);
        }

        [Test]
        public void ITicketRepository_Delete_ShouldDeleteTicket()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var ticket = new Ticket { Id = 1 };

            // Act
            mockTicketRepository.Object.Delete(ticket);

            // Assert
            mockTicketRepository.Verify(tr => tr.Delete(ticket), Times.Once);
        }

        [Test]
        public void ITicketRepository_GetAll_ShouldReturnAllTickets()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var tickets = new List<Ticket>
            {
                new Ticket { Id = 1 },
                new Ticket { Id = 2 }
            };

            mockTicketRepository.Setup(tr => tr.GetAll()).Returns(tickets);

            // Act
            var result = mockTicketRepository.Object.GetAll();

            // Assert
            Assert.AreEqual(tickets, result);
        }

        [Test]
        public void ITicketRepository_GetById_ShouldReturnTicket()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var ticket = new Ticket { Id = 1 };

            mockTicketRepository.Setup(tr => tr.GetById(1)).Returns(ticket);

            // Act
            var result = mockTicketRepository.Object.GetById(1);

            // Assert
            Assert.AreEqual(ticket, result);
        }

        [Test]
        public void ITicketRepository_GetSoldTickets_ShouldReturnSoldTickets()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var routeId = 1;
            var departureDateTime = DateTime.Now;
            var soldTickets = 10;

            mockTicketRepository.Setup(tr => tr.GetSoldTickets(routeId, departureDateTime)).Returns(soldTickets);

            // Act
            var result = mockTicketRepository.Object.GetSoldTickets(routeId, departureDateTime);

            // Assert
            Assert.AreEqual(soldTickets, result);
        }

        [Test]
        public void ITicketRepository_Update_ShouldUpdateTicket()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var ticket = new Ticket { Id = 1 };

            // Act
            mockTicketRepository.Object.Update(ticket);

            // Assert
            mockTicketRepository.Verify(tr => tr.Update(ticket), Times.Once);
        }

        [Test]
        public void ITicketRepository_Save_ShouldSaveChanges()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();

            // Act
            mockTicketRepository.Object.Save();

            // Assert
            mockTicketRepository.Verify(tr => tr.Save(), Times.Once);
        }

        [Test]
        public void ITicketRepository_GetRealDepartureDateTime_ShouldReturnRealDepartureDateTime()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var ticketId = 1;
            var realDepartureDateTime = DateTime.Now;

            mockTicketRepository.Setup(tr => tr.GetRealDepartureDateTime(ticketId)).Returns(realDepartureDateTime);

            // Act
            var result = mockTicketRepository.Object.GetRealDepartureDateTime(ticketId);

            // Assert
            Assert.AreEqual(realDepartureDateTime, result);
        }
        
        
        [Test]
        public void ITicketRepresentation_Represent_ShouldReturnRepresentation()
        {
            // Arrange
            var mockTicketRepresentation = new Mock<ITicketRepresentation>();
            var ticket = new Ticket { Id = 1 };
            var representation = "Ticket Representation";

            mockTicketRepresentation.Setup(tr => tr.Represent(ticket)).Returns(representation);

            // Act
            var result = mockTicketRepresentation.Object.Represent(ticket);

            // Assert
            Assert.AreEqual(representation, result);
        }
        
        
        [Test]
        public void ITicketService_PurchaseTicket_ShouldReturnTrueAndSetOutputParameters()
        {
            // Arrange
            var mockTicketService = new Mock<ITicketService>();
            var routeIds = new List<int> { 1, 2 };
            var departureDateTime = DateTime.Now;
            var buyerId = "buyer123";
            var discountStrategy = new Mock<IDiscountStrategy>().Object;
            var representation = new Mock<ITicketRepresentation>().Object;
            var representationMessage = "Ticket Purchased";
            var newTicketId = 1;

            mockTicketService.Setup(ts => ts.PurchaseTicket(routeIds, departureDateTime, buyerId, out representationMessage, out newTicketId, discountStrategy, representation))
                .Returns(true);

            // Act
            var result = mockTicketService.Object.PurchaseTicket(routeIds, departureDateTime, buyerId, out var repMessage, out var ticketId, discountStrategy, representation);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(representationMessage, repMessage);
            Assert.AreEqual(newTicketId, ticketId);
        }

        [Test]
        public void ITicketService_GetTicketById_ShouldReturnTicket()
        {
            // Arrange
            var mockTicketService = new Mock<ITicketService>();
            var ticket = new Ticket { Id = 1 };

            mockTicketService.Setup(ts => ts.GetTicketById(1)).Returns(ticket);

            // Act
            var result = mockTicketService.Object.GetTicketById(1);

            // Assert
            Assert.AreEqual(ticket, result);
        }
        
        
        [Test]
        public void IUserRepository_GetUserByUsername_ShouldReturnUser()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var user = new User { Username = "testuser" };

            mockUserRepository.Setup(ur => ur.GetUserByUsername("testuser")).Returns(user);

            // Act
            var result = mockUserRepository.Object.GetUserByUsername("testuser");

            // Assert
            Assert.AreEqual(user, result);
        }
        
        
        // [Test]
        // public void TicketReport_Represent_ShouldReturnRepresentation()
        // {
        //     // Arrange
        //     var mockTicketRepresentation = new Mock<ITicketRepresentation>();
        //     var ticket = new Ticket { Id = 1 };
        //     var representation = "Ticket Representation";
        //     mockTicketRepresentation.Setup(tr => tr.Represent(ticket)).Returns(representation);
        //
        //     var ticketReport = new Mock<TicketReport>(mockTicketRepresentation.Object) { CallBase = true };
        //
        //     // Act
        //     var result = ticketReport.Object.Represent(ticket);
        //
        //     // Assert
        //     Assert.AreEqual(representation, result);
        // }
    }
}