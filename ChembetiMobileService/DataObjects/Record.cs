using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChembetiMobileService.DataObjects
{
    public class Record : EntityData
    {
        public string PatientId { get; set; }
        public string InsuranceId { get; set; }
        public string DoctorId { get; set; }
        public string Emergency { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
    }
}