using Camillo.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camillo.DataModel
{
    public class PatientRepository : IPatientRepository
    {
        protected CamilloContext _context;

        public PatientRepository(CamilloContext context)
        {
            _context = context;
            context.Database.Log = Console.WriteLine;
        }

        public IQueryable<Patient> GetAllPatients()
        {
            return _context.Patients;
        }

        public bool Add(Patient entry)
        {
            try
            {
                _context.Patients.Add(entry);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Patient entry)
        {
            return UpdateEntity(_context.Patients, entry);
        }

        public bool DeletePatient(int id)
        {
                var entity = _context.Patients.Where(f => f.AccountId == id).FirstOrDefault();
                if (entity != null)
                {
                    _context.Patients.Remove(entity);
                    return true;
                }

            return false;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        // Helper to update objects in context
        bool UpdateEntity<T>(DbSet<T> dbSet, T entity) where T : class
        {
            try
            {
                if (this._context.Entry(entity).State == EntityState.Detached)
                    dbSet.Attach(entity);

                this._context.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (this._context != null)
                this._context.Dispose();
        }
    }
}
