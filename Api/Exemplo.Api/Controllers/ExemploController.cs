using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Exemplo.Api.Helpers;
using Exemplo.Api.Models;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces.Application;
using Exemplo.Repository.Contexts;

namespace Exemplo.Api.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class ExemploController : ControllerBase<ExemploContext, Exemplo, ExemploViewModel>
{
    private readonly IExemploApplication<ExemploContext> _appPedido;
    public readonly IMapper _mapper;
    public ExemploController(IExemploApplication<ExemploContext> appPedido, IMapper mapper)
        : base(appPedido, mapper)
    {
        _mapper = mapper;
        _appPedido = appPedido;
    }
    public override async Task<IActionResult> Post([FromBody] ExemploViewModel model)
    {            
        var retorno = await _appPedido.ChamadaService(model.Model());            
        return Ok(retorno);
    }
}







