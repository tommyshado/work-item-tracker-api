using Microsoft.AspNetCore.Mvc;
using WorkItemTrackerApi.DTOs;
using WorkItemTrackerApi.Models;

[ApiController]
[Route("api/workitems")]
public class WorkItemsController : ControllerBase
{
    private readonly IWorkItemService _service;

    public WorkItemsController(IWorkItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var items = await _service.GetWorkItems();
            return Ok(items);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var item = await _service.GetWorkItem(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(WorkItemDto dto)
    {
        try
        {
            var item = await _service.CreateWorkItem(dto);
            return Created($"/api/workitems/{item.Id}", item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, WorkItem item) 
    {
        if (id != item.Id)
            return BadRequest("ID mismatch");

        try
        {
            var updatedItem = await _service.UpdateWorkItem(item);
            return Ok(updatedItem);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var item = await _service.GetWorkItem(id);
            if (item == null)
                return NotFound();

            await _service.DeleteWorkItem(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        try
        {
            var items = await _service.SearchWorkItem(query);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(WorkItemStatus status)
    {
        try
        {
            var items = await _service.GetWorkItemsByStatus(status);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
