using Microsoft.EntityFrameworkCore;
using UserMessengerService.Application.Middlewares;
using UserMessengerService.Application.Services;
using UserMessengerService.Domain.Models;
using UserMessengerService.Infrastructure.Data;
using UserMessengerService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(UserRegistrationProfile),
    typeof(UserInformationProfile), typeof(UserChangeProfile), typeof(IEnumerable<UserInformationProfile>));


builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository<UserModel>, UserRepository>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<UserService>();

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

app.MapControllers();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();