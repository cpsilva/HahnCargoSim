using HahnCargoSimAutomation.EndpointDefinitions.Configuration;
using HahnCargoSim.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddConcrete();
builder.Services.AddEndpointDefinitions(typeof(Program));

var app = builder.Build();
app.UseRouting();
//app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseEndpointDefinitions();

app.Run();