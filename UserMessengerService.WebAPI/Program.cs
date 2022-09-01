using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UserMessengerService.Application.Middlewares;
using UserMessengerService.Application.Services;
using UserMessengerService.Domain.Models;
using UserMessengerService.Infrastructure.Data;
using UserMessengerService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer("TokenKey", cfg =>
    {
        cfg.RequireHttpsMetadata = true;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            RequireExpirationTime = false,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(UserRegistrationProfile),
    typeof(UserInformationProfile), typeof(UserChangeProfile), typeof(IEnumerable<UserInformationProfile>));


builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserRepository<UserModel>, UserRepository>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<IUserProviderMiddleware, UserProviderMiddleware>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();