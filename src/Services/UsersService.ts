import { http } from "../http";
import type { User } from "../Models/User";

export const UsersService = {
  async getUserById(id: string): Promise<User> {
    const res = await http.get<User>(`/api/Users/${id}`);
    return res.data;
  },

  async getUserByEmail(email: string): Promise<User> {
    const res = await http.get<User>(`/api/Users/byEmail`, {
      params: { email },
    });
    return res.data;
  },

  async createUser(user: User): Promise<string> {
    const res = await http.post<string>("/api/Users", user);
    return res.data;
  },

  async updateUser(user: User): Promise<string> {
    const res = await http.put<string>(`/api/Users/${user.id}`, {
      params: { user },
    });
    return res.data;
  },
};
