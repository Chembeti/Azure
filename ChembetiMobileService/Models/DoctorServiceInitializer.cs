using ChembetiMobileService.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChembetiMobileService.Models
{
    public class DoctorServiceInitializer : DropCreateDatabaseIfModelChanges<ChembetiMobileServiceContext>
    {
        protected override void Seed(ChembetiMobileServiceContext context)
        {
            var doctors = new List<Doctor>
            {
                new Doctor { Id = "1", Specialty = "PRI", Availability = true, Name = "John Smith." },
                new Doctor { Id = "2", Specialty = "GYNO", Availability = true, Name = "Ram Jaiswal." },
                new Doctor { Id = "3", Specialty = "ORTHO", Availability = true, Name = "Cassandra Stevens" }
            };

            foreach (var doctor in doctors)
            {
                context.Set<Doctor>().Add(doctor);
            }
        }
    }
}