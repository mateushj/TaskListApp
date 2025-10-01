import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { TaskItem } from '../../models/task-item.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';

@Component({
  selector: 'app-task-dialog',
  imports: [
        CommonModule,
        FormsModule,
        MatInputModule,
        MatSelectModule,
        MatButtonModule,
        MatDatepickerModule,
        MatDialogModule
    ],
  templateUrl: './task-dialog.component.html',
  styleUrls: ['./task-dialog.component.scss']
})
export class TaskDialogComponent {
  localTask: Partial<TaskItem>;

  constructor(
    public dialogRef: MatDialogRef<TaskDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TaskItem
  ) {
    this.localTask = { ...data };

    if (typeof this.localTask.status === 'string') {
      this.localTask.status = parseInt(this.localTask.status, 10) as TaskItem['status'];
    }
 
    if (this.localTask.status === undefined) {
      this.localTask.status = 0;
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}