using Microsoft.EntityFrameworkCore;
using CW8.Services;
using CW8.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IHospitalService, HospitalService>();

builder.Services.AddDbContext<Apbd8Context>(opt => 
    opt.UseSqlServer(builder
        .Configuration
        .GetConnectionString("DefaultConnection"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "CW8 API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
