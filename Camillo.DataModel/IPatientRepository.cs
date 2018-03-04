using Camillo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camillo.DataModel
{
    public interface IPatientRepository
    {
        IQueryable<Patient> GetAllPatients();

        bool Add(Patient entry);

        bool Update(Patient entry);

        bool DeletePatient(int id);

        bool Commit();
        void Dispose();
    }
}
