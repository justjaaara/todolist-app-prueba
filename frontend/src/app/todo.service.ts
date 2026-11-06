import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

export interface Todo {
  id: number;
  titulo: string;
  completada: boolean;
}

@Injectable({ providedIn: 'root' })
export class TodoService {
  private readonly http = inject(HttpClient);
  private readonly url = '/api/todos';

  listar(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.url);
  }

  pendientes(): Observable<{ cantidad: number }> {
    return this.http.get<{ cantidad: number }>(`${this.url}/pendientes`);
  }

  agregar(titulo: string): Observable<Todo> {
    return this.http.post<Todo>(this.url, { titulo });
  }

  alternar(id: number): Observable<Todo> {
    return this.http.put<Todo>(`${this.url}/${id}/alternar`, {});
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
