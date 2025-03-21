using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using PassengerPortal.Client.Services;
using PassengerPortal.Shared.Models;
using PassengerPortal.Client.Pages;

namespace TestProject1
{
    public class ApiServiceTests
    {
        private ApiService _apiService;
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;

        [SetUp]
        public void SetUp()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost:5001/")
            };

            _apiService = new ApiService(httpClient);
        }

        [Test]
        public async Task GetStationsAsync_ShouldReturnStationList()
        {
            // Arrange
            var expectedStations = new List<Station>
            {
                new Station { Id = 1, Name = "Warszawa Centralna" },
                new Station { Id = 2, Name = "Kraków Główny" }
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(expectedStations)
                });

            // Act
            var result = await _apiService.GetStationsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Warszawa Centralna", result[0].Name);
        }

        [Test]
        public async Task SearchConnectionsAsync_ShouldBuildQueryAndReturnConnections()
        {
            // Arrange
            var expectedConnections = new List<Connection>
            {
                new Connection
                {
                    Id = 1,
                    Routes = new List<Route>
                    {
                        new Route { Duration = TimeSpan.FromHours(2), Price = 50 },
                        new Route { Duration = TimeSpan.FromHours(1), Price = 30 }
                    }
                },
                new Connection
                {
                    Id = 2,
                    Routes = new List<Route>
                    {
                        new Route { Duration = TimeSpan.FromHours(3), Price = 70 },
                        new Route { Duration = TimeSpan.FromHours(2), Price = 40 }
                    }
                }
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri.ToString().Contains("startStationId=1") &&
                        req.RequestUri.ToString().Contains("endStationId=2")
                    ),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(expectedConnections)
                });

            // Act
            var result = await _apiService.SearchConnectionsAsync(1, 2, DateTime.UtcNow);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0].Routes.Count);
            Assert.AreEqual(80, result[0].TotalPrice);
        }
        
        // [Test]
        // public async Task PurchaseTicketAsync_ShouldPostRequestAndReturnResponse()
        // {
        //     // Arrange
        //     var request = new SearchConnections.PurchaseTicketRequest
        //     {
        //         ConnectionId = 1,
        //         PassengerName = "Jan Kowalski",
        //         SeatNumber = 12
        //     };
        //
        //     var responseMessage = new HttpResponseMessage
        //     {
        //         StatusCode = HttpStatusCode.OK
        //     };
        //
        //     _httpMessageHandlerMock
        //         .Protected()
        //         .Setup<Task<HttpResponseMessage>>(
        //             "SendAsync",
        //             ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
        //             ItExpr.IsAny<CancellationToken>()
        //         )
        //         .ReturnsAsync(responseMessage);
        //
        //     // Act
        //     var response = await _apiService.PurchaseTicketAsync(request);
        //
        //     // Assert
        //     Assert.IsNotNull(response);
        //     Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        // }
        
    //     [Test]
    //     public async Task GetTicketRepresentationAsync_ShouldReturnTicketRepresentation()
    //     {
    //         // Arrange
    //         var ticketId = 1;
    //         var type = "pdf";
    //         var expectedResponse = new SearchConnections.PurchaseTicketResponse
    //         {
    //             TicketId = ticketId,
    //             RepresentationType = type,
    //             Content = new byte[] { 1, 2, 3, 4 }
    //         };
    //
    //         _httpMessageHandlerMock
    //             .Protected()
    //             .Setup<Task<HttpResponseMessage>>(
    //                 "SendAsync",
    //                 ItExpr.Is<HttpRequestMessage>(req => 
    //                     req.Method == HttpMethod.Get && 
    //                     req.RequestUri.ToString().Contains($"/api/tickets/{ticketId}/representation/{type}")
    //                 ),
    //                 ItExpr.IsAny<CancellationToken>()
    //             )
    //             .ReturnsAsync(new HttpResponseMessage
    //             {
    //                 StatusCode = HttpStatusCode.OK,
    //                 Content = JsonContent.Create(expectedResponse)
    //             });
    //
    //         // Act
    //         var result = await _apiService.GetTicketRepresentationAsync(ticketId, type);
    //
    //         // Assert
    //         Assert.IsNotNull(result);
    //         Assert.AreEqual(expectedResponse.TicketId, result.TicketId);
    //         Assert.AreEqual(expectedResponse.RepresentationType, result.RepresentationType);
    //         Assert.AreEqual(expectedResponse.Content, result.Content);
    //     }
    }
}