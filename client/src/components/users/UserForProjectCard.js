import { useNavigate } from "react-router-dom";
import {
  Card,
  CardBody,
  CardTitle,
  CardText,
  CardSubtitle,
  Button,
  Progress,
} from "reactstrap";

import { deleteUserProject, getUserProjectsByProjectId } from "../../managers/userProjectManager";

export default function UserProjectForProjectCard({ userProject, getAllUserProjects, getProjectDetails, project, getAllUsersForProject }) {

  const deleteProjectFunction = (userProjectId) => {
    deleteUserProject(userProjectId) 
      .then(() => {
        getAllUsersForProject(project.id);
      })
  };

  const navigate = useNavigate();
  
  return (
    <Card color="dark" outline style={{ marginBottom: "4px" }}>
      <CardBody>
        <CardTitle tag="h5">{userProject?.userProfile?.fullName}</CardTitle>
        <CardSubtitle className="mb-2 text-muted" tag="h6">
         Id: {userProject?.id}
        </CardSubtitle>

        <Button
          onClick={() => deleteProjectFunction(userProject.id)}
          color="danger"
          style={{ marginLeft: "8px" }} // Add left margin for spacing
        >
          Remove
        </Button>
      </CardBody>
    </Card>
  );
}