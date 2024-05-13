using Exemplo.Domain.Helpers;

namespace Exemplo.Domain.Entities;

public class UserIdentity
{
    public int Id { get; }
    public string Email { get; }
    public int UsuarioId { get; }
    public int UsuarioRealId { get; }
    public int partnerId { get; }
    public string UsuarioCodigo { get; set; }
    public int Perfil { get; set; }
    public bool Administrador { get; set; }

    public int EmpresaId { get; set; }

    public UserIdentity(int id, string email, int usuarioRealId, int partnerId, int perfil, bool administrador, int empresaid)
    {            
        ThrowHelper.IfIsNullOrWhiteSpace(email);            
        this.Id = id;
        this.Email = email;
        this.UsuarioRealId = usuarioRealId;
        this.partnerId = partnerId;
        this.Perfil = perfil;
        this.Administrador = administrador;
        this.EmpresaId = empresaid;
    }
}






