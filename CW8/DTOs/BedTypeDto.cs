using System.ComponentModel.DataAnnotations;

namespace CW8.DTOs;

public record BedTypeDto(
    int id,
    
    [MaxLength(300)]
    string name,
    
    string description
);