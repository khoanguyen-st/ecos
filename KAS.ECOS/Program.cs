



using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<KAS.Entity.DB.ECOS.KASECOSContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("POSTGRESQL")));
var app = builder.Build();

//Create Code
//cd C:\Users\ducph\Documents\MyProject\KAS\KAS.ECOS\KAS.ECOS.Entity.DB
//dotnet ef dbcontext scaffold "Host=127.0.0.1;Database=KAS.ECOS;Username=kasEcos_user01;Password=123" Npgsql.EntityFrameworkCore.PostgreSQL -f



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ClassLibrary1.Middeware>((builder.Configuration.GetValue<string>("isDebug") ?? "0").ToLower() == "1");

app.Run();
