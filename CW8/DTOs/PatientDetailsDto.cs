namespace CW8.DTOs;

public record PatientDetailsDto
(
    string pesel,
    string firstName,
    string lastName,
    int age,
    string sex,
    IEnumerable<AdmissionDto> admissions,
    IEnumerable<BedAssignmentDto> bedAssignments
);