namespace Exemplo.Api.Authorization.Dto
{
    public class UsuarioDTO
    {
        public UsuarioDTO()
        {

        }

        public UsuarioDTO(int? CodigoUsuario, string Usuario, string NomeCliente, string NomeEstabelecimento, int? CodigoCliente, int? CodigoEstabelecimento)
        {
            this.CodigoUsuario = CodigoUsuario;
            this.Usuario = Usuario;
            this.NomeCliente = NomeCliente;
            this.NomeEstabelecimento = NomeEstabelecimento;
            this.CodigoCliente = CodigoCliente;
            this.CodigoEstabelecimento = CodigoEstabelecimento;
        }

        public string SessionId { get; set; }
        public string Codecypher { get; set; }
        public string CardId { get; set; }
        public string SessionTokenUIP { get; set; }
        public string RoleIDBasicUser { get; set; }
        public string RoleIDAdminUser { get; set; }
        public string Usuario { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeCliente { get; set; }
        public string NomeEstabelecimento { get; set; }
        public string NumeroConselho { get; set; }
        public string Password { get; set; }
        public string NumeroCartao { get; set; }
        public string PessoaDocumento { get; set; }
        public int? CodigoUsuario { get; set; }
        public int? CodigoCliente { get; set; }
        public int? CodigoEstabelecimento { get; set; }
        public string NomeMunicipio { get; set; }
        public int? Codigo { get; set; }
        public DateTime? UltimoAcesso { get; set; }

    }
}






