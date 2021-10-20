using FlightPlanner.Core.Models;
using System.Data.Entity;

namespace FLightPlanner.Data
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext() : base("name=FlightPlannerDbContext")
        {
            Database.SetInitializer<FlightPlannerDbContext>(new CreateDatabaseIfNotExists<FlightPlannerDbContext>());
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
