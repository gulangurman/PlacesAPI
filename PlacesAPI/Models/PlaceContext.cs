using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacesAPI.Models
{
    public class PlaceContext : DbContext
    {
        public PlaceContext(DbContextOptions<PlaceContext> options)
            : base(options)
        {
        }
        
        public DbSet<Place> Places { get; set; }
    }
}
