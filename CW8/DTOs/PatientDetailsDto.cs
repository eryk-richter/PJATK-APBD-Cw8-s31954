using System.ComponentModel.DataAnnotations;

namespace CW8.DTOs;

public record PatientDetailsDto
(
    [MaxLength(11)]
    string pesel,
    
    [MaxLength(50)]
    string firstName,
    
    [MaxLength(100)]
    string lastName,
    
    int age,
    string sex,
    IEnumerable<AdmissionDto> admissions,
    IEnumerable<BedAssignmentDto> bedAssignments
);