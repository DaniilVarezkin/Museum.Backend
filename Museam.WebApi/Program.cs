using Museum.Application;
using Museum.Application.Common.Mappings;
using Museum.Application.Interfases;
using Museum.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IMuseumDbContext).Assembly));
});

builder.Services.AddApplicaton();
builder.Services.AddPersistance(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<MuseumDbContext>();
        DbInitializer.Initialize(context);
    } 
    catch (Exception ex)
    {

    }
}

//Configure the HTTP request pipeline.

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapControllers();



app.Run();
