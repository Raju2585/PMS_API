using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Application.Services;
using PMS.Domain;
using PMS.Infra;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IApplicationDbContext,ApplicationDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDb"),
b=>b.MigrationsAssembly("PMS.Api")
));

builder.Services.AddScoped<IPatientService,PatientService>();
builder.Services.AddScoped<IPatientRepository,PatientRepository>();
builder.Services.AddScoped<IDeviceService,DeviceService>();
builder.Services.AddScoped<IDeviceRepository,DeviceRepository>();
builder.Services.AddScoped<IVitalSignService,VitalSignService>();
builder.Services.AddScoped<IVitalSignRepository,VitalSignRepository>();
builder.Services.AddScoped<IMedicalhistoryService,MedicalHistoryService>();
builder.Services.AddScoped<IMedicalHistoryRepository,MedicalHistoryRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
