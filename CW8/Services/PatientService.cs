using CW8.DTOs;
using CW8.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CW8.Services;

public class PatientService (Apbd8Context ctx): IPatientService {
    public async Task<IEnumerable<PatientDetailsDto>> GetAllAsync(CancellationToken cancellationToken) {
        return await ctx.Patients.Select(p => new PatientDetailsDto(
            p.Pesel,
            p.FirstName,
            p.LastName,
            p.Age,
            p.Sex ? "Male" : "Female",
            p.Admissions.Select(pa => new AdmissionDto(
                pa.Id,
                pa.AdmissionDate,
                pa.DischargeDate,
                new WardDto(
                    pa.Ward.Id,
                    pa.Ward.Name,
                    pa.Ward.Description)
            )),
            p.BedAssignments.Select(pb => new BedAssignmentDto(
                pb.Id,
                pb.From,
                pb.To,
                new BedDto(
                    pb.Bed.Id,
                    new BedTypeDto(
                        pb.Bed.BedType.Id,
                        pb.Bed.BedType.Name,
                        pb.Bed.BedType.Description
                    ),
                    new RoomDto(
                        pb.Bed.Room.Id,
                        pb.Bed.Room.HasTv,
                        new WardDto(
                            pb.Bed.Room.Ward.Id,
                            pb.Bed.Room.Ward.Name,
                            pb.Bed.Room.Ward.Description
                        )

                    )

                )
            ))
        )).ToListAsync();
    }

    public Task<PatientDetailsDto> GetPatientByNameAsync(string name, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}