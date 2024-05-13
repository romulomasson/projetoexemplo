namespace Exemplo.Domain.Entities;

public abstract class EntityBase : ValidatableObject
{
    public EntityBase()
    {
        this.Ativar();
        this.DataCadastro = DateTime.Now;
    }

    public int Id { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public int? UsuarioCadastro { get; set; }
    public int? UsuarioAlteracao { get; set; }

    #region Metados
    public virtual void AtualizarId(int id)
    {
        if (id == 0)
            AddException(nameof(EntityBase), nameof(this.Id), "Required", "id");

        if (string.IsNullOrEmpty(id.ToString()))
            AddException(nameof(EntityBase), nameof(this.Id), "Required", "id");

        this.Id = id;
    }
    public virtual void AtualizarUsuarioCadastro(int userId)
    {
        if (userId <= 0)
            AddException(nameof(EntityBase), nameof(this.UsuarioCadastro), "RequiredId", "user");

        this.UsuarioCadastro = userId;
    }
    public virtual void AtualizarUsuarioAlteracao(int? userId)
    {
        if (userId <= 0)
            AddException(nameof(EntityBase), nameof(this.UsuarioAlteracao), "RequiredId", "user");
        this.UsuarioAlteracao = userId;
    }
    public virtual void AtualizarDataCadastro(DateTime? data = null) => this.DataCadastro = data.HasValue ? data.GetValueOrDefault() : DateTime.Now;
    public virtual void AtualizarDataAlteracao(DateTime? data = null) => this.DataAlteracao = data.HasValue ? data.GetValueOrDefault() : DateTime.Now;
    public virtual void Ativar() => this.Ativo = true;
    public virtual void Inativar() => this.Ativo = false;

    public virtual string[] PropriedadesIgnoradasUpdate()
    {
        List<string> props = new List<string>
        {
            nameof(this.DataCadastro),
            nameof(this.UsuarioCadastro)
        };

        return props.ToArray();
    }
    #endregion
}





