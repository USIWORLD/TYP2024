using FarmacyAPI;
using FarmacyAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<FarmacyService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
 {
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  
});

builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP request pipeline.
//  if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(
c =>
{
    string basePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
    c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "CodeX API");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();