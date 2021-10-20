using System.Data.Entity;
using FlightPlanner2.Models;

namespace FlightPlanner2.Db
{
    public class FlightPlannerDbContext : DbContext
    {
        public FlightPlannerDbContext() : base("name=FlightPlannerDbContext")
        {
            Database.SetInitializer<FlightPlannerDbContext>(new CreateDatabaseIfNotExists<FlightPlannerDbContext>());
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}