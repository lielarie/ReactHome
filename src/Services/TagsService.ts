import { http } from "../http";
import type { Tag } from "../Models/Tag";

export const TagsService = {
  async getAllTags(): Promise<Tag[]> {
    const res = await http.get<Tag[]>(`/api/Tags`);
    return res.data;
  },

  async createTag(tag: Tag): Promise<string> {
    const res = await http.post<string>("/api/Tags", tag);
    return res.data;
  },
};
