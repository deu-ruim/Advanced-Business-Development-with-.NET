using GB1.Application.UseCases;
using GB1.Application.Validators;
using GB1.Domain.Entitiy;
using GB1.Infrastructure.Context;
using GB1.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco Oracle
builder.Services.AddDbContext<DeuRuimContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDeuRuim")));

// Adiciona controladores e configura JsonOptions
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Conversor customizado para DateTime no formato dd/MM/yyyy HH:mm:ss
        options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

// Registro de dependências
builder.Services.AddScoped<IRepository<Usuario>, Repository<Usuario>>();
builder.Services.AddScoped<IRepository<Desastre>, Repository<Desastre>>();
builder.Services.AddScoped<CreateUsuarioRequestValidator>();
builder.Services.AddScoped<CreateDesastreRequestValidator>();
builder.Services.AddScoped<UsuarioUseCase>();
builder.Services.AddScoped<DesastreUseCase>();

// Configuração para remover a validação automática do ModelState
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Swagger/OpenAPI
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = builder.Configuration["Swagger:Title"] ?? "API de Desastres Ambientais",
        Description = "Um informativo de desastres",
        Contact = new OpenApiContact
        {
            Email = "luizhneri12@gmail.com",
            Name = "Luiz Henrique Neri Reimberg"
        }
    });

    // Inclusão do arquivo XML para documentação Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swagger.IncludeXmlComments(xmlPath);

    // Mostrar enums como string no Swagger
    var enumTypes = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsEnum && t.Namespace != null && t.Namespace.Contains("GB1.Domain.Enums"));

    foreach (var enumType in enumTypes)
    {
        swagger.MapType(enumType, () => new OpenApiSchema
        {
            Type = "string",
            Enum = Enum.GetNames(enumType)
                       .Select(name => new OpenApiString(name))
                       .Cast<IOpenApiAny>()
                       .ToList()
        });
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


// Conversor de DateTime para formato dd/MM/yyyy HH:mm:ss
public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _dateFormat = "dd/MM/yyyy HH:mm:ss";
    private readonly CultureInfo _culture = CultureInfo.InvariantCulture;

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();

        if (DateTime.TryParseExact(stringValue, _dateFormat, _culture, DateTimeStyles.None, out var date))
        {
            return date;
        }

        throw new JsonException($"Data inválida. Esperado no formato {_dateFormat}.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateFormat, _culture));
    }
}
