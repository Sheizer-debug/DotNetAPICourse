var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((options)=>{
    options.AddPolicy("DevCors",(corsBuilder)=>{
        corsBuilder.WithOrigins("http://localhost:4200","http://localhost:3000","http://localhost:8000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();//default  origins for angular, react and vue

    });
    options.AddPolicy("ProdCors",(corsBuilder)=>{
        corsBuilder.WithOrigins("https:/myproductionsite.com")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();//default  origins for angular, react and vue
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("ProdCors");
    app.UseHttpsRedirection();//as we dont have ssl certificate in development
}

app.MapControllers();

// app.MapGet("/weatherforecast", () =>
// {

// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.Run();
