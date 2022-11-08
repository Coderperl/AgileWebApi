using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgileWebApi.Controllers;
using AgileWebApi.Data;
using AgileWebApi.DataTransferObjects.CaseDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTests.CaseControllerTests
{
    public class CaseControllerTest
    {
        private readonly CaseController _sut;
        private readonly ApplicationDbContext _context;
        public CaseControllerTest()
        {
            
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test")
                .Options;
            _context = new ApplicationDbContext(contextOptions);
            _context.Database.EnsureCreated();
            _sut = new CaseController(_context);
        }

        [Fact]
        public void GetAllCases_When_Called_ReturnOkResult()
        {
            _context.Database.EnsureDeleted();
            //Arrange
            var testCase = new Case()
            {
                Id = 1,
                Name = "TestCase",
                CaseCreated = DateTime.Now,
                CaseEnded = DateTime.Now.AddDays(10),
                Elevator = new Elevator()
                {
                    Id = 1,
                    Name = "TestElevator",
                    Address = "test",
                    ElevatorStatus = "test",
                    MaximumWeight = "test"
                },
                Technician = new Technician()
                {
                    Id = 1,
                    Name = "testTechnicianName",
                    Role = "TestRole"
                },
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Issue = "testComment"
                    }
                },
                CreatedBy = 1,
                Status = "TestStatus"
            };
            _context.Add(testCase);
            _context.SaveChanges();
            //Act
            var okResult = _sut.GetAllCases();
            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetAll_When_Called_returns_All_Cases()
        {
            _context.Database.EnsureDeleted();
            //Arrange
            var testCase = new Case()
            {
                Id = 2,
                Name = "TestCase",
                CaseCreated = DateTime.Now,
                CaseEnded = DateTime.Now.AddDays(10),
                Elevator = new Elevator()
                {
                    Id = 1,
                    Name = "TestElevator",
                    Address = "test",
                    ElevatorStatus = "test",
                    MaximumWeight = "test"
                },
                Technician = new Technician()
                {
                    Id = 1,
                    Name = "testTechnicianName",
                    Role = "TestRole"
                },
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Issue = "testComment"
                    }
                },
                CreatedBy = 1,
                Status = "TestStatus"
            };
            _context.Add(testCase);
            _context.SaveChanges();
            //Act
            var okResult = _sut.GetAllCases() as OkObjectResult;
            //Assert
            var items = Assert.IsType<List<CasesDTO>>(okResult.Value);
            Assert.Equal(1,items.Count);
        }

        [Fact]
        public void GetOneCase_With_InvalidId_ReturnsNoFoundResult()
        {
            //Arrange 
            var testId = 10;
            //Act
            var notFoundResult = _sut.GetOneCase(testId);
            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetOneCase_WithExistingId_ReturnsOkResult()
        {
            _context.Database.EnsureDeleted();
            //Arrange
            var testCase = new Case()
            {
                Id = 1,
                Name = "TestCase",
                CaseCreated = DateTime.Now,
                CaseEnded = DateTime.Now.AddDays(10),
                Elevator = new Elevator()
                {
                    Id = 1,
                    Name = "TestElevator",
                    Address = "test",
                    ElevatorStatus = "test",
                    MaximumWeight = "test"
                },
                Technician = new Technician()
                {
                    Id = 1,
                    Name = "testTechnicianName",
                    Role = "TestRole"
                },
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Issue = "testComment"
                    }
                },
                CreatedBy = 1,
                Status = "TestStatus"
            };
            _context.Add(testCase);
            _context.SaveChanges();
            //Act
            var okResult = _sut.GetOneCase(testCase.Id);
            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void CreateCase_InvalidObjectSent_NotFoundResult()
        {
            _context.Database.EnsureDeleted();
            //Arrange
            var testCase = new CreateCaseDTO()
            {
                CaseCreated = DateTime.Now,
                CaseEnded = DateTime.Now.AddDays(10),
                ElevatorId = 1,
                TechnicianId = 0,
                CreatedBy = 1,
                Status = "TestStatus",
                Comment = new Comment(),
            };
            _sut.ModelState.AddModelError("Name","Required");
            //Act
            var badRequest = _sut.CreateCase(testCase);
            //Assert
            Assert.IsType<NotFoundObjectResult>(badRequest);
        }

        [Fact]
        public void CreateCase_ValidObjectSent_ReturnsCreatedResponse()
        {
            _context.Database.EnsureDeleted();
            //Arrange
            var testCase = new Case()
            {
                Id = 1,
                Name = "TestCase",
                CaseCreated = DateTime.Now,
                CaseEnded = DateTime.Now.AddDays(10),
                Elevator = new Elevator()
                {
                    Id = 1,
                    Name = "TestElevator",
                    Address = "test",
                    ElevatorStatus = "test",
                    MaximumWeight = "test"
                },
                Technician = new Technician()
                {
                    Id = 1,
                    Name = "testTechnicianName",
                    Role = "TestRole"
                },
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Issue = "testComment"
                    }
                },
                CreatedBy = 1,
                Status = "TestStatus"
            };
            _context.Add(testCase);
            _context.SaveChanges();
            var testCase2 = new CreateCaseDTO()
            {
                Name = "TestCase",
                CaseCreated = DateTime.Now,
                CaseEnded = DateTime.Now.AddDays(10),
                ElevatorId = 1,
                TechnicianId = 1,
                CreatedBy = 1,
                Status = "TestStatus",
                Comment = new Comment()
                {
                    Issue = "TestComment"
                },
            };
            //Act
            var badRequest = _sut.CreateCase(testCase2);
            //Assert
            Assert.IsType<CreatedAtActionResult>(badRequest);
        }
        [Fact]
        public void EditCase_ValidObjectSent_ReturnsNoContent()
        {
            _context.Database.EnsureDeleted();
            //Arrange
            var testCase = new Case()
            {
                Id = 1,
                Name = "TestCase",
                CaseCreated = DateTime.Now,
                CaseEnded = DateTime.Now.AddDays(10),
                Elevator = new Elevator()
                {
                    Id = 1,
                    Name = "TestElevator",
                    Address = "test",
                    ElevatorStatus = "test",
                    MaximumWeight = "test"
                },
                Technician = new Technician()
                {
                    Id = 1,
                    Name = "testTechnicianName",
                    Role = "TestRole"
                },
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Issue = "testComment"
                    }
                },
                CreatedBy = 1,
                Status = "TestStatus"
            };
            _context.Add(testCase);
            _context.SaveChanges();
            var testCase2 = new UpdateCaseDTO()
            {
                CaseEnded = DateTime.Now.AddDays(10),
                TechnicianId = 1,
                Status = "TestStatus",
                Comment = new Comment()
                {
                    Issue = "TestComment"
                },
            };
            //Act
            var updateCase = _sut.UpdateCase(testCase2, 1);
            //Assert
            Assert.IsType<NoContentResult>(updateCase);
        }
        [Fact]
        public void EditCase_InValidObjectSent_ReturnsBadRequest()
        {
            _context.Database.EnsureDeleted();
            //Arrange
            var testCase = new Case()
            {
                Id = 1,
                Name = "TestCase",
                CaseCreated = DateTime.Now,
                CaseEnded = DateTime.Now.AddDays(10),
                Elevator = new Elevator()
                {
                    Id = 1,
                    Name = "TestElevator",
                    Address = "test",
                    ElevatorStatus = "test",
                    MaximumWeight = "test"
                },
                Technician = new Technician()
                {
                    Id = 1,
                    Name = "testTechnicianName",
                    Role = "TestRole"
                },
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Issue = "testComment"
                    }
                },
                CreatedBy = 1,
                Status = "TestStatus"
            };
            _context.Add(testCase);
            _context.SaveChanges();
            var testCase2 = new UpdateCaseDTO()
            {
                CaseEnded = DateTime.Now.AddDays(10),
                TechnicianId = 1,
                Status = "TestStatus",
                Comment = new Comment()
                {
                    Issue = "TestComment"
                },
            };
            //Act
            var updateCase = _sut.UpdateCase(testCase2, 10);
            //Assert
            Assert.IsType<BadRequestResult>(updateCase);
        }
    }
}
