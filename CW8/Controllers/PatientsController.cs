using CW8.Services;
using CW8.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CW8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IPatientService patientService) : ControllerBase {


    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) {
        return Ok(await patientService.GetAllAsync(cancellationToken));
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetByName([FromQuery] string name, CancellationToken cancellationToken) {
        try {
            return Ok(await patientService.GetPatientByNameAsync(name, cancellationToken));
        }
        catch (PatientNotFoundException e) {
            return NotFound(e.Message);
        }
    }

}