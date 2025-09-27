using Microsoft.AspNetCore.Mvc;
using TaskListAPI.Application.DTOs;
using TaskListAPI.Domain.Interfaces.Services;

namespace TaskListAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemController : ControllerBase
{
    private readonly ITaskItemService _service;

    public TaskItemController(ITaskItemService service)
    {
        _service = service;
    }

    /// <summary> 
    /// Retorna todas as tarefas. 
    /// </summary>
    /// <remarks>
    /// Exemplo de request:
    /// GET /api/TaskItem
    /// </remarks>
    /// <returns>Lista de tarefas</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    /// <summary> 
    /// Retorna uma tarefa pelo Id. 
    /// </summary>
    /// <param name="id">Id da tarefa</param>
    /// <returns>Tarefa correspondente ao Id</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TaskItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskItemDto>> GetById(Guid id)
    {
        var task = await _service.GetByIdAsync(id);
        return task == null ? NotFound() : Ok(task);
    }

    /// <summary> 
    /// Cria uma nova tarefa. 
    /// </summary>
    /// <param name="dto">Dados da tarefa a ser criada</param>
    /// <returns>Tarefa criada</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TaskItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskItemDto>> Create([FromBody] TaskCreateDto dto)
    {
        var createdTask = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }

    /// <summary> 
    /// Atualiza uma tarefa existente. 
    /// </summary>
    /// <param name="dto">Dados da tarefa a ser atualizada</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] TaskUpdateDto dto)
    {
        await _service.UpdateAsync(dto);
        return NoContent();
    }

    /// <summary> 
    /// Remove uma tarefa pelo Id. 
    /// </summary>
    /// /// <param name="id">Id da tarefa a ser removida</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
