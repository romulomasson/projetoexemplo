using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Interfaces.Application;
using Exemplo.Domain.Interfaces.Repository;
using Exemplo.IoC;
using Exemplo.Api.AutoMapper;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces.Bases;

namespace Exemplo.Api.Helpers;

[Authorize("Bearer")]
[Route("api/[controller]")]
[Route("{language:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/api/[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
public class ControllerBase<TContext, TEntity, TViewModel> : ControllerBase
     where TContext : IUnitOfWork<TContext>
     where TViewModel : class, IViewModel<TEntity>
{
    protected readonly IApplicationBase<TContext, TEntity> _application;
    //protected readonly IStringLocalizer _localizer = ServiceLocator.Resolve<IStringLocalizer>();
    public readonly IMapper _mapper;
    public PagedListConverter<TEntity, TViewModel> _pgConvert = new PagedListConverter<TEntity, TViewModel>();


    #region Web API 

    protected ControllerBase(IApplicationBase<TContext, TEntity> application, IMapper mapper)
    {
        this._application = application;
        this._mapper = mapper;
    }


    [HttpGet]
    public virtual Task<IActionResult> Get()
    {
        var list = _application.GetAll();
        return Task.FromResult<IActionResult>(Ok(_mapper.Map<IList<TViewModel>>(list)));
    }

    [HttpGet("filter")]
    public virtual ActionResult<PagedList<TViewModel>> GetQuery([ModelBinder] QueryFilter queryFilter)
    {
        var result = _application.GetPaginated(queryFilter, queryFilter.Start, queryFilter.Limit);
        return _pgConvert.Convert(result, _mapper);
    }

    [HttpGet("{id}")]
    public virtual Task<IActionResult> Get(int id)
    {
        //Throw.IfIsNull(id, this._localizer["campoNulo", nameof(id)]);
        var result = _mapper.Map<TViewModel>(_application.Get(id));
        return Task.FromResult<IActionResult>(Ok(result));
    }

    [HttpPost]
    public virtual Task<IActionResult> Post([FromBody] TViewModel model)
    {
        //Throw.IfIsNull(model, this._localizer["campoNulo", nameof(model)]);
        var result = _mapper.Map<TViewModel>(_application.Save(model.Model()));
        return Task.FromResult<IActionResult>(Ok(result));
    }

    [HttpPut("{id}")]
    public virtual Task<IActionResult> Put(long id, [FromBody] TViewModel model)
    {
        //Throw.IfIsNull(id, this._localizer["campoNulo", nameof(id)]);
        //Throw.IfLessThanOrEqZero(id, this._localizer["menorOuIgual", nameof(id), "zero"]);
        //Throw.IfIsNull(model, this._localizer["campoNulo", nameof(model)]);
        var result = _mapper.Map<TViewModel>(_application.Update(model.Model()));
        return Task.FromResult<IActionResult>(Ok(result));
    }

    [HttpDelete("{id}")]
    public virtual Task<IActionResult> Delete(int id)
    {
        //Throw.IfIsNull(id, this._localizer["campoNulo", nameof(id)]);
        _application.Delete(id);
        return Task.FromResult<IActionResult>(Ok());
    }
    #endregion
}









