using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Business;
using WebApplication1.Model.Interfaces;
using WebApplication1.Repository;
using WebApplication1.Repository.Context;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os de controle
builder.Services.AddControllers();

// Aprende mais sobre como configurar o Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inje��o de depend�ncia
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<IStudentBusiness, StudentBusiness>();
builder.Services.AddScoped<ISubjectBusiness, SubjectBusiness>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

// Configura o DbContext para usar o SQL Server
builder.Services.AddDbContext<StudentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoDBPlaca")));

// Habilita o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:3000") // Permite apenas a origem especificada
                          .AllowAnyMethod() // Permite qualquer m�todo HTTP
                          .AllowAnyHeader()); // Permite qualquer cabe�alho
});

// Cria o aplicativo
var app = builder.Build();

// Configura o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica a pol�tica de CORS
app.UseCors("AllowOrigin");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Inicia o aplicativo
app.Run();
