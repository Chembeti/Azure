using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using ChembetiMobileService.DataObjects;
using ChembetiMobileService.Models;
using System.Data.Entity.Migrations;

namespace ChembetiMobileService.Controllers
{
    public class RecordController : TableController<Record>
    {
        ChembetiMobileServiceContext context = null;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new ChembetiMobileServiceContext();
            DomainManager = new EntityDomainManager<Record>(context, Request, Services);
        }

        // GET tables/Record
        public IQueryable<Record> GetAllRecord()
        {
            return Query(); 
        }

        // GET tables/Record/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Record> GetRecord(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Record/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Record> PatchRecord(string id, Delta<Record> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Record
        public async Task<IHttpActionResult> PostRecord(Record item)
        {
            // save record first
            var current = await InsertAsync(item);
            // record saved now randomly allocate a doctor to the incoming request
            var recordPatch = new Delta<Record>();
            var doctorToAssign = this.context.Doctors.FirstOrDefault(doctor => doctor.Availability);
            if (doctorToAssign != null)
            {
                recordPatch.TrySetPropertyValue("DoctorId", doctorToAssign.Id);
                // assign doctor to patient
                await this.UpdateAsync(current.Id, recordPatch);
                // block doctor
                doctorToAssign.Availability = false;
                this.context.Doctors.AddOrUpdate(doctor => doctor.Id, doctorToAssign);
                await this.context.SaveChangesAsync();
                // TODO: send mobile notification
                //(broadcast only but you can add tags to target
                //a unique patient)
            }
            return this.CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Record/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteRecord(string id)
        {
             return DeleteAsync(id);
        }

    }
}