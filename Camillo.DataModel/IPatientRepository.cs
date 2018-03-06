using Camillo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Camillo.DataModel
{
    public interface IPatientRepository
    {
        IQueryable<Patient> GetAllPatients();

        bool Add(Patient entry);

        bool Update(Patient entry);

        bool Update(Patient entity, params Expression<Func<Patient, object>>[] properties);

        bool DeletePatient(int id);

        bool Commit();
        void Dispose();
    }
}
