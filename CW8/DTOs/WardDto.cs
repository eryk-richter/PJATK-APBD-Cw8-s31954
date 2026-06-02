using System.ComponentModel.DataAnnotations;

namespace CW8.DTOs;

public record WardDto(
    int Id,
    
    [MaxLength(300)]
    string Name,
    
    string Description
);