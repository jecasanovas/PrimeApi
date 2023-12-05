using System.Data;
using System.Reflection;
using API.Middleware;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.Reposititories;
using BLL.Services;
using Core.DBContext;
using Courses.API.Extensions;
using Courses.Helpers;

using Identity.DbContext;
using Identity.Extensions;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddApplicationServices();

builder.Services.AddDbContext<GestionCursosContext>(opt =>
{

    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("PrimeApi"));
});

builder.Services.AddDbContext<AppIdentityDbContext>(x =>
{
    object p = x.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"), b => b.MigrationsAssembly("PrimeApi"));
});

builder.Services.AddIdentityCore<AppUser>()
  .AddEntityFrameworkStores<AppIdentityDbContext>()
  .AddDefaultTokenProviders();


builder.Services.AddApplicationServices();





builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsRule", rule =>
    {
        rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
    });
});


builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICourseService, CoursesService>();
builder.Services.AddScoped<IPaymentInfoService, PaymentInfoService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ITechnologyService, TechnologyService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IUserInfoService, UserService>();
builder.Services.AddScoped<IAddressesService, AddressesService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");



app.UseCors("CorsRule");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();

