using Museum.Persistense;
using Museum.Application;
using Museum.Application.Interfaces;
using AutoMapper;
using Museum.Application.Common.Mapping;
using System.Reflection;
using Museum.WebApi.Middleware;
using Museum.Domain;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IMuseumDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistanse(builder.Configuration);
builder.Services.AddControllers();



builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
        policy.WithExposedHeaders("content-disposition");
    });
});

builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    config.IncludeXmlComments(xmlPath);
    config.IncludeXmlComments(@"D:\VSProjects\Museam.Backend\Domain\bin\Debug\net8.0\Museum.Domain.xml");

    config.UseAllOfToExtendReferenceSchemas();
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
    catch (Exception exception)
    {

    }
}



if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request started: {context.Request.Method} {context.Request.Path}");
    try
    {
        await next();
    }
    finally
    {
        Console.WriteLine($"Request finished: {context.Request.Method} {context.Request.Path} - Status: {context.Response.StatusCode}");
    }
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseStaticFiles();

// Ваш обработчик исключений ДОЛЬШЕ UseRouting
app.UseCustomExceptionHandler();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = string.Empty;
    options.SwaggerEndpoint("swagger/v1/swagger.json", "Museum API");
});
app.Run();