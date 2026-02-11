import "./App.css";
import { useState } from "react";
import Users from "./Components/Users";
import Tags from "./Components/Tags";

function App() {
  return (
    <>
      <div>
        <Users />
        <Tags />
      </div>
    </>
  );
}

export default App;
