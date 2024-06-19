using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IVaccineService
    {
        void AddVaccineToPatient(VaccineDTO dto);
        List<Vaccine> GetVaccinesForCenter(Guid? centerId);

        List<Vaccine> GetVaccinesForPatient(Guid? patientId);

    }
}
