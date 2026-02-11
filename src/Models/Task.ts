import type { Priority } from "./Priority";
import type { Tag } from "./Tag";

export type Task = {
  id: string;
  userId: string;
  title: string;
  description: string;
  dueDate: Date;
  priority: Priority;
  tags: Tag[];
};
