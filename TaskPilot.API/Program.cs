using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskPilot.Application.UseCases.ProjectUseCases;
using TaskPilot.Application.UseCases.ReportsUseCases;
using TaskPilot.Application.UseCases.TaskUseCases;
using TaskPilot.Domain.Interfaces;
using TaskPilot.Infrastructure.Context;
using TaskPilot.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskPilot API",
        Version = "v1",
        Description = "TaskPilot é uma API moderna e intuitiva para gerenciamento de tarefas, criada para ajudar equipes a organizar, monitorar e colaborar em suas atividades diárias."
    });

});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddHealthChecks();

// Repositórios
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();

// Use Cases de Task
builder.Services.AddScoped<CreateTaskUseCase>();
builder.Services.AddScoped<DeleteTaskUseCase>();
builder.Services.AddScoped<UpdateTaskUseCase>();
builder.Services.AddScoped<GetTasksByProjectUseCase>();

// Use Cases de Project
builder.Services.AddScoped<CreateProjectUseCase>();
builder.Services.AddScoped<DeleteProjectUseCase>();
builder.Services.AddScoped<UpdateProjectUseCase>();
builder.Services.AddScoped<GetProjectByIdUseCase>();
builder.Services.AddScoped<GetAllProjectsUseCase>();

// Use Case de relatório de desempenho
builder.Services.AddScoped<GetPerformanceReportUseCase>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskPilot API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/health");

app.Run();