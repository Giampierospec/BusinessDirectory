using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.Services
{
    /// <summary>
    /// Class to help me save location data
    /// </summary>
    public class GeoCoordResult
    {
       
            public bool Success { get; set; }
            public string Message { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        
    }
}
