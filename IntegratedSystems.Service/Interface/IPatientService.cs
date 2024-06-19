using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IPatientService
    {
        List<Patient> GetAll();
        Patient Get(Guid? id);
        void Update(Patient patient);
        void Delete(Guid id);
        void AddPatient(Patient patient);

    }
}
