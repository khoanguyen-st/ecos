using System.Text.Json.Serialization;
using KAS.ECOS.API.Middlewares;
using KAS.ECOS.API.Services;
using KAS.ECOS.MIDDLEWARE;
using KAS.ECOS.SERVICE.Mapping.Application;
using KAS.ECOS.SERVICE.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ApplicationProfile));
builder.Services.AddDbContext<KAS.Entity.DB.ECOS.Entities.ECOSContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("POSTGRESQL"), b => b.MigrationsAssembly("KAS.ECOS.API")));
//builder.Services.AddScoped<KAS.Entity.DB.ECOS.Entities.ECOSContext>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();

var app = builder.Build();

//Create Code
//cd C:\Users\ducph\Documents\MyProject\KAS\KAS.ECOS\KAS.ECOS.Entity.DB
//dotnet ef dbcontext scaffold "Host=127.0.0.1;Database=KAS.ECOS;Username=kasEcos_user01;Password=123" Npgsql.EntityFrameworkCore.PostgreSQL --project KAS.Entity.DB.ECOS -f

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

//app.UseMiddleware<SessionMiddleware>();

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<Middleware>((builder.Configuration.GetValue<string>("isDebug") ?? "0").ToLower() == "1");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<KAS.Entity.DB.ECOS.Entities.ECOSContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.InnerException!=null?ex.InnerException.Message:ex.Message);
}
Console.ReadLine();
