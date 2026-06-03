using CW8.DTOs;
using CW8.Exceptions;
using CW8.Infrastructure;
using CW8.Models;
using Microsoft.EntityFrameworkCore;

namespace CW8.Services;

public class PatientService (Apbd8Context ctx): IPatientService {

    public async Task<IEnumerable<PatientDetailsDto>> GetPatientsAsync(string? search, CancellationToken cancellationToken) {
        
        IQueryable<Patient> query = ctx.Patients;
        
        if (!string.IsNullOrWhiteSpace(search)) {
            query = query.Where(p =>
                EF.Functions.Like(p.FirstName, $"%{search}%") ||
                EF.Functions.Like(p.LastName, $"%{search}%")
            );
        }
        
        return await query
            .OrderByDescending(p => p.Pesel)
            .Select(p => new PatientDetailsDto(
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
                )
            )
        )).ToListAsync(cancellationToken);
        
    }

    public async Task AssignBedAsync(string pesel, CreateBedAssignDto req, CancellationToken cancellationToken) {
        if(pesel.Length != 11)
            throw new InvalidPeselException("Pesel must be 11 characters long");
        
        if (!await ctx.Patients.AnyAsync(p => p.Pesel == pesel, cancellationToken)) 
            throw new PatientNotFoundException("Patient with requested pesel not found!");

        if (!await ctx.BedTypes.AnyAsync(b => b.Name == req.BedType, cancellationToken))
            throw new BedTypeNotFoundException("Requested BedType does not exist!");
        
        
        var bed = await ctx.Beds
            .Where(b => 
                b.BedType.Name == req.BedType && 
                b.Room.Ward.Name == req.Ward && 
                !b.BedAssignments.Any(ba =>
                    (req.To == null || ba.From < req.To) &&
                    (ba.To == null || req.From < ba.To))
                )
            .FirstOrDefaultAsync(cancellationToken);


        if (bed == null) {
            throw new BedNotAvailableException("requested bed is not available!");
        }
        
        var bedAssignment = new BedAssignment {
            PatientPesel =  pesel,
            BedId =  bed.Id,
            From = req.From,
            To = req.To
        };
        ctx.Add(bedAssignment);
        await ctx.SaveChangesAsync(cancellationToken);
        
    }
}


















