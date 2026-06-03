using CW8.DTOs;
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

    [HttpPost("{pesel}/bedassignments")]
    public async Task<IActionResult> PostBedAssign(string pesel, [FromBody] CreateBedAssignDto req,
        CancellationToken cancellationToken) {
        try {
            await patientService.AssignBedAsync(pesel, req, cancellationToken);
            return Created();
        }
        catch (InvalidPeselException e) {
            return BadRequest(e.Message);
        }

        catch (PatientNotFoundException e) {
            return NotFound(e.Message);
        }
        catch (WardNotFoundException e) {
            return NotFound(e.Message);
        }
        catch (BedTypeNotFoundException e) {
            return NotFound(e.Message);
        }
        catch (BedNotAvailableException e) {
            return Conflict(e.Message);
        }
        catch(ArgumentException e) {
            return BadRequest(e.Message);
        }
    }

}