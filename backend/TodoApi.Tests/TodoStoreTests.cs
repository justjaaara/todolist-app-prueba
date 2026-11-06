using TodoApi;

namespace TodoApi.Tests;

public class TodoStoreTests
{
    [Fact]
    public void Agregar_CreaUnaTareaPendiente()
    {
        var store = new TodoStore();

        var todo = store.Agregar("Comprar pan");

        Assert.Equal("Comprar pan", todo.Titulo);
        Assert.False(todo.Completada);
        Assert.Single(store.Listar());
    }

    [Fact]
    public void Alternar_CambiaElEstadoDeLaTarea()
    {
        var store = new TodoStore();
        var todo = store.Agregar("Pagar la luz");

        var actualizada = store.Alternar(todo.Id);

        Assert.True(actualizada!.Completada);
    }

    [Fact]
    public void Eliminar_QuitaLaTarea()
    {
        var store = new TodoStore();
        var todo = store.Agregar("Llamar al banco");

        Assert.True(store.Eliminar(todo.Id));
        Assert.Empty(store.Listar());
    }

    [Fact]
    public void Pendientes_SoloCuentaLasQueNoEstanCompletadas()
    {
        var store = new TodoStore();
        store.Agregar("Tarea 1");
        store.Agregar("Tarea 2");
        var tercera = store.Agregar("Tarea 3");
        store.Alternar(tercera.Id);

        Assert.Equal(2, store.Pendientes());
    }
}
