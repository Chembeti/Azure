using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contoso.Transport.Services.Models
{
    public class Package
    {
        public int Id { get; set; }
        public Guid AccountNumber { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
        public double Weight { get; set; }
        public double Units { get; set; }
        public int StatusCode { get; set; }
        public DateTime Created { get; set; }
    }
}