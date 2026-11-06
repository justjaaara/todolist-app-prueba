namespace TodoApi;

// Almacén en memoria: al reiniciar el servidor, la lista vuelve a quedar vacía.
public class TodoStore
{
    private readonly List<Todo> _todos = new();
    private int _siguienteId = 1;

    public IReadOnlyList<Todo> Listar() => _todos.ToList();

    public Todo Agregar(string titulo)
    {
        var todo = new Todo(_siguienteId++, titulo.Trim(), Completada: false);
        _todos.Add(todo);
        return todo;
    }

    public Todo? Alternar(int id)
    {
        var indice = _todos.FindIndex(t => t.Id == id);
        if (indice < 0) return null;

        _todos[indice] = _todos[indice] with { Completada = !_todos[indice].Completada };
        return _todos[indice];
    }

    public bool Eliminar(int id) => _todos.RemoveAll(t => t.Id == id) > 0;

    public int Pendientes() => _todos.Count(t => !t.Completada);
}
