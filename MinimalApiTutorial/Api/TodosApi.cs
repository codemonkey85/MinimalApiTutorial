namespace MinimalApiTutorial.Api;

public static class TodosApi
{
    private static AppDbContext _appDbContext;

    public static void RegisterTodosApi(this IEndpointRouteBuilder app, AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

        app.MapGet("/todos", GetAllTodosAsync);
        app.MapGet("/todos/{id:long}", GetTodoAsync);
        app.MapPost("/todos", CreateTodoAsync);
        app.MapPut("/todos", UpdateTodoAsync);
        app.MapDelete("/todos/{id:long}", DeleteTodoAsync);
    }

    private static async Task<IResult> GetAllTodosAsync() =>
        Results.Ok(await _appDbContext.Todos.ToArrayAsync());

    private static async Task<IResult> GetTodoAsync(long id) =>
        await _appDbContext.Todos.FindAsync(id) is Todo todo ? Results.Ok(todo) : Results.NotFound("Todo not found.");

    private static async Task<IResult> CreateTodoAsync(Todo todo)
    {
        _appDbContext.Todos.Add(todo);
        await _appDbContext.SaveChangesAsync();
        return Results.Ok(todo);
    }

    private static async Task<IResult> UpdateTodoAsync(Todo todo)
    {
        _appDbContext.Entry(todo).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
        return Results.Ok(todo);
    }

    private static async Task<IResult> DeleteTodoAsync(long id)
    {
        if (await _appDbContext.Todos.FindAsync(id) is Todo todo)
        {
            _appDbContext.Entry(todo).State = EntityState.Deleted;
            await _appDbContext.SaveChangesAsync();
            return Results.Ok();
        }
        return Results.NotFound("Todo not found.");
    }
}
