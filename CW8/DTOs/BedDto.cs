using CW8.Models;

namespace CW8.DTOs;

public record BedDto(
    int id,
    BedType bedType,
    RoomDto room
);