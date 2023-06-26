using api_eventos.Models;
using api_eventos.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_eventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuartoController : ControllerBase
{
    private readonly QuartoService _quartoService;

    public QuartoController(QuartoService quartoService) =>
        _quartoService = quartoService;

    [HttpGet]
    public async Task<List<Quarto>> Get() =>
        await _quartoService.GetQuartosAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Quarto>> Get(string id)
    {
        var quarto = await _quartoService.GetQuartoAsync(id);

        if (quarto is null)
        {
            return NotFound();
        }

        return quarto;
    }

    [HttpPost]
    public async Task<IActionResult> Post (Quarto quarto)
    {
        await _quartoService.CreateQuartoAsync(quarto);
        return CreatedAtAction(nameof(Get), new { id = quarto.Id }, quarto);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Quarto updatedQuarto)
    {
        var quarto = await _quartoService.GetQuartoAsync(id);

        if(quarto is null)
        {
            return NotFound();
        }

        updatedQuarto.Id = quarto.Id;
        await _quartoService.UpdateQuartoAsync(id, updatedQuarto);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var quarto = await _quartoService.GetQuartoAsync(id);

        if (quarto is null)
        {
            return NotFound();
        }

        await _quartoService.RemoveQuartoAsync(id);
        return NoContent();
    }
}