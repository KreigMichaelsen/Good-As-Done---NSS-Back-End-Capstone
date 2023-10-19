import { useState } from "react";
import ProjectList from "./ProjectList";
import { Button } from "reactstrap";
import { useNavigate } from "react-router-dom";

export default function Projects() {

  const navigate = useNavigate();

  return (
    <div className="container">
      <div className="row">
      <h2>Projects</h2>
      <div className="col-sm-8">
      <Button
          color="dark"
          onClick={() => {
            navigate(`/projects/create`)
          }}
        >
          Create Project
        </Button>
        </div>
        <div className="col-sm-8">
          <ProjectList />
        </div>
      </div>
    </div>
  );
}
