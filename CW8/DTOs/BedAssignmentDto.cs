namespace CW8.DTOs;

public record BedAssignmentDto (
    int id,
    DateTime from,
    DateTime? to,
    BedDto bed
);