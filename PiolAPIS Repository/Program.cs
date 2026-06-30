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
builder.Services.AddScoped<GetAllUsersUseCase>();
builder.Services.AddScoped<UpdateUserUseCase>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<CreateProjectUseCase>();
builder.Services.AddScoped<DeleteProjectUseCase>();
builder.Services.AddScoped<GetAllProjectsUseCase>();
builder.Services.AddScoped<GetProjectByIdUseCase>();
builder.Services.AddScoped<UpdateProjectUseCase>();

builder.Services.AddScoped<IDocumentationRepository, DocumentationRepository>();
builder.Services.AddScoped<CreateDocumentationUseCase>();
builder.Services.AddScoped<ChangeDocumentationStatusUseCase>();
builder.Services.AddScoped<DeleteDocumentationUseCase>();
builder.Services.AddScoped<GetAllDocumentationsUseCase>();
builder.Services.AddScoped<GetDocumentationByIdUseCase>();
builder.Services.AddScoped<UpdateDocumentationUseCase>();

builder.Services.AddScoped<IVariableRepository, VariableRepository>();
builder.Services.AddScoped<CreateVariableUseCase>();
builder.Services.AddScoped<ChangeVariableStatusUseCase>();
builder.Services.AddScoped<DeleteVariableUseCase>();
builder.Services.AddScoped<GetAllVariablesUseCase>();
builder.Services.AddScoped<GetVariableByIdUseCase>();
builder.Services.AddScoped<UpdateVariableUseCase>();

builder.Services.AddScoped<ICodeMessageRepository, CodeMessageRepository>();
builder.Services.AddScoped<CreateCodeMessageUseCase>();
builder.Services.AddScoped<GetCodeMessageByIdUseCase>();

builder.Services.AddScoped<IDocumentationSettingRepository, DocumentationSettingRepository>();
builder.Services.AddScoped<CreateDocumentationSettingUseCase>();
builder.Services.AddScoped<ChangeDocumentationSettingStatusUseCase>();
builder.Services.AddScoped<DeleteDocumentationSettingUseCase>();
builder.Services.AddScoped<GetAllDocumentationSettingsUseCase>();
builder.Services.AddScoped<GetDocumentationSettingByIdUseCase>();
builder.Services.AddScoped<UpdateDocumentationSettingUseCase>();

builder.Services.AddScoped<ITemplatesDTOsRepository, TemplatesDTOsRepository>();
builder.Services.AddScoped<CreateTemplatesDTOsUseCase>();
builder.Services.AddScoped<GetTemplateByIdUseCase>();
builder.Services.AddScoped<GetAllTemplatesUseCase>();
builder.Services.AddScoped<UpdateTemplateUseCase>();
builder.Services.AddScoped<DeleteTemplateUseCase>();
builder.Services.AddScoped<ChangeTemplateStatusUseCase>();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendCorsPolicy", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "https://019f15d7-7261-74a5-9a69-015663f45cc2-piolapis.linapps.online",
            "https://piolapis.linapps.online"
        )
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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PiolapisDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            Console.WriteLine("--> Aplicando migraciones pendientes en Supabase...");
            context.Database.Migrate();
            Console.WriteLine("--> ¡Migraciones aplicadas con éxito!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--> Error al aplicar migraciones automáticas: {ex.Message}");
    }
}

app.Run();
