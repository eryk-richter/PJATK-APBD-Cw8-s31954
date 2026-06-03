using CW8.DTOs;

namespace CW8.Services;

public interface IPatientService {
    

    public Task<IEnumerable<PatientDetailsDto>> GetPatientsAsync(string? search, CancellationToken cancellationToken);
    public Task AssignBedAsync(string pesel, CreateBedAssignDto req, CancellationToken cancellationToken);
}