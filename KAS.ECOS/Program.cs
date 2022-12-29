using System.Text;
using System.Text.Json.Serialization;
using KAS.ECOS.API.Middlewares;
using KAS.ECOS.API.Services;
using KAS.ECOS.MIDDLEWARE;
using KAS.ECOS.SERVICE.Mapping.Application;
using KAS.ECOS.SERVICE.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using KAS.ECOS.API.Modules;
using KAS.ECOS.API.Policy;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ECOS",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddAutoMapper(typeof(ApplicationProfile));
builder.Services.AddDbContext<KAS.Entity.DB.ECOS.Entities.ECOSContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("POSTGRESQL"), b => b.MigrationsAssembly("KAS.ECOS.API")), ServiceLifetime.Transient);
//builder.Services.AddScoped<KAS.Entity.DB.ECOS.Entities.ECOSContext>();

builder.Services.AddIdentity<EndUserList, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<ECOSContext>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddSingleton<IAuthorizationPolicyProvider, UserAuthorizePolicyProvider>();
builder.Services.AddSingleton<IAuthorizationHandler, UserAuthorizeHandler>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes((builder.Configuration["Authentication:SecretForKey"])))
        };
    });

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new CoreModule()));

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

app.UseAuthentication();

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
