using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Radio
    {
        public int Id { get; set; }
        public string Alias { get; set; }

        public List<Location> AllowedLocations { get; set; }

        public Location CurrentLocation { get; set; }
    }
}
