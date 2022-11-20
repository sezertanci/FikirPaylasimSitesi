using Persistence.Extensions;
using WebApi.Infrastructure.ActionFilters;
using FluentValidation.AspNetCore;
using Application.Extensions;
using WebApi.Infrastructure.Extensions;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => opt.Filters.Add<ValidationFilter>())//Controller'lara gelen istekleri filtreler
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;//Json çýktýsýnýn baþ harflerinde deðiþiklik yapmaz
    })
    .AddFluentValidation()
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyPolicy");

app.MapControllers();

//yeni migration varsa otomatik olarak iþleme alýr
using(var context = new FikirPaylasimSitesiContext())
{
    context.Database.Migrate();
}

app.Run();
