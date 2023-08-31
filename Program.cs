using Microsoft.EntityFrameworkCore;

using RankingAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//var configuration = new ConfigurationBuilder()
//               .SetBasePath(Directory.GetCurrentDirectory())
//               .AddJsonFile("appsettings.json")
//               .Build();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//UseInMemoryDataBase
builder.Services.AddDbContext<ItemContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("RankApiConnection")));
builder.Services.AddDbContext<TierContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("RankApiConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var provider = builder.Services.BuildServiceProvider();
//var configuration = provider.GetRequiredService<IConfiguration>();

//Register Database Context



//builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<TierContext>());

builder.Services.AddCors(options =>
{
    var frontEndURL = builder.Configuration.GetSection("frontendURL")["devFrontendURL"].ToString();
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontEndURL).AllowAnyMethod().AllowAnyHeader();
    });
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
