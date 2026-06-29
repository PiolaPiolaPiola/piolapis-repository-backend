using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Infraestructure.Adapters;
using PiolAPIS_Repository.Infraestructure.Persistence;
using PiolAPIS_Repository.Application.Ports.User;
using PiolAPIS_Repository.Application.Ports.Project;
using PiolAPIS_Repository.Application.Ports.Documentation;
using PiolAPIS_Repository.Application.Ports.Variable;
using PiolAPIS_Repository.Application.Ports.CodeMessage;
using PiolAPIS_Repository.Application.Ports.DocumentationSetting;
using PiolAPIS_Repository.Application.Ports.TemplateDTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PiolapisDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<GetUserByIdUseCase>();
builder.Services.AddScoped<DeleteUserUseCase>();
builder.Services.AddScoped<ChangeUserStatusUseCase>();
builder.Services.AddScoped<GetAllUsersByRoleUseCase>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<CreateProjectUseCase>();

builder.Services.AddScoped<IDocumentationRepository, DocumentationRepository>();
builder.Services.AddScoped<CreateDocumentationUseCase>();

builder.Services.AddScoped<IVariableRepository, VariableRepository>();
builder.Services.AddScoped<CreateVariableUseCase>();

builder.Services.AddScoped<ICodeMessageRepository, CodeMessageRepository>();
builder.Services.AddScoped<CreateCodeMessageUseCase>();

builder.Services.AddScoped<IDocumentationSettingRepository, DocumentationSettingRepository>();
builder.Services.AddScoped<CreateDocumentationSettingUseCase>();

builder.Services.AddScoped<ITemplatesDTOsRepository, TemplatesDTOsRepository>();
builder.Services.AddScoped<CreateTemplatesDTOsUseCase>();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/openapi/v1.json", "PiolAPIS v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("FrontendCorsPolicy");

app.MapControllers();

app.Run();
