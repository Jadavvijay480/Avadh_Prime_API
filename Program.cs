using AVADH_PRIME_API.Data;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<HostelRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<StudentAttendanceRepository>();
builder.Services.AddScoped<StudentComplaintsRepository>();
builder.Services.AddScoped<StudentFeesRepository>();
builder.Services.AddScoped<VisitorsRepository>();
builder.Services.AddScoped<WardenRepository>();
builder.Services.AddScoped<FeesReceiptsRepository>();
builder.Services.AddScoped<RoomsAllocationRepository>();








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
