import { http } from "../http";
import type { Task } from "../Models/Task";

export const TasksService = {
  async getTaskById(id: string): Promise<Task[]> {
    const res = await http.get<Task[]>(`/api/Tasks/${id}`);
    return res.data;
  },

  async getTasksByEmail(email: string): Promise<Task[]> {
    const res = await http.get<Task[]>("/api/Tasks", { params: { email } });
    return res.data;
  },

  async createTask(task: Task): Promise<string> {
    const res = await http.post<string>("/api/Tasks", task);
    return res.data;
  },

  async updateTask(task: Task): Promise<string> {
    const res = await http.put<string>(`/api/Tasks/${task.id}`, {
      params: { task },
    });
    return res.data;
  },

  async deleteTask(id: string): Promise<string> {
    const res = await http.delete<string>(`/api/Tasks/${id}`);
    return res.data;
  },
};
