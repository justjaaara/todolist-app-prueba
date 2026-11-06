using TodoApi;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5080");
builder.Services.AddSingleton<TodoStore>();

var app = builder.Build();

var api = app.MapGroup("/api/todos");

api.MapGet("/", (TodoStore store) => store.Listar());

api.MapGet("/pendientes", (TodoStore store) => new { cantidad = store.Pendientes() });

api.MapPost("/", (NuevoTodo nuevo, TodoStore store) =>
{
    if (string.IsNullOrWhiteSpace(nuevo.Titulo))
        return Results.BadRequest(new { error = "El título es obligatorio." });

    var todo = store.Agregar(nuevo.Titulo);
    return Results.Created($"/api/todos/{todo.Id}", todo);
});

api.MapPut("/{id:int}/alternar", (int id, TodoStore store) =>
    store.Alternar(id) is { } todo ? Results.Ok(todo) : Results.NotFound());

api.MapDelete("/{id:int}", (int id, TodoStore store) =>
    store.Eliminar(id) ? Results.NoContent() : Results.NotFound());

app.Run();

record NuevoTodo(string Titulo);
