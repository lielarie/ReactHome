import { UsersService } from "../Services/UsersService";
import type { User } from "../Models/User";
import { useState } from "react";

export default function Users() {
  const [email, setEmail] = useState("");
  const [user, setUser] = useState<User>();

  return (
    <div>
      <div className="users">
        <div className="getUserByEmail">
          <input
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="Email"
          />

          <button
            disabled={email.trim().length === 0}
            onClick={async () => {
              const user = await UsersService.getUserByEmail(email);
              setUser(user);
              alert(user);
            }}
          >
            Get User By Email
          </button>
          {user && (
            <div>
              <div>
                <b>Id:</b> {user.id}
              </div>
              <div>
                <b>Name:</b> {user.fullName}
              </div>
              <div>
                <b>Email:</b> {user.email}
              </div>
              <div>
                <b>Phone:</b> {user.phone}
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
