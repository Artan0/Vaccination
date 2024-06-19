using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccinationCenterService : IVaccinationCenterService
    {
       
        private readonly IRepository<VaccinationCenter> vaccinationCenterRepository;
        private readonly IRepository<Vaccine> vaccineRepository;

        public VaccinationCenterService(IRepository<VaccinationCenter> vaccinationCenterRepository, IRepository<Vaccine> vaccineRepository)
        {
            this.vaccinationCenterRepository = vaccinationCenterRepository;
            this.vaccineRepository = vaccineRepository;
        }

        public void AddVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            vaccinationCenterRepository.Insert(vaccinationCenter);
        }


        public void Delete(Guid id)
        {
            var vaccCenter = vaccinationCenterRepository.Get(id);
            vaccinationCenterRepository.Delete(vaccCenter);
        }

        public VaccinationCenter Get(Guid? id)
        {
            var vaccCenter = vaccinationCenterRepository.Get(id);
            return vaccCenter;
        }

        public List<VaccinationCenter> GetAll()
        {
            return vaccinationCenterRepository.GetAll().ToList();
        }

        public void Update(VaccinationCenter vaccinationCenter)
        {
            vaccinationCenterRepository.Update(vaccinationCenter);
        }
    }
}
