using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;
using IntegratedSystems.Service.Implementation;
using IntegratedSystems.Domain.DTO;

namespace IntegratedSystems.Web.Controllers
{
    public class VaccinationCentersController : Controller
    {
        private readonly IVaccinationCenterService _vaccinationCenterService;
        private readonly ApplicationDbContext _context;
        private readonly IVaccineService _vaccineService;
        private readonly IPatientService patientService;

        public VaccinationCentersController(IVaccinationCenterService vaccinationCenterService, ApplicationDbContext context, IVaccineService vaccineService, IPatientService patientService)
        {
            _vaccinationCenterService = vaccinationCenterService;
            _context = context;
            _vaccineService = vaccineService;
            this.patientService = patientService;
        }


        // GET: VaccinationCenters
        public IActionResult Index()
        {
            return View(_vaccinationCenterService.GetAll());
        }

        // GET: VaccinationCenters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var centerDTO = new CenterVaccineDTO();
            
            centerDTO.VaccinationCenter = _vaccinationCenterService.Get(id);
            centerDTO.vaccines = _vaccineService.GetVaccinesForCenter(id);
            if (centerDTO == null)
            {
                return NotFound();
            }

            return View(centerDTO);
        }

        // GET: VaccinationCenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccinationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (ModelState.IsValid)
            {
                _vaccinationCenterService.AddVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.Get(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (id != vaccinationCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vaccinationCenterService.Update(vaccinationCenter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationCenterExists(vaccinationCenter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.Get(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vaccinationCenter = _vaccinationCenterService.Get(id);
            if (vaccinationCenter != null)
            {
                _vaccinationCenterService.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationCenterExists(Guid id)
        {
            return _context.VaccinationCenters.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult AddVaccinedPatient(Guid id)
        {
            var vaccCenter = _vaccinationCenterService.Get(id);
           
            VaccineDTO dto = new VaccineDTO();
            dto.vaccCenterId = id;
            dto.patients = patientService.GetAll();
            dto.manufacturers = new List<string>()
            {
                "Sputnik", "Astra Zeneca", "Phizer"
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult AddVaccineToPatient(VaccineDTO vaccine)
        {
            if (ModelState.IsValid)
            {
                _vaccineService.AddVaccineToPatient(vaccine);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
