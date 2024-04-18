using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BAL.Interfaces;
using BAL.Services;
using Serilog;
using Data;
using BAL.Repository;
using BAL.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

// Add services to the container.
builder.Services.AddControllers();

//Cors Config
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173", "https://dev-sigmacloud-app.azurewebsites.net")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
    

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestapBehavior", true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SigmaproIisContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("MyDbContext"));
});
builder.Services.AddDbContext<SigmaproIisContextUdf>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("MyDbContextudf"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IPatientService, PatientRepository>();
builder.Services.AddScoped<IInventoryService, InventoryRepository>();

builder.Services.AddScoped(typeof(IDataAccessProvider<>), typeof(DataAccessProvider<>));
builder.Services.AddScoped<IMasterDataService, MasterDataService>();
builder.Services.AddScoped<IAddressesService, AddressesService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IAddressesRepository, AddressesRepository>();
builder.Services.AddScoped<IBusinessConfigurationRepository, BusinessConfigurationRepository>();
builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();
builder.Services.AddScoped<IContactsRepository, ContactsRepository>();
builder.Services.AddScoped<ICountiesRepository, CountiesRepository>();
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IFacilitiesRepository, FacilitiesRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IJuridictionsRepository, JuridictionsRepository>();
builder.Services.AddScoped<ILOVTypeMasterRepository, LovTypeMasterRepository>();
builder.Services.AddScoped<IOrganizationsRepository, OrganizationsRepository>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IStatesRepository, StatesRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "(Dev)Sigma Health Pro API V1");
        // Specify an optional route prefix
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "(Stage)Sigma Health Pro API V1");
        // Specify an optional route prefix
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Add this line to enable authentication
app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();
app.UseCors("AllowSpecificOrigin");

app.MapGet("/", (SigmaproIisContext context) => { return context; });

app.Run();
