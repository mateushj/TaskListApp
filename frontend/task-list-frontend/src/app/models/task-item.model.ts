export enum TaskItemStatus {
  Todo = 0,
  InProgress = 1,
  Done = 2
}

export const TaskStatusLabel: Record<TaskItemStatus, string> = {
  [TaskItemStatus.Todo]: 'A Fazer',
  [TaskItemStatus.InProgress]: 'Em Progresso',
  [TaskItemStatus.Done]: 'Concluída'
};

export interface TaskItem {
  id: number;
  title: string;
  description?: string;
  dueDate?: Date;
  status: TaskItemStatus;
}
