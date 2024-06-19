using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IVaccinationCenterService
    {
        List<VaccinationCenter> GetAll();
        VaccinationCenter Get(Guid? id);
        void Delete(Guid id);
        void Update(VaccinationCenter vaccinationCenter);
        void AddVaccinationCenter(VaccinationCenter vaccinationCenter);

    }
}
