using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contoso.Transport.Services.Models
{
    public class Event
    {
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public Guid Id { get; set; }
        public string Location { get; set; }
    }
}