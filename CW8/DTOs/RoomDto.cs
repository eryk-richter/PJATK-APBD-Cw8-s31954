using System.ComponentModel.DataAnnotations;

namespace CW8.DTOs;

public record RoomDto(
    [MaxLength(4)]
    string Id,
    
    bool HasTv,
    WardDto Ward
);