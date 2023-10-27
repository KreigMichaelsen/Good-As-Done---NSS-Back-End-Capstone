using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BackEndCapstone.Data;
using BackEndCapstone.Models;
namespace BackEndCapstone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private BackEndCapstoneDbContext _dbContext;

    public UserProfileController(BackEndCapstoneDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    // [Authorize]
    public IActionResult GetUserProfiles()
    {
        return Ok(_dbContext.UserProfiles
        .ToList());
    }

    [HttpGet("{projectId}/userProfilesNotInProject")]
    // [Authorize]
    public IActionResult GetUserProfilesNotInProject(int projectId)
    {
        var userProfileIdsInProject = _dbContext.UserProjects
        .Where(up => up.ProjectId == projectId)
        .Select(up => up.UserProfileId)
        .ToList();

        var userProfilesNotInProject = _dbContext.UserProfiles
        .Where(up => !userProfileIdsInProject.Contains(up.Id))
        .ToList();

        return Ok(userProfilesNotInProject);
    }

    [HttpGet("{id}")]
    // [Authorize]
    public IActionResult GetById(int id)
    {
        UserProfile userProfile = _dbContext
            .UserProfiles
            .Include(p => p.IdentityUser)
            .SingleOrDefault(p => p.Id == id);

        if (userProfile == null)
        {
            return NotFound();
        }




            return Ok(userProfile);
    }

    // [HttpPost]
    // // [Authorize]
    // public IActionResult CreateProject(Project project)
    // {
        
    //     _dbContext.Projects.Add(project);
    //     _dbContext.SaveChanges();
    //     return Created($"/api/project/{project.Id}", project);
    // }

    // [HttpPut("{id}")]
    // [Authorize]
    // public IActionResult UpdateWorkOrder(WorkOrder workOrder, int id)
    // {
    //     WorkOrder workOrderToUpdate = _dbContext.WorkOrders.SingleOrDefault(wo => wo.Id == id);
    //     if (workOrderToUpdate == null)
    //     {
    //         return NotFound();
    //     }
    //     else if (id != workOrder.Id)
    //     {
    //         return BadRequest();
    //     }

    //     //These are the only properties that we want to make editable
    //     workOrderToUpdate.Description = workOrder.Description;
    //     workOrderToUpdate.UserProfileId = workOrder.UserProfileId;
    //     workOrderToUpdate.BikeId = workOrder.BikeId;

    //     _dbContext.SaveChanges();

    //     return NoContent();
    // }
    // [HttpPut("{id}/complete")]
    // [Authorize]
    // public IActionResult CompleteWorkOrder(int id)
    // {
    //     WorkOrder workOrderToComplete = _dbContext.WorkOrders.SingleOrDefault(wo => wo.Id == id);
    //     if (workOrderToComplete == null)
    //     {
    //         return NotFound();
    //     }

    //     //These are the only properties that we want to make editable
    //     workOrderToComplete.DateCompleted = DateTime.Now;
        

    //     _dbContext.SaveChanges();

    //     return NoContent();
    // }

    // [HttpDelete("{id}/delete")]
    // [Authorize]
    // public IActionResult DeleteWorkOrder(int id)
    // {
    //     WorkOrder workOrderToDelete = _dbContext.WorkOrders.SingleOrDefault(wo => wo.Id == id);
    //     if (workOrderToDelete== null)
    //     {
    //         return NotFound();
    //     }

    
    //     _dbContext.WorkOrders.Remove(workOrderToDelete);
    //     _dbContext.SaveChanges();

    //     return NoContent();
    // }
}