namespace Exemplo.Domain.Entities;

public class Exemplo : EntityBase
{
    #region "Constructors"
    public Exemplo(int Id, string Descricao)
    {
        this.Ativar();
        this.Id = Id;
        this.Descricao = Descricao;                       
        this.AtualizarDataCadastro();
    }
    #endregion

    #region "Properties"
    public string Descricao { get; protected set; }
    
    #endregion

    #region "References"
  
    #endregion

    #region "Methods"
    public void UpdateDescricao(string Descricao) => this.Descricao = Descricao;    
    #endregion
}







