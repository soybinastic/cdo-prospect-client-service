

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Infrastructure service contains all services have registered here
// eg. Data Access, Identity and ect..
builder.Services.AddInfrastructure(builder.Configuration);

// add application layer to container
builder.Services.AddApplication();


builder.Services.AddControllers()
    .AddJsonOptions(opyions => 
        opyions.JsonSerializerOptions.ReferenceHandler =  ReferenceHandler.IgnoreCycles);

// CORS configuartion
builder.Services.AddCors(config => 
    config.AddDefaultPolicy(options => 
        options
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
