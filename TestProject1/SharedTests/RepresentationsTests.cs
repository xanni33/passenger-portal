using NUnit.Framework;
using Moq;
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Shared.Models;
using PassengerPortal.Shared.Representations;
using System;
using System.Collections.Generic;

namespace TestProject1.SharedTests
{
    public class RepresentationsTests
    {
        [Test]
        public void RepresentEmail_ShouldReturnCorrectRepresentation_WhenRoutesAreEmpty()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var emailRepresentation = new EmailRepresentation(mockTicketRepository.Object);
            var ticket = new Ticket { Id = 1, BuyerId = "user1", Price = 100m, Routes = new List<Route>() };

            // Act
            var result = emailRepresentation.Represent(ticket);

            // Assert
            var expected = $"EMAIL TICKET\nUser: user1\nBrak tras w tym bilecie.\nPrice: {ticket.Price:C}\n";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RepresentEmail_ShouldReturnCorrectRepresentation_WhenRoutesAreNotEmpty()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var emailRepresentation = new EmailRepresentation(mockTicketRepository.Object);
            var ticket = new Ticket
            {
                Id = 1,
                BuyerId = "user1",
                Price = 100m,
                Routes = new List<Route>
                {
                    new Route { StartStation = new Station { Name = "Start" }, EndStation = new Station { Name = "End" } }
                }
            };

            mockTicketRepository.Setup(tr => tr.GetRealDepartureDateTime(ticket.Id)).Returns(DateTime.Now);

            // Act
            var result = emailRepresentation.Represent(ticket);

            // Assert
            StringAssert.Contains("EMAIL TICKET", result);
            StringAssert.Contains("User: user1", result);
            StringAssert.Contains("Price: 100,00 zł", result);
            StringAssert.Contains("Routes:", result);
            StringAssert.Contains("Start -> End", result);
        }
        
        
        [Test]
        public void RepresentPdf_ShouldReturnCorrectRepresentation_WhenRoutesAreEmpty()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var pdfRepresentation = new PdfRepresentation(mockTicketRepository.Object);
            var ticket = new Ticket { Id = 1, BuyerId = "user1", Price = 100m, Routes = new List<Route>() };

            // Act
            var result = pdfRepresentation.Represent(ticket);

            // Assert
            var expected = $"EMAIL TICKET\nUser: user1\nBrak tras w tym bilecie.\nPrice: {ticket.Price:C}\n";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RepresentPdf_ShouldReturnCorrectRepresentation_WhenRoutesAreNotEmpty()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var pdfRepresentation = new PdfRepresentation(mockTicketRepository.Object);
            var ticket = new Ticket
            {
                Id = 1,
                BuyerId = "user1",
                Price = 100m,
                Routes = new List<Route>
                {
                    new Route { StartStation = new Station { Name = "Start" }, EndStation = new Station { Name = "End" } }
                }
            };

            mockTicketRepository.Setup(tr => tr.GetRealDepartureDateTime(ticket.Id)).Returns(DateTime.Now);

            // Act
            var result = pdfRepresentation.Represent(ticket);

            // Assert
            StringAssert.Contains("PDF TICKET", result);
            StringAssert.Contains("User: user1", result);
            StringAssert.Contains("Price: 100,00 zł", result);
            StringAssert.Contains("Routes:", result);
            StringAssert.Contains("Start -> End", result);
        }
        
        
        [Test]
        public void RepresentPrint_ShouldReturnCorrectRepresentation_WhenRoutesAreEmpty()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var printRepresentation = new PrintRepresentation(mockTicketRepository.Object);
            var ticket = new Ticket { Id = 1, BuyerId = "user1", Price = 100m, Routes = new List<Route>() };

            // Act
            var result = printRepresentation.Represent(ticket);

            // Assert
            var expected = $"EMAIL TICKET\nUser: user1\nBrak tras w tym bilecie.\nPrice: {ticket.Price:C}\n";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RepresentPrint_ShouldReturnCorrectRepresentation_WhenRoutesAreNotEmpty()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var printRepresentation = new PrintRepresentation(mockTicketRepository.Object);
            var ticket = new Ticket
            {
                Id = 1,
                BuyerId = "user1",
                Price = 100m,
                Routes = new List<Route>
                {
                    new Route { StartStation = new Station { Name = "Start" }, EndStation = new Station { Name = "End" } }
                }
            };

            mockTicketRepository.Setup(tr => tr.GetRealDepartureDateTime(ticket.Id)).Returns(DateTime.Now);

            // Act
            var result = printRepresentation.Represent(ticket);

            // Assert
            StringAssert.Contains("PRINT TICKET", result);
            StringAssert.Contains("User: user1", result);
            StringAssert.Contains("Price: 100,00 zł", result);
            StringAssert.Contains("Routes:", result);
            StringAssert.Contains("Start -> End", result);
        }
    }
}