import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Todo, TodoService } from './todo.service';

@Component({
  imports: [FormsModule],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App implements OnInit {
  private readonly service = inject(TodoService);

  protected readonly todos = signal<Todo[]>([]);
  protected readonly pendientes = signal(0);
  protected readonly error = signal('');
  protected nuevoTitulo = '';

  ngOnInit(): void {
    this.cargar();
  }

  protected agregar(): void {
    const titulo = this.nuevoTitulo.trim();
    if (!titulo) return;

    this.service.agregar(titulo).subscribe({
      next: () => {
        this.nuevoTitulo = '';
        this.cargar();
      },
      error: () => this.error.set('No se pudo agregar la tarea.'),
    });
  }

  protected alternar(todo: Todo): void {
    this.service.alternar(todo.id).subscribe(() => this.cargar());
  }

  protected eliminar(todo: Todo): void {
    this.service.eliminar(todo.id).subscribe(() => this.cargar());
  }

  private cargar(): void {
    this.error.set('');
    this.service.listar().subscribe({
      next: (todos) => this.todos.set(todos),
      error: () => this.error.set('No se pudo conectar con el servidor. ¿Está corriendo el backend?'),
    });
    this.service.pendientes().subscribe({
      next: (r) => this.pendientes.set(r.cantidad),
    });
  }
}
