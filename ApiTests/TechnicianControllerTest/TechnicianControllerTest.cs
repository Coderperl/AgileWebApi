using AgileWebApi.Data;
using Microsoft.EntityFrameworkCore;
using AgileWebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using AgileWebApi.DataTransferObjects.TechnicialDTO;

namespace ApiTests.TechnicianControllerTest
{
    public class TechnicianControllerTest
    {
        private readonly TechnicianController _sut;
        private readonly ApplicationDbContext _context;
        public TechnicianControllerTest()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test")
                .Options;
            _context = new ApplicationDbContext(contextOptions);
            _context.Database.EnsureCreated();
            _sut = new TechnicianController(_context);
        }

        [Fact]
        public void GetAllTechnicians_When_Called_ReturnOkResult()
        {
            _context.Database.EnsureDeleted();
            //Arrange
            var testTechnician = new Technician()
            {
                Id = 1,
                Name = "TestCase",
                Role = "TestRole"
            };
            _context.Add(testTechnician);
            _context.SaveChanges();

            //Act
            var okResult = _sut.GetAllTechnicians();
            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        [Fact]
        public void GetAll_When_Called_returns_All_Technicians()
        {
            _context.Database.EnsureDeleted();
            //Arrange
            var testTechnician = new Technician()
            {
                Id = 2,
                Name = "TestCase",
                Role = "TestRole"
            };
            var testTechnician2 = new Technician()
            {
                Id = 3,
                Name = "TestCase",
                Role = "TestRole"
            };
            _context.Add(testTechnician);
            _context.Add(testTechnician2);
            _context.SaveChanges();
            //Act
            var okResult = _sut.GetAllTechnicians() as OkObjectResult;
            //Assert
            var items = Assert.IsType<List<TechniciansDTO>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void GetOneTechnician_With_InvalidId_ReturnsNoFoundResult()
        {
            //Arrange
            var testId = 10;
            //Act
            var notFoundResult = _sut.GetOneTechnician(testId);
            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetOneTechnician_WithExistingId_ReturnsOkResult()
        {
            _context.Database.EnsureDeleted();
            //Arrange
            var testTechnician = new Technician()
            {
                Id = 1,
                Name = "TestCase",
                Role = "TestRole"
            };
            _context.Add(testTechnician);
            _context.SaveChanges();
            //Act
            var okResult = _sut.GetOneTechnician(testTechnician.Id);
            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
    }
}
