using CW8.DTOs;

namespace CW8.Services;

public interface IPatientService {
    
    public Task<IEnumerable<PatientDetailsDto>> GetAllAsync(CancellationToken cancellationToken);
    public Task<PatientDetailsDto> GetPatientByNameAsync(string name, CancellationToken cancellationToken);
    
}