using Camillo.DataModel;
using Camillo.Domain;
using Camillo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Security;

namespace Camillo.Controllers
{
    [RoutePrefix("api/patients")]
    public class PatientsController : ApiController
    {
        private IPatientRepository _repository;
        public PatientsController()
        {
            //TODO to resolve via DI
            _repository = new PatientRepository(new CamilloContext());
        }

        [Route("", Name ="Patients")]
        public IHttpActionResult Get(int page=0, int pageSize=20)
        {
            try { 
           
            var baseQuery = _repository
                            .GetAllPatients()
                           .OrderByDescending(p=>p.AccountId);

            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var prevUrl = page > 0 ? Url.Link("Patients", new { page = page - 1, pageSize=pageSize }) : "";
            var nextUrl = page < totalPages - 1 ? Url.Link("Patients", new { page = page + 1, pageSize = pageSize }) : "";

            var results = baseQuery.Skip(pageSize * page)
                                   .Take(pageSize)
                                   .Select(p => new PatientSummary
                                   {
                                       AccountId = p.AccountId,
                                       RegisterDate = p.Account.CreationDate,
                                       FirstName = p.FirstName,
                                       LastName = p.LastName,
                                       Status = p.Status,
                                       PhotoUrl = p.PhotoUrl
                                   }).ToList();

           
            return base.Ok(new
            {
                TotalCount = totalCount,
                TotalPage = totalPages,
                PrevPageUrl = prevUrl,
                NextPageUrl = nextUrl,
                Results = results
            });

            }
            catch (Exception ex)
            {
                return base.BadRequest(ex.Message);
            }
        }

        [Route("{id:int:min(1)}")]
        public IHttpActionResult Get(int id) {

            try { 

            var result = _repository.GetAllPatients()
                        .Where(p => p.AccountId == id)
                        .Select( CreateModel() ).FirstOrDefault();

            if (result == null)
            {
                return base.NotFound();
            }

            return base.Ok(result);

            }
            catch (Exception ex)
            {
                return base.BadRequest(ex.Message);
            }
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]PatientEntryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new Patient
                    {
                        Account = new Account
                        {
                            Username = model.Username,
                            Password = model.Password,
                            Email = model.Email,
                            CreationDate = DateTime.Now
                        },
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = model.BirthDate.Value,
                        Status = model.Status,
                        Gender = model.Gender,
                        Telephone = model.Telephone,
                        Address = model.Address,
                        MedicareNumber = model.MedicareNumber
                    };

                    _repository.Add(entity);
                    _repository.Commit();

                    model.AccountId = entity.AccountId;

                    return base.Ok(model);
                }
                else
                {
                    return base.BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return base.BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [HttpPatch]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult Patch(int id,[FromBody] PatientEntryModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new Patient
                    {
                        AccountId=id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = model.BirthDate.Value,
                        Status = model.Status,
                        Gender = model.Gender,
                        Telephone = model.Telephone,
                        Address = model.Address,
                        MedicareNumber = model.MedicareNumber
                    };

                    if (_repository.Update(entity, e => e.FirstName,
                                                   e => e.FirstName,
                                                   e => e.LastName,
                                                   e => e.BirthDate,
                                                   e => e.Status,
                                                   e => e.Gender,
                                                   e => e.Telephone,
                                                   e => e.Address,
                                                   e => e.MedicareNumber))
                    {
                        _repository.Commit();
                    }
                    else
                    {
                        return base.BadRequest("Cannot update Patient "+entity.AccountId);
                    }

                    return base.Ok();
                }
                else
                {
                    return base.BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return base.BadRequest(ex.Message);
            }
        }
        
        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult Delete(int id)
        {
            try {

                _repository.DeletePatient(id);
                _repository.Commit();

                return base.Ok();
            }
            catch (Exception ex)
            {
                return base.BadRequest(ex.Message);
            }
        }

        private Expression<Func<Patient, PatientEntryModel>> CreateModel()
        {
            return
                p =>
                new PatientEntryModel
                {
                    AccountId = p.AccountId,
                    RegisterDate = p.Account.CreationDate,
                    Username = p.Account.Username,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Account.Email,
                    Address = p.Address,
                    Gender = p.Gender,
                    Status = p.Status,
                    Telephone = p.Telephone,
                    PhotoUrl = p.PhotoUrl,
                    MedicareNumber = p.MedicareNumber,
                    BirthDate = p.BirthDate
                };
        }

        

        //The IoC container should take care of this once implemented
        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
