using api_eventos.Models;
using api_eventos.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_eventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocalController : ControllerBase
{
    private readonly LocalService _localService;

    public LocalController(LocalService localService) =>
        _localService = localService;

    [HttpGet]
    public async Task<List<Local>> Get() =>
        await _localService.GetLocaisAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Local>> Get(string id)
    {
        var local = await _localService.GetLocalAsync(id);

        if (local is null)
        {
            return NotFound();
        }

        return local;
    }

    [HttpPost]
    public async Task<IActionResult> Post (Local local)
    {
        await _localService.CreateLocalAsync(local);
        return CreatedAtAction(nameof(Get), new { id = local.Id }, local);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Local updatedLocal)
    {
        var local = await _localService.GetLocalAsync(id);

        if(local is null)
        {
            return NotFound();
        }

        updatedLocal.Id = local.Id;
        await _localService.UpdateLocalAsync(id, updatedLocal);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var local = await _localService.GetLocalAsync(id);

        if (local is null)
        {
            return NotFound();
        }

        await _localService.RemoveLocalAsync(id);
        return NoContent();
    }
}