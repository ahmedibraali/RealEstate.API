using FluentValidation.AspNetCore;
using RealEstate.Application.Repository.Interfaces;
using RealEstate.Application.Repository;
using RealEstate.Application.Service.Interfaces;
using RealEstate.Application.Service;
using RealEstate.Structure.Dto.Request;
using RealEstate.Structure.Validators;
using FluentValidation;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddFluentValidation();
      

        var configuration = builder.Configuration;
        configuration.AddUserSecrets<Program>();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
        });
        builder.Services.AddScoped<IValidator<ReferenceDataRequestDto>, AddReferenceDataValidator>();
        builder.Services.AddScoped<IValidator<CustomerRequestDto>, AddCustomerValidator>();
        builder.Services.AddScoped<IValidator<AgentRequestDto>, AddAgentValidator>();

        ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

        builder.Services.AddScoped<IReferenceDataService, ReferenceDataService>();
        builder.Services.AddScoped<IRealEstateService, RealEstateService>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IAgentService, AgentService>();

        builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();
        builder.Services.AddScoped<IReferenceDataRepository, ReferenceDataRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IAgentRepository, AgentRepository>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}