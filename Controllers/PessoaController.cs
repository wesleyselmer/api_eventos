using api_eventos.Models;
using api_eventos.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_eventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly PessoaService _pessoaService;

    public PessoaController(PessoaService pessoaService) =>
        _pessoaService = pessoaService;

    [HttpGet]
    public async Task<List<Pessoa>> Get() =>
        await _pessoaService.GetPessoasAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Pessoa>> Get(string id)
    {
        var pessoa = await _pessoaService.GetPessoaAsync(id);

        if (pessoa is null)
        {
            return NotFound();
        }

        return pessoa;
    }

    [HttpPost]
    public async Task<IActionResult> Post (Pessoa pessoa)
    {
        await _pessoaService.CreatePessoaAsync(pessoa);
        return CreatedAtAction(nameof(Get), new { id = pessoa.Id }, pessoa);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Pessoa updatedPessoa)
    {
        var pessoa = await _pessoaService.GetPessoaAsync(id);

        if(pessoa is null)
        {
            return NotFound();
        }

        updatedPessoa.Id = pessoa.Id;
        await _pessoaService.UpdatePessoaAsync(id, updatedPessoa);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var pessoa = await _pessoaService.GetPessoaAsync(id);

        if (pessoa is null)
        {
            return NotFound();
        }

        await _pessoaService.RemovePessoaAsync(id);
        return NoContent();
    }
}