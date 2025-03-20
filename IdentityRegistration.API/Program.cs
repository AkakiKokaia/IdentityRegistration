using IdentityRegistration.API;
using IdentityRegistration.Application.Configuration;
using IdentityRegistration.Application.Configuration.Middlewares;
using IdentityRegistration.Infrastructure;
using IdentityRegistration.Infrastructure.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApiLayer(builder.Configuration, builder.Environment)
                .AddApplicationLayer(builder.Configuration, builder.Environment)
                .AddInfrastructureLayer(builder.Configuration); // ✅ Ensures DbContext & Repositories are registered

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

using (IServiceScope scope = app.Services.CreateScope())
{
    IdentityRegistrationDbContext context = scope.ServiceProvider.GetRequiredService<IdentityRegistrationDbContext>();
    if (context != null)
    {
        await DbInitializer.InitializeDatabase(app.Services, context);
    }

    app.MapControllers();
}

app.UseMiddleware<ErrorHandlerMiddleware>()
   .UseRouting();

app.Run();
