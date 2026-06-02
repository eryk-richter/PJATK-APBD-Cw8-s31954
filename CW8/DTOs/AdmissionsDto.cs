namespace CW8.DTOs;

public record AdmissionDto(
    int id,
    DateTime admissionDate,
    DateTime dischargeDate,
    WardDto ward
);