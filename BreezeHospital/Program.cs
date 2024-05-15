using Breeze.Application.Abstractions.IRepositories.IEntityRepos;
using Breeze.Application.Abstractions.IRepositories;
using Breeze.Application.Abstractions.IUnitOfWorks;
using Breeze.Persistance.Implementations.RepoImplementations.EntitiyRepos;
using Breeze.Persistance.Implementations.RepoImplementations;
using Breeze.Persistance.Implementations.UnitOfWorkImplementations;
using Breeze.Persistance.MyDbContext;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Breeze.Application.Profiles;
using Breeze.Application.Exceptions;
using Breeze.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Breeze.Application.Abstractions.IServices;
using Breeze.Persistance.Implementations.ServiceImplementations;
using Serilog.Events;
using Serilog;
using System.Collections.ObjectModel;
using System.Data;
using Serilog.Core;
using FluentValidation.AspNetCore;
using BreezeHospitalWebApp.Controllers;
using Breeze.Application.Attributes;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using FluentValidation;
using Breeze.Domain.Entities;
using Breeze.Application.DTOs;
using Breeze.Application.Validators.AnalysValids;
using Breeze.Application.DTOs.DoctorOperationsDtos;
using Breeze.Application.Validators.DoctorOperationValids;
using Breeze.Application.Validators.DoctorValids;
using Breeze.Application.Validators.PatientValids;
using Breeze.Application.Validators.OperationValids;
using Breeze.Application.DTOs.DoctorPatientDtos;
using Breeze.Application.Validators.DoctorPatientValids;
using Breeze.Application.DTOs.IdentityDtos;
using Breeze.Application.Validators.IdentityValids;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//swaggerin gorunusunu deyisirik------------------------------------------------------------------------
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation  
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Breeze Hospital Final Project",
        Description = "ASP.NET Core 6 Web API"
    });
    // To Enable authorization using Swagger (JWT)  
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});
//---------------------------------------------------------------------------
//add authentification---------------------------------------------------------------
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{

    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        LifetimeValidator = (notBefore, expires, securitToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role


    };
});
//--------------------------authentification---------------------------------------------------------
var conf = builder.Configuration.GetConnectionString("AppDbContext");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conf).UseLazyLoadingProxies());
//identity
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped(typeof(ValidateModelAttribute));
builder.Services.AddAutoMapper(typeof(MyMapper).Assembly);
builder.Services.AddScoped<IAnalysService, AnalysService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDoctorPatientService, DoctorPatientService>();
builder.Services.AddScoped<IDoctorOperationService, DoctorOperationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthoService, AuthoService>();
builder.Services.AddScoped<ITokenHandler, Breeze.Persistance.Implementations.ServiceImplementations.TokenHandler>();
//validasiyalar
builder.Services.AddScoped<IValidator<AnalysDto>,AnalysesDtoValidator>();
builder.Services.AddScoped<IValidator<DoctorOperationAddDto>, DoctorOperationAddDtoValidator>();
builder.Services.AddScoped<IValidator<DoctorOperationDto>, DoctorOperationDtoValidator>();
builder.Services.AddScoped<IValidator<DoctorPatientAddDto>, DoctorPatientAddDtoValidator>();
builder.Services.AddScoped<IValidator<DoctorPatientDto>, DoctorPatientDtoValidator>();
builder.Services.AddScoped<IValidator<DoctorsDto>, DoctorsDtoValidator>();
builder.Services.AddScoped<IValidator<PatientsDto>, PatientsDtoValidator>();
builder.Services.AddScoped<IValidator<OperationDto>, OperationDtoValidator>();
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
//validasiyalar
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDocPatientRepository, DocPatientRepository>();
builder.Services.AddScoped<IDocOperRepository, DocOperRepository>();
builder.Services.AddScoped<IAnalysRepository, AnalysRepository>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Serilog---------------------------------------
Logger? log = new LoggerConfiguration()
    .WriteTo.Console(LogEventLevel.Debug)
    .WriteTo.File("Logs/myJsonLogs.json")
    .WriteTo.File("logs/mylogs.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

Log.Logger = log;
builder.Host.UseSerilog(log);
//Serilog-------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureExtentionHandler();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
