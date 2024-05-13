using System;

namespace Exemplo.Domain.Entities;
public class Usuario : EntityBase
{
    #region "Constructors"
    public Usuario(int Id, int UsuarioTipoId, string Nome, string Email, string Login,  string Senha,int  EmpresaId)
    {
        this.Ativar();
        this.Id = Id;
        this.UsuarioTipoId = UsuarioTipoId;
        this.Email = Email;
        this.Nome = Nome;
        this.Login = Login;
        this.Senha = Senha;
        this.EmpresaId = EmpresaId;
    }
    #endregion

    #region "Properties"
    public int UsuarioTipoId { get; protected set; }
    public string Nome { get; protected set; }
    public string Email { get; protected set; }
    public string Login { get; protected set; }
    public string Senha { get; protected set; }

    public int EmpresaId { get; protected set; }
    #endregion

    #region "Referencess"

    #endregion

    #region "Methods"
    public void UpdateUsuarioTipoId(int UsuarioTipoId) => this.UsuarioTipoId = UsuarioTipoId;    
    public void UpdateNome(string Nome) => this.Nome = Nome;
    public void UpdateUserName(string Login) => this.Login = Login;
    public void UpdateMail(string Email) => this.Email = Email;
    public void UpdatePassword(string Senha) => this.Senha = Senha;
    public void UpdateEmpresaId(int EmpresaId) => this.EmpresaId = EmpresaId;
    public void AtualizarSenha(string novaSenha) => this.Senha = novaSenha;
    #endregion
}

