namespace CW8.DTOs;

public record RoomDto(
    string id,
    bool hasTv,
    WardDto ward
);