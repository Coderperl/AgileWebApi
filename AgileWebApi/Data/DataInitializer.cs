using Bogus;
using Microsoft.EntityFrameworkCore;

namespace AgileWebApi.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _context;

        public DataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            _context.Database.Migrate();
            SeedTechnicians();
            SeedElevators();
            SeedCases();

        }

        private static Random random = new Random();
        private void SeedCases()
        {

            while (_context.Cases.Count() < 10)
            {
                var cases =
                    GenerateCases();
                _context.Cases.Add(cases);
                _context.SaveChanges();
            }
        }

        private void SeedElevators()
        {
            while (_context.Elevators.Count() < 10)
            {
                var elevator =
                    GenerateElevators();
                _context.Elevators.Add(elevator);
                _context.SaveChanges();
            }
        }
        private void SeedTechnicians()
        {
            while (_context.Technicians.Count() < 10)
            {
                var tech =
                    GenerateTechnicians();
                _context.Technicians.Add(tech);
                _context.SaveChanges();
            }
        }
        private Elevator GenerateElevators()
        {
            var n = random.Next(0, 10);
            Elevator elevator = null;
            var testelevator = new Faker<Elevator>()
                    .StrictMode(false)
                    .RuleFor(e => e.Id, f => f.IndexFaker)
                    .RuleFor(e => e.Name, (f, u) => f.Company.CompanyName() + "Elevator")
                    .RuleFor(e => e.Address, (f, u) => f.Address.StreetAddress())
                    .RuleFor(e => e.LastInspection, f => DateTime.Now)
                    .RuleFor(e => e.NextInspection, f => DateTime.Now.AddDays(20))
                    .RuleFor(e => e.Door, (f, u) => false)
                    .RuleFor(e => e.ShutDown, (f, u) => false)
                    .RuleFor(e => e.Reboot, (f, u) => false)
                    .RuleFor(e => e.Floor, (f, u) => f.Random.Number(10))
                    .RuleFor(e => e.ElevatorStatus, (f, u) => "Active")
                    .RuleFor(e => e.MaximumWeight, (f, u) => f.Random.Number(750).ToString());
            elevator = testelevator.Generate(1).First();
            return elevator;
        }
        private Case GenerateCases()
        {            
            var e = random.Next(1, 11);
            var t = random.Next(1, 11);
            var tec = _context.Technicians.FirstOrDefault(a => a.Id == t);
            var ele = _context.Elevators.FirstOrDefault(a => a.Id == e);
            Case c = null;
            var testCase = new Faker<Case>()
                    .StrictMode(false)
                    .RuleFor(e => e.Id, f => f.IndexFaker)
                    .RuleFor(e => e.Name, (f, u) => f.Hacker.Phrase())
                    .RuleFor(e => e.Elevator, ele)
                    .RuleFor(e => e.Technician, tec)
                    .RuleFor(e => e.Status, (f, u) => "Not Started")
                    .RuleFor(e => e.Comments, (f, u) => new List<Comment>()
                    {
                        new Comment() { Issue = "blabla" },
                        new Comment() { Issue = "blabla2"}
                    });
            c = testCase.Generate(1).First();

            return c;
        }
        private Technician GenerateTechnicians()
        {
            var n = random.Next(0, 10);
            Technician technician = null;
            if (n <= 5)
            {
                var testUser = new Faker<Technician>()
                    .StrictMode(false)
                    .RuleFor(e => e.Id, f => f.IndexFaker)
                    .RuleFor(e => e.Name, (f, u) => f.Person.FirstName)
                    .RuleFor(e => e.Role, (f, u) => "SecondLine Technician");


                technician = testUser.Generate(1).First();
            }
            else if (n > 5)
            {
                var testUser = new Faker<Technician>()
                    .StrictMode(false)
                    .RuleFor(e => e.Id, f => f.IndexFaker)
                    .RuleFor(e => e.Name, (f, u) => f.Person.FirstName)
                    .RuleFor(e => e.Role, (f, u) => "Field Technician");


                technician = testUser.Generate(1).First();
            }

            return technician;
        }
    }
}
