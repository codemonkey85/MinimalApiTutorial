var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var appSettings = config.Get<AppSettings>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<AppDbContext>
(
    options => _ = appSettings.DbProvider switch
    {
        "sqlite" => options.UseSqlite(config.GetConnectionString("sqlite")),
        "sqlserver" => options.UseSqlServer(config.GetConnectionString("sqlserver")),
        _ => throw new Exception($"Unsupported provider: {appSettings.DbProvider}")
    }
);

builder.Services.AddScoped<ApiRegistrationService>();

var app = builder.Build();

var apiRegistrationService = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApiRegistrationService>();
apiRegistrationService.RegisterApis(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
