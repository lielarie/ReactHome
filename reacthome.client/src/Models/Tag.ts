import type { Task } from "../Models/Task";

export type Tag = {
  id: string;
  name: string;
  tasks: Task[];
};
