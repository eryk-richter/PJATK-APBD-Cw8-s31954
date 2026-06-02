using CW8.Services;
using CW8.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CW8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IPatientService patientService) : ControllerBase {

    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery] string? search, CancellationToken cancellationToken) {
            return Ok(await patientService.GetPatientsAsync(search, cancellationToken));
    }

}