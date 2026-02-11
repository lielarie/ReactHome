import { useState } from "react";
import { TagsService } from "../Services/TagsService";
import type { Tag } from "../Models/Tag";

export default function Tags() {
  const [tags, setTags] = useState<Tag[]>([]);
  const [tagName, setTagName] = useState("");

  return (
    <div>
      <div className="tags">
        <div className="getAllTags">
          <button
            onClick={async () => {
              const data = await TagsService.getAllTags();
              setTags(data);
              console.log(tags);
            }}
          >
            Get All Tags
          </button>
          <ul>
            {tags.map((t) => (
              <li key={t.id}>{t.name}</li>
            ))}
          </ul>
        </div>
        <div className="createTag">
          <input
            value={tagName}
            onChange={(e) => setTagName(e.target.value)}
            placeholder="Tag name"
          />

          <button
            disabled={tagName.trim().length === 0}
            onClick={async () => {
              await TagsService.createTag({ name: tagName } as Tag);
              setTagName("");
              const data = await TagsService.getAllTags();
              setTags(data);
            }}
          >
            Create Tag
          </button>
        </div>
      </div>
    </div>
  );
}
