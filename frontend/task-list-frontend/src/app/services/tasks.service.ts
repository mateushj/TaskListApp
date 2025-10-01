import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TaskItem } from '../models/task-item.model';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  private apiUrl = '/api/TaskItem';

  constructor(private http: HttpClient) {}

  getTasks(): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(this.apiUrl);
  }

  addTask(task: TaskItem): Observable<TaskItem> {
    return this.http.post<TaskItem>(this.apiUrl, task);
  }

  updateTask(task: TaskItem): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}`, task);
  }

  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
