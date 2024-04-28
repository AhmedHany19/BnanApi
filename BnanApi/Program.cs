using BnanApi.DTOS;
using BnanApi.Models;
using BnanApi.Services.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BnanKSAContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", build =>
{
    build.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
                    .SetIsOriginAllowed(hostName => true);
}
));

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddTransient<IMailingService, MailingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
