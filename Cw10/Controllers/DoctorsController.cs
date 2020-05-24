using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw10.DTOs.Requests;
using Cw10.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw10.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : Controller
    {
        private readonly IDbService dbService;

        public DoctorsController(IDbService dbService)
        {
            this.dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            var res = dbService.GetDoctors();
            if (res == null)
                return NotFound("NO DATA TO DISPLAY OR AN ERROR OCURED");
            else
                return Ok(res);
        }

        [HttpDelete("deleteDoctor/{IdDoctor}")]
        public IActionResult DeleteDoctor(int IdDoctor)
        {
            if (dbService.DeleteDoctor(IdDoctor))
                return Ok("DOCTOR WAS DELETED");
            else
                return BadRequest("WRONG DATA");
        }

        [HttpPost("addDoctor")]
        public IActionResult AddDoctor(AddDoctorRequest request)
        {
            if (dbService.AddDoctor(request))
                return Ok("DOCTOR WAS ADDED");
            else
                return BadRequest("WRONG DATA");
        }

        [HttpPut("updateDoctor")]
        public IActionResult UpdateDoctor(UpdateDoctorRequest request)
        {
            if (dbService.UpdateDoctor(request))
                return Ok("DOCTOR DATA WAS UPDATED");
            else
                return BadRequest("WRONG DATA");
        }

    }
}