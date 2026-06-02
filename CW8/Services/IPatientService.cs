using CW8.DTOs;

namespace CW8.Services;

public interface IPatientService {
    
    // public Task<IEnumerable<PatientDetailsDto>> GetAllAsync(CancellationToken cancellationToken);
    // public Task<IEnumerable<PatientDetailsDto>> GetPatientByNameAsync(string name, CancellationToken cancellationToken);
    
    public Task<IEnumerable<PatientDetailsDto>> GetPatientsAsync(string? search, CancellationToken cancellationToken);
}