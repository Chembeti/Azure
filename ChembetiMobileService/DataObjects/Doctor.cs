using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChembetiMobileService.DataObjects
{
    public class Doctor : EntityData
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Location { get; set; }
        public bool Availability { get; set; }
    }
}