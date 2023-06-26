using api_eventos.Models;
using api_eventos.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_eventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly EventoService _eventoService;

    public EventoController(EventoService eventoService) =>
        _eventoService = eventoService;

    [HttpGet]
    public async Task<List<Evento>> Get() =>
        await _eventoService.GetEventosAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Evento>> Get(string id)
    {
        var evento = await _eventoService.GetEventoAsync(id);

        if (evento is null)
        {
            return NotFound();
        }

        return evento;
    }

    [HttpPost]
    public async Task<IActionResult> Post (Evento evento)
    {
        await _eventoService.CreateEventoAsync(evento);
        return CreatedAtAction(nameof(Get), new { id = evento.Id }, evento);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Evento updatedEvento)
    {
        var evento = await _eventoService.GetEventoAsync(id);

        if(evento is null)
        {
            return NotFound();
        }

        updatedEvento.Id = evento.Id;
        await _eventoService.UpdateEventoAsync(id, updatedEvento);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var evento = await _eventoService.GetEventoAsync(id);

        if (evento is null)
        {
            return NotFound();
        }

        await _eventoService.RemoveEventoAsync(id);
        return NoContent();
    }
}