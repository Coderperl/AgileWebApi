using AgileWebApi.Controllers;
using AgileWebApi.Data;
using AgileWebApi.DataTransferObjects.ElevatorDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests.ElevatorControllerTests
{
	public class ElevatorControllerTests
	{
		private readonly ElevatorController _sut;
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public ElevatorControllerTests()
		{
			var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase("test2")
				.Options;
			_context = new ApplicationDbContext(contextOptions);
			_context.Database.EnsureCreated();
			_sut = new ElevatorController(_context, _configuration);
		}

		[Fact]
		public void GetAll_Elevators_When_Return_OK()
		{
			_context.Database.EnsureDeleted();
			//Arrange

			var elevator = new Elevator()
			{
				Id = 7,
				Name = "h1",
				Address = "hus1",
				MaximumWeight = "5",
				LastInspection = DateTime.Now,
				NextInspection = DateTime.Now.AddMonths(12),
				Reboot = false,
				ShutDown = false,
				Door = true,
				Floor = 3,
				ElevatorStatus = "Active"
			};
			_context.Elevators.Add(elevator);
			_context.SaveChanges();

			//Act

			var okResult = _sut.GetAll();

			//Assert

			Assert.IsType<OkObjectResult>(okResult);
		}

		[Fact]
		public void Get_One_Elevator_by_existing_Id_Returns_OkObjectResult()
		{
			_context.Database.EnsureDeleted();
			//Arrange
			var elevator = new Elevator()
			{
				Id = 3,
				Name = "h1",
				Address = "hus1",
				MaximumWeight = "5",
				LastInspection = DateTime.Now,
				NextInspection = DateTime.Now.AddMonths(12),
				Reboot = false,
				ShutDown = false,
				Door = true,
				Floor = 3,
				ElevatorStatus = "Active"
			};
			_context.Elevators.Add(elevator);
			_context.SaveChanges();

			//Act
			var okObject = _sut.GetById(3);

			//Assert
			Assert.IsType<OkObjectResult>(okObject as OkObjectResult);

		}

		[Fact]
		public void Get_One_By_Id_Not_Existing_Id_Should_Return_NotFoundResult()
		{
			//Arrange

			var testId = 15;

			//Act
			var result = _sut.GetById(testId);

			//Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public void Update_Elevator_with_Correct_Id_Returns_NoContent()
		{
			_context.Database.EnsureDeleted();

			//Arrange

			var elevator = new Elevator()
			{
				Id = 3,
				Name = "h1",
				Address = "hus1",
				MaximumWeight = "5",
				LastInspection = DateTime.Now,
				NextInspection = DateTime.Now.AddMonths(12),
				Reboot = false,
				ShutDown = false,
				Door = true,
				Floor = 3,
				ElevatorStatus = "Active"
			};

			_context.Elevators.Add(elevator);
			_context.SaveChanges();

			var updatedElevator = new UpdateElevatorDTO()
			{
				Name = "h2",
				Address = "hus2",
				MaximumWeight = "5",
				LastInspection = DateTime.Now,
				NextInspection = DateTime.Now.AddMonths(12),
				Reboot = false,
				ShutDown = false,
				Door = true,
				Floor = 3,
				ElevatorStatus = "Active"
			};

			//Act
			var updatedEle = _sut.UpdateElevator(updatedElevator, 3);

			//Assert

			Assert.IsType<NoContentResult>(updatedEle);

		}

		[Fact]
		public void Update_Elevator_Wrong_Id_Returns_BadRequest()
		{

			_context.Database.EnsureDeleted();

			//Arrange

			var elevator = new Elevator()
			{
				Id = 44,
				Name = "h1",
				Address = "hus1",
				MaximumWeight = "5",
				LastInspection = DateTime.Now,
				NextInspection = DateTime.Now.AddMonths(12),
				Reboot = false,
				ShutDown = false,
				Door = true,
				Floor = 3,
				ElevatorStatus = "Active"
			};

			_context.Elevators.Add(elevator);
			_context.SaveChanges();

			var updatedElevator = new UpdateElevatorDTO()
			{
				Name = "h2",
				Address = "hus2",
				MaximumWeight = "5",
				LastInspection = DateTime.Now,
				NextInspection = DateTime.Now.AddMonths(12),
				Reboot = false,
				ShutDown = false,
				Door = true,
				Floor = 3,
				ElevatorStatus = "Active"
			};

			//Act
			var updatedEle = _sut.UpdateElevator(updatedElevator, 45);

			//Assert

			Assert.IsType<BadRequestResult>(updatedEle);

		}
	}
}
