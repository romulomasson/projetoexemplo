using Exemplo.Api.Helpers;
using Exemplo.Domain.Entities;

namespace Exemplo.Api.Models;

public class ExemploViewModel : IViewModel<Exemplo>
{
    public int Id { get; set; }                
    public string Descricao { get; set; }        
    public Exemplo Model()
    {
        return new Exemplo(Id, Descricao);
    }
}









