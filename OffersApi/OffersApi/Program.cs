using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using offers.Business.Implementation;
using offers.Business.Interface;
using offers.DataAccess.Implementation;
using offers.DataAccess.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IOfferBusiness, OfferBusiness>();
builder.Services.AddTransient<IOfferDataAccess, OfferDataAccess>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
