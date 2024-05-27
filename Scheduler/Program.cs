using Microsoft.EntityFrameworkCore;
using Scheduler;
using Scheduler.Repository;
using Scheduler.Services;


var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IStudentGroupProcessService, StudentGroupProcessService>();
builder.Services.AddTransient<IStudentGroupRepository, StudentGroupRepository>();
builder.Services.AddTransient<IDisciplineRepository, DisciplineRepository>();
builder.Services.AddTransient<IDepartmentWorkRepository, DepartmentWorkRepository>();
builder.Services.AddTransient<IDepartmentWorkProcessService, DepartmentWorkProcessService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
