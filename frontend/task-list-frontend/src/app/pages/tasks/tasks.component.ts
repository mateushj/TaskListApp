import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { TaskItem, TaskItemStatus, TaskStatusLabel } from '../../models/task-item.model';
import { TasksService } from '../../services/tasks.service';
import { CommonModule, formatDate } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { TaskDialogComponent } from '../../components/task-dialog/task-dialog.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';

@Component({
    selector: 'app-tasks',
    templateUrl: './tasks.component.html',
    styleUrls: ['./tasks.component.scss'],
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatButtonModule,
        MatIconModule,
        MatTableModule,
        MatPaginatorModule,
        MatSnackBarModule,
        MatTooltipModule,
        MatSortModule
    ],
})
export class TasksComponent implements OnInit, AfterViewInit {
    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;

    tasks: TaskItem[] = [];
    newTaskName = '';
    filterStatus: TaskItemStatus | 'Todos' = 'Todos';
    displayedColumns: string[] = ['title', 'description', 'dueDate', 'status', 'actions'];
    statusOptions = [
        TaskItemStatus.Todo,
        TaskItemStatus.InProgress,
        TaskItemStatus.Done
    ];
    taskStatuses = Object.values(TaskItemStatus)
        .filter(v => typeof v === 'number') as TaskItemStatus[];

    dataSource = new MatTableDataSource<TaskItem>([]);

    constructor(private taskService: TasksService, public dialog: MatDialog, private snackBar: MatSnackBar, private cdr: ChangeDetectorRef) { }

    ngOnInit() {
        this.dataSource.filterPredicate = (data: any, filter: string) => {
            const taskStatus = String(data.status).toLowerCase();
            return taskStatus.includes(filter);
        };
        this.loadTasks();
    }

    ngAfterViewInit() {
        if (this.paginator) {
            this.dataSource.paginator = this.paginator;
        }
        if (this.sort) {
            this.dataSource.sort = this.sort;
        }
        this.cdr.detectChanges();
    }

    loadTasks() {
        this.taskService.getTasks().subscribe(tasks => {
            this.tasks = tasks;
            this.dataSource.data = tasks;

            if (this.dataSource.paginator) {
                this.dataSource.paginator.firstPage();
            }
        });
    }

    addTask(task: TaskItem) {
        this.taskService.addTask(task).subscribe({
            next: created => {
                this.tasks = [created, ...this.tasks];
                this.dataSource.data = this.tasks;
                this.snackBar.open('✅ Tarefa criada com sucesso!', 'Fechar', {
                    duration: 3000,
                    horizontalPosition: 'right',
                    verticalPosition: 'top',
                });
            },
            error: () => {
                this.snackBar.open('❌ Erro ao criar tarefa.', 'Fechar', {
                    duration: 3000,
                    horizontalPosition: 'right',
                    verticalPosition: 'top',
                    panelClass: ['snackbar-error']
                });
            }
        });
    }

    updateTask(task: TaskItem) {
        this.taskService.updateTask(task).subscribe({
            next: () => {
                const index = this.tasks.findIndex(t => t.id === task.id);
                if (index !== -1) {
                    this.tasks[index] = { ...task };
                    this.tasks = [...this.tasks];
                    this.dataSource.data = this.tasks;
                }
                this.snackBar.open('✅ Tarefa atualizada!', 'Fechar', {
                    duration: 3000,
                    horizontalPosition: 'right',
                    verticalPosition: 'top',
                });
            },
            error: () => {
                this.snackBar.open('❌ Erro ao atualizar tarefa.', 'Fechar', {
                    duration: 3000,
                    horizontalPosition: 'right',
                    verticalPosition: 'top',
                    panelClass: ['snackbar-error']
                });
            }
        });
    }

    deleteTask(task: TaskItem) {
        this.taskService.deleteTask(task.id).subscribe({
            next: () => {
                this.tasks = this.tasks.filter(t => t.id !== task.id);
                this.dataSource.data = this.tasks;
                this.snackBar.open('✅ Tarefa removida!', 'Fechar', {
                    duration: 3000,
                    horizontalPosition: 'right',
                    verticalPosition: 'top',
                });
            },
            error: () => {
                this.snackBar.open('❌ Erro ao remover tarefa.', 'Fechar', {
                    duration: 3000,
                    horizontalPosition: 'right',
                    verticalPosition: 'top',
                    panelClass: ['snackbar-error']
                });
            }
        });
    }

    openDialog(task?: TaskItem): void {
        const dialogRef = this.dialog.open(TaskDialogComponent, {
            width: '90%',  
            maxWidth: '600px',
            maxHeight: '100vh',  
            autoFocus: false,
            data: task ? { ...task } : {}
        });
        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                if (result.id) {
                    this.updateTask(result);
                } else {
                    this.addTask(result);
                }
            }
        });
    }

    getStatusLabel(status: TaskItemStatus) {
        return TaskStatusLabel[status];
    }

    statusClass(status: TaskItemStatus) {
        switch (status) {
            case TaskItemStatus.Todo: return 'status-badge status-todo';
            case TaskItemStatus.InProgress: return 'status-badge status-inprogress';
            case TaskItemStatus.Done: return 'status-badge status-done';
            default: return '';
        }
    }

    formatDate(date?: Date | string): string {
        return date ? formatDate(date, 'dd/MM/yyyy', 'pt-BR') : "";
    }

    isTruncated(element: HTMLElement): boolean {
        return element.scrollWidth > element.clientWidth;
    }

    applyStatusFilter() {
        const filterValue = this.filterStatus === 'Todos' ? '' : String(this.filterStatus);

        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }

}
