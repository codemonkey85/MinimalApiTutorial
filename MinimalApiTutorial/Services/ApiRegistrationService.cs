namespace MinimalApiTutorial.Services;

public class ApiRegistrationService
{
    private AppDbContext _appDbContext;

    public ApiRegistrationService(AppDbContext appDbContext) => _appDbContext = appDbContext;

    public void RegisterApis(IEndpointRouteBuilder app)
    {
        app.RegisterTodosApi(_appDbContext);
    }
}
