using E_CommerceAPI.Application;
using E_CommerceAPI.Application.Validators.Products;
using E_CommerceAPI.Infrastructure;
using E_CommerceAPI.Infrastructure.Filters;
using E_CommerceAPI.Infrastructure.Services.Storage.Local;
using E_CommerceAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddStorage<LocalStorage>();
//builder.Service.AddStorage<AzureStorage>();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy=>
policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));

builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(cfg=>cfg.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options=>options.SuppressModelStateInvalidFilter=true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin",options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
    };
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
