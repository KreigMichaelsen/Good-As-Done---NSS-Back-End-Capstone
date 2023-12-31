import React, { useState, useEffect } from "react";
import TaskCard from "./TaskCard";
import { getProjectTasks, getProjectTasksByUserId } from "../../managers/projectTaskManager";
import { Button, ButtonDropdown, DropdownItem, DropdownMenu, DropdownToggle } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { MdOutlineAddTask, MdAddTask, MdOutlineNoteAdd } from "react-icons/md";

export default function TaskList({loggedInUser}) {
  const [tasks, setTasks] = useState([]);
  const [filteredTasks, setFilteredTasks] = useState([]);
  const [completed, setCompleted] = useState([]);
  const [showCompleted, setShowCompleted] = useState(false);
  const [dropdownOpen, setDropdownOpen] = useState(false);
  
  const toggle = () => setDropdownOpen((prevState) => !prevState);

  const navigate = useNavigate();


  const Id = loggedInUser.id;

  const getAllTasks = () => {
    getProjectTasksByUserId(Id).then((t) => {
      if (showCompleted === true) {
        const completedTasks = t.filter(t => t.isCompleted);
        setTasks(completedTasks);
      } else if (showCompleted === false) {
        const incompleteTasks = t.filter(t => !t.isCompleted);
        setTasks(incompleteTasks);
      } else {
        setTasks(t); 
      }
    }); 
  };

  useEffect(() => {

    getAllTasks();

  }, [showCompleted]);

  useEffect(() => {

    setFilteredTasks(tasks);

  }, [tasks]);


  return (
    <>  
    
      <Button
          className="createTaskButton"
          color="warning"
          onClick={() => {
            navigate(`/tasks/create`)
          }}
        >
          <MdAddTask className="addTaskForProjectIcon"/>
        </Button>
      <ButtonDropdown isOpen={dropdownOpen} toggle={toggle}>
        <DropdownToggle caret color="dark">
          Filter Completed
        </DropdownToggle>
        <DropdownMenu>
          <DropdownItem onClick={() => setShowCompleted(false)}>Show Incomplete</DropdownItem>
          <DropdownItem onClick={() => setShowCompleted(true)}>Show Completed</DropdownItem>
          <DropdownItem onClick={() => setShowCompleted(null)}>Show All Tasks</DropdownItem>
        </DropdownMenu>
      </ButtonDropdown>
      <div className="taskCardListContainer">
      {filteredTasks.map((task) => (
        <TaskCard
          task={task}
          key={task.id}
          getAllTasks={getAllTasks}
        ></TaskCard>
      ))}
      </div>
    </>
  );
}
