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
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> patientRepository;

        public PatientService(IRepository<Patient> patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public void AddPatient(Patient patient)
        {
            patientRepository.Insert(patient);
        }

        public void Delete(Guid id)
        {
            var patient = patientRepository.Get(id);
            patientRepository.Delete(patient);
        }

        public Patient Get(Guid? id)
        {
            var patient = patientRepository.Get(id);
            return patient;
        }

        public List<Patient> GetAll()
        {
            return patientRepository.GetAll().ToList();
        }

        public void Update(Patient patient)
        {
            patientRepository.Update(patient);
        }
    }
}
