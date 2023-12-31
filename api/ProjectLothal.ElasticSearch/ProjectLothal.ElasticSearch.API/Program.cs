using ProjectLothal.ElasticSearch.API.Extensions;
using ProjectLothal.Elastic.Application;
using ProjectLothal.Elastic.Persistance;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();
builder.Services.AddElastic(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddPersistanceServices();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


