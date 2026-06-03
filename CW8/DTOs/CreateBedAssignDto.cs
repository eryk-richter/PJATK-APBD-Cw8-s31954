using System.ComponentModel.DataAnnotations;

namespace CW8.DTOs;

public record CreateBedAssignDto 
(
    DateTime From,
    DateTime? To,
    
    [MaxLength(300)]
    string BedType,
    [MaxLength(300)]
    string Ward
    
);