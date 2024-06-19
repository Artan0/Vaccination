using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.DTO;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccineService : IVaccineService
    {
        private readonly IRepository<Vaccine> vaccineRepository;
        private readonly IRepository<VaccinationCenter> vaccinationCenterRepository;
        public VaccineService(IRepository<Vaccine> repository, IRepository<VaccinationCenter> vaccinationCenterRepo)
        {
            this.vaccineRepository = repository;
            vaccinationCenterRepository = vaccinationCenterRepo;
        }
        public void AddVaccineToPatient(VaccineDTO dto)
        {
            Vaccine vaccine = new Vaccine();
            vaccine.Manufacturer = dto.manufacturer;
            vaccine.Certificate = Guid.NewGuid();
            vaccine.VaccinationCenter = dto.vaccCenterId;
            vaccine.PatientId = dto.patientId;
            vaccine.DateTaken = dto.vaccinationDate;
            vaccine.Center = vaccinationCenterRepository.Get(dto.vaccCenterId);
            vaccineRepository.Insert(vaccine);
        }

        public List<Vaccine> GetVaccinesForCenter(Guid? centerId)
        {
            return vaccineRepository.GetAll().Where(z => z.VaccinationCenter == centerId).ToList();
        }

        public List<Vaccine> GetVaccinesForPatient(Guid? patientId)
        {
            return vaccineRepository.GetAll().Where(z => z.PatientId == patientId).ToList();
        }
    }
}
