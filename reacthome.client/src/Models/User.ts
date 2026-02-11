import type { Task } from "../Models/Task";

export type User = {
  id: string;
  fullName: string;
  phone: string;
  email: string;
  tasks: Task[];
};
