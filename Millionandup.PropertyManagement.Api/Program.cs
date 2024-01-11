using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Domain.Entities;
using Millionandup.PropertyManagement.Infrastructure.Base;
using Millionandup.PropertyManagement.Infrastructure.DI;
using Millionandup.PropertyManagement.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Millionandup.PropertyManagement.Api", Version = "v1" });
});

#region Odata
static IEdmModel GetModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<PropertyReadDto>("PropertyData");
    return builder.GetEdmModel();
}

void AddFormatters(IServiceCollection services)
{
    services.AddMvcCore(option =>
    {
        foreach (var outputFormatter in option.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
            outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }

        foreach (var inputFormatter in option.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
            inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }
    });
}

builder.Services.AddControllers().AddOData((op, df) =>
{
    op.Expand().Filter().OrderBy().Select().SetMaxTop(100);
    op.AddRouteComponents(GetModel());
});

AddFormatters(builder.Services);
#endregion

builder.Services.AddMvc(op => op.EnableEndpointRouting = false);
builder.Services.AddAutoMapper(typeof(Millionandup.PropertyManagement.Aplication.Automapper.AutoMapperProfile));

#region FluentValidation
//builder.Services.AddControllers().AddFluentValidation(cfg =>
//{
//    cfg.RegisterValidatorsFromAssemblyContaining<OwnerDtoValidator>();
//});

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
#endregion

#region App db and services configuration

var dbSettings = new DbSettings();
builder.Configuration.Bind("DbSetting", dbSettings);
builder.Services.AddSingleton(dbSettings);
DependencyInjectionProfile.RegisterProfile(builder.Services, builder.Configuration);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Millionandup.PropertyManagement.Api v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

#region Creacion de usurio de prueba
try
{
    IServiceScope scope = app.Services.CreateScope();
    IServiceProvider services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbContext>();
    var entity = context.Set<User>();
    if (!entity.Any())
    {
        await entity.AddAsync(new User { UserName = "Million.property", Password = "M3110n" });
        await context.SaveChangesAsync();
    }
}
catch (Exception e)
{
    throw;
}
#endregion

app.Run();
