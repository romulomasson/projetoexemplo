If Object_Id('EmprestimoStatus') Is Null 
	Create Table dbo.EmprestimoStatus
	(
		EmprestimoStatusId int Identity Not Null  Constraint PK_EmprestimoStatus Primary Key
		,Descricao varchar(64)
	) 
GO
If Object_Id('ContratoStatus') Is Null 
	Create Table dbo.ContratoStatus
	(
		ContratoStatusId int Identity Not Null  Constraint PK_ContratoStatus Primary Key
		,Descricao varchar(64)
	) 
GO
If Object_Id('EmpresaEnderecoTipo') Is Null 
	Create Table dbo.EmpresaEnderecoTipo
	(
		EmpresaEnderecoTipoId int Identity Not Null  Constraint PK_EmpresaEnderecoTipo Primary Key
		,Descricao varchar(64) Not Null 
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int Not Null 
		,UsuarioAlteracao int
	) 
GO
If Object_Id('BoletoStatus') Is Null 
	Create Table dbo.BoletoStatus
	(
		 BoletoStatusId int Identity Not Null Constraint PK_BoletoStatus Primary Key
		,Descricao varchar(64)
	) 
GO
If Object_Id('ContaTipo') Is Null 
	Create Table dbo.ContaTipo
	(
		ContaTipoId int Identity Not Null  Constraint PK_ContaTipo Primary Key
		,Descricao varchar(64)
	) 
GO
If Object_Id('Configuracao') Is Null 
	Create Table dbo.Configuracao
	(
		ConfiguracaoId int Identity Not Null  Constraint PK_Configuracao Primary Key
		,Chave varchar(64) Not Null 
		,Valor varchar(64) Not Null 
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int Not Null 
		,UsuarioAlteracao int
	) 
GO
If Object_Id('NotificacaoTipo') Is Null 
	Create Table dbo.NotificacaoTipo
	(
		NotificacaoTipoId int Identity Not Null  Constraint PK_NotificacaoTipo Primary Key
		,Nome varchar(64) Not Null 
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('ContratoDocumentoTipo') Is Null 
	Create Table dbo.ContratoDocumentoTipo
	(
		ContratoDocumentoTipoId int Identity Not Null  Constraint PK_ContratoDocumentoTipo Primary Key
		,Descricao varchar(32)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('NotaFiscalStatus') Is Null 
	Create Table dbo.NotaFiscalStatus
	(
		NotaFiscalStatusId int Identity Not Null  Constraint PK_NotaFiscalStatus Primary Key
		,Descricao varchar(64)
	) 
GO
If Object_Id('UsuarioTipo') Is Null 
	Create Table dbo.UsuarioTipo
	(
		UsuarioTipoId int Identity Not Null  Constraint PK_UsuarioTipo Primary Key
		,Descricao varchar(64)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int Not Null 
		,UsuarioAlteracao int
	) 
GO
If Object_Id('Seguradora') Is Null 
	Create Table dbo.Seguradora
	(
		SeguradoraId int Identity Not Null  Constraint PK_Seguradora Primary Key
		,Nome varchar(64)
		,CodigoApolice varchar(64)
		,Percentual decimal(10, 4)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('Usuario') Is Null 
	Create Table dbo.Usuario
	(
		UsuarioId int Identity Not Null  Constraint PK_Usuario Primary Key
		,EmpresaId int
		,[Login] varchar(64) Not Null 
		,Email varchar(128) Not Null 
		,Senha varchar(256) Not Null 
		,Nome varchar(64) Not Null 
		,Telefone varchar(64)
		,Cargo varchar(64)
		,Superior int
		,NumeroDocumento varchar(64)
		,Bloqueado bit
		,Avatar varchar(max)
		,UsuarioTipoId int Not Null References UsuarioTipo(UsuarioTipoId) 
		,ForcarTrocaSenha bit Not Null
		,EmpresaId Int Not Null References Empresa(EmpresaId) 
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int Not Null 
		,UsuarioAlteracao int
	)
GO
If Object_Id('Filial') Is Null 
	Create Table dbo.Filial
	(
		FilialId int Identity Not Null  Constraint PK_Filial Primary Key
		,Codigo varchar(64)
		,RazaoSocial varchar(128)
		,NomeFantasia varchar(64)
		,DataAbertura date
		,Regime int
		,CNPJ varchar(16)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('Conta') Is Null 
	Create Table dbo.Conta
	(
		ContaId int Identity Not Null  Constraint PK_Conta Primary Key
		,ContaTipoId int Not Null References ContaTipo(ContaTipoId)
		,Codigo varchar(16)
		,Saldo decimal(18, 2)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('Carteira') Is Null 
	Create Table dbo.Carteira
	(
		CarteiraId int Identity Not Null  Constraint PK_Carteira Primary Key
		,FilialId int Not Null References Filial(FilialId)
		,CodigoBanco varchar(64)
		,BancoNome varchar(64)
		,Agencia varchar(64)
		,DigitoAgencia varchar(64)
		,ContaCorrente varchar(64)
		,DigitoContaCorrente varchar(64)
		,CodigoCarteira varchar(8)
		,Instrucoes varchar(512)
		,ContaId int Not Null References Conta(ContaId)
		,SequenciaLote int
		,SequenciaNossoNumero int
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('Empresa') Is Null 
	Create Table dbo.Empresa
	(
		EmpresaId int Identity Not Null  Constraint PK_Empresa Primary Key
		,CNPJ varchar(32)
		,RazaoSocial varchar(256) Not Null 
		,NomeFantasia varchar(256)
		,CodigoAtividade varchar(32)
		,NomeAtividade varchar(64)
		,Tipo varchar(16)
		,DataAbertura date
		,DataSituacao date
		,SituacaoRFB varchar(16)
		,MotivoSituacaoRFB varchar(64)
		,SituacaoEspecialRFB varchar(64)
		,DataSituacaoEspecial date
		,StatusRFB varchar(16)
		,NaturezaJuridica varchar(32)
		,Porte varchar(16)
		,CapitalSocial decimal(9, 2)
		,UF varchar(2)
		,Telefone varchar(32)
		,Email varchar(128)
		,Gestor int
		,Vendedor int
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int Not Null 
		,UsuarioAlteracao int
	)
GO
If Object_Id('EmpresaEndereco') Is Null 
	Create Table dbo.EmpresaEndereco
	(
		EmpresaEnderecoId int Identity Not Null  Constraint PK_EmpresaEndereco Primary Key
		,EmpresaId int Not Null References Empresa(EmpresaId)
		,EmpresaEnderecoTipoId int Not Null References EmpresaEnderecoTipo(EmpresaEnderecoTipoId)
		,Logradouro varchar(128)
		,Numero int
		,Complemento varchar(64)
		,Bairro varchar(64)
		,Cidade varchar(64)
		,UF varchar(64)
		,CEP varchar(12)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int Not Null 
		,UsuarioAlteracao int
	) 
GO
If Object_Id('EmpresaSocio') Is Null 
	Create Table dbo.EmpresaSocio
	(
		EmpresaSocioId int Identity Not Null  Constraint PK_EmpresaSocio Primary Key
		,EmpresaId int Not Null References Empresa(EmpresaId)
		,Nome varchar(128)
		,Tipo varchar(64)
		,CPF varchar(16)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int Not Null 
		,UsuarioAlteracao int
	) 
GO
If Object_Id('Contato') Is Null 
	Create Table dbo.Contato
	(
		ContatoId int Identity Not Null  Constraint PK_Contato Primary Key
		,Nome varchar(128)
		,Telefone varchar(32)
		,Email varchar(128)
		,Cargo varchar(64)
		,Empresa varchar(128)
		,Contato_Empresa varchar(128)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('EmpresaContrato') Is Null 
	Create Table dbo.EmpresaContrato
	(
		EmpresaContratoId int Identity Not Null  Constraint PK_EmpresaContrato Primary Key
		,Codigo varchar(64)
		,ContratoStatusId int Not Null References ContratoStatus(ContratoStatusId)
		,DataAssinatura datetime
		,RequerAprovacao bit
		,Aprovador int Not Null 
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('EmpresaContratoSignatario') Is Null 
	Create Table dbo.EmpresaContratoSignatario
	(
		EmpresaContratoSignatarioId int Identity Not Null  Constraint PK_ContratoSignatario Primary Key
		,EmpresaContratoId int Not Null References EmpresaContrato(EmpresaContratoId)
		,UsuarioId 			Int Not Null References Usuario(UsuarioId)
		,Email varchar(128)
		,EnderecoIP varchar(32)
		,Token varchar(256)
		,Hash varchar(256)
		,DataAssinatura datetime
		,Assinado Bit Default(0)		
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('EmpresaContratoDocumento') Is Null 
	Create Table dbo.EmpresaContratoDocumento
	(
		EmpresaContratoDocumentoId int Identity Not Null  Constraint PK_ContratoDocumento Primary Key
		,EmpresaContratoId int Not Null 
		,ContratoDocumentoTipoId int Not Null References ContratoDocumentoTipo(ContratoDocumentoTipoId)
		,MD5 varchar(32)
		,SHA256 varchar(256)
		,Diretorio varchar(1024)
		,NomeArquivo varchar(64)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('Funcionario') Is Null 
	Create Table dbo.Funcionario
	(
		FuncionarioId int Identity Not Null  Constraint PK_Funcionario Primary Key
		,Nome varchar(128)
		,DataAdmissao date Not Null 		
		,DataUltimaFerias date
		,SalarioLiquido decimal(10, 2)
		,SalarioBruto decimal(10, 2)
		,QuantidadeDependentes int
		,NumeroPIS varchar(64)
		,CPF varchar(16)
		,RG varchar(16)
		,DataNascimento date
		,Cargo varchar(64)
		,CodigoOcupacao int
		,Sexo Varchar(16)
		,FormacaoAcedemica Varchar(256)
		,MultaFGTS decimal(10, 2)
		,ValorRecisao decimal(10, 2)
		,ScoreFuncionario decimal(10, 2)
		,Email varchar(128)
		,EmpresaId Int Not Null References Empresa(EmpresaId) 
		,UsuarioId 	Int Not Null References Usuario(UsuarioId)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('FuncionarioCredito') Is Null 
	Create Table dbo.FuncionarioCredito
	(
		 FuncionarioCreditoId int Identity Not Null  Constraint PK_FuncionarioCredito Primary Key
		,FuncionarioId int  Not Null  References Funcionario(FuncionarioId)
		,ValorCreditoMinimo Decimal(10,2)
		,ValorCreditoMaximo Decimal(10,2)
		,PrazoEmMeses Int Not Null
		,TaxaAoMes Decimal(10,2)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int

	)
GO
If Object_Id('ChavePixTipo') Is Null 
	Create Table dbo.ChavePixTipo
	(
		ChavePixTipoId int Identity Not Null  Constraint PK_ChavePixTipo Primary Key
		,Descricao 	Varchar(64)
	)
GO
If Object_Id('EmprestimoDadoBancarioTipo') Is Null 
	Create Table dbo.EmprestimoDadoBancarioTipo
	(
		EmprestimoDadoBancarioTipoId int Identity Not Null  Constraint PK_EmprestimoDadoBancarioTipo Primary Key
		,Descricao 	Varchar(64)
	)
GO
If Not Exists (Select * From EmprestimoDadoBancarioTipo)
	Insert Into EmprestimoDadoBancarioTipo(Descricao)Values('TED'),('PIX')
go
If Not Exists (Select * From ChavePixTipo)
	Insert Into ChavePixTipo(Descricao)Values('Telefone'),('Email'),('CPF/CNPJ'),('Aleatorio')
GO
If Object_Id('Emprestimo') Is Null 
	Create Table dbo.Emprestimo
	(
		EmprestimoId int Identity Not Null  Constraint PK_Emprestimo Primary Key
		,EmpresaId int Not Null References Empresa(EmpresaId)
		,FuncionarioId int Not Null References Funcionario(FuncionarioId)
		,EmprestimoStatusId int Not Null  References EmprestimoStatus(EmprestimoStatusId)
		,Valor decimal(10, 2)
		,QuantidadeParcelas int
		,ValorParcela decimal(10, 2)
		,PossuiSeguro bit
		,SeguradoraId int References Seguradora(SeguradoraId)
		,ValorSeguro decimal(10, 2)
		,SeguroDiluido bit
		,ValorTaxa decimal(10, 2)
		,TaxaDiluida bit
		,EmprestimoDadoBancarioTipoId Int Not Null References EmprestimoDadoBancarioTipo(EmprestimoDadoBancarioTipoId)
		,CodigoBanco INT
		,Agencia Varchar(64)
		,DigitoAgencia Varchar(16)
		,ContaCorrente Varchar(16)
		,ChavePixTipoId Int Null References ChavePixTipo(ChavePixTipoId)
		,ChavePix Varchar(64)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('EmprestimoContrato') Is Null 
	Create Table dbo.EmprestimoContrato
	(
		 EmprestimoContratoId Int Identity Constraint PK_EmprestimoContrato Primary Key
		,EmprestimoId int Not Null References Emprestimo(EmprestimoId)
		,ContratoStatusId int Not Null References ContratoStatus(ContratoStatusId)
		,Codigo varchar(64)
		,DataAssinatura datetime
		,RequerAprovacao bit
		,Aprovador int Not Null References Usuario(UsuarioId)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('EmprestimoContratoSignatario') Is Null 
	Create Table dbo.EmprestimoContratoSignatario
	(
		EmprestimoContratoSignatarioId int Identity Not Null  Constraint PK_EmprestimoContratoSignatario Primary Key
		,EmprestimoContratoId int Not Null References EmprestimoContrato(EmprestimoContratoId)
		,UsuarioId 			Int Not Null References Usuario(UsuarioId)
		,Email varchar(128)
		,EnderecoIP varchar(32)
		,Token varchar(256)
		,Hash varchar(256)
		,DataAssinatura datetime
		,Assinado Bit Default(0)
		,DocumentoAssinado Varchar(256)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('EmprestimoParcela') Is Null 
	Create Table dbo.EmprestimoParcela
	(
		EmprestimoParcelaId int Identity Not Null  Constraint PK_EmprestimoParcela Primary Key
		,EmprestimoId int Not Null References Emprestimo(EmprestimoId)
		,NumeroParcela int
		,DataVencimento date
		,Valor decimal(10, 2)
		,ValorPago decimal(10, 2)
		,DataPagamento date
		,Liquidado bit
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('Item') Is Null 
	Create Table dbo.Item
	(
		ItemId int Identity Not Null  Constraint PK_Item Primary Key
		,Nome varchar(64)
		,CodigoServico varchar(64)
		,ItemVenda bit
		,ItemCompra bit
		,Material bit
		,Servico bit
		,Emprestimo bit
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
GO
If Object_Id('NotaFiscal') Is Null 
	Create Table dbo.NotaFiscal
	(
		NotaFiscalId int Identity Not Null  Constraint PK_NotaFiscal Primary Key
		,FilialId int Not Null
		,Numero Int Not Null
		,Serie Varchar(3)
		,EmpresaId int Not Null References Empresa(EmpresaId)
		,DataEmissao date
		,DataVencimento date
		,NotaFiscalStatusId int Not Null References NotaFiscalStatus(NotaFiscalStatusId)
		,ValorTotal decimal(10, 2)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('NotaFiscalItem') Is Null 
	Create Table dbo.NotaFiscalItem
	(
		NotaFiscalItemId int Identity Not Null  Constraint PK_NotaFiscalItem Primary Key
		,NotaFiscalId int Not Null References NotaFiscal(NotaFiscalId)
		,ItemId int Not Null References Item(ItemId)
		,ContaId int Not Null References Conta(ContaId)
		,Quantidade int
		,ValorUnitario decimal(10, 2)
		,ValorTotal decimal(10, 2)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('Boleto') Is Null 
	Create Table dbo.Boleto
	(
		BoletoId int Identity Constraint PK_Boleto Primary Key
		,NotaFiscalId int Not Null References NotaFiscal(NotaFiscalId)
		,BoletoStatusId int Not Null References BoletoStatus(BoletoStatusId)
		,CarteiraId int Not Null References Carteira(CarteiraId)
		,NossoNumero int
		,CodigoBarra varchar(128)
		,LinhaDigitavel varchar(128)
		,Valor decimal(10, 2)
		,ValorPago decimal(10, 2)
		,DataEmissao date
		,DataVencimento date
		,DataPagamento date
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	)
GO
If Object_Id('NotificacaoProvedor') Is Null 
	Create Table dbo.NotificacaoProvedor
	(
		NotificacaoProvedorId int Identity Not Null  Constraint PK_NotificacaoProvedor Primary Key
		,Nome varchar(64) Not Null 
		,NotificacaoTipoId int Not Null References NotificacaoTipo(NotificacaoTipoId) 
		,Chave varchar(512)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('NotificacaoStatus') Is Null 
	Create Table dbo.NotificacaoStatus
	(
		NotificacaoStatusId int Identity Not Null  Constraint PK_NotificacaoStatus Primary Key
		,Nome varchar(64)
		,NotificacaoProvedorId int Not Null References NotificacaoProvedor(NotificacaoProvedorId) 
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('NotificacaoTemplate') Is Null 
	Create Table dbo.NotificacaoTemplate
	(
		NotificacaoTemplateId int Identity Not Null  Constraint PK_NotificacaoTemplate Primary Key
		,Nome varchar(64) Not Null 
		,Titulo varchar(64) Not Null 
		,Email varchar(64) Not Null 
		,Conteudo varchar(max)
		,Metadados varchar(max)
		,CodigoIntegracao varchar(64)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('Notificacao') Is Null 
	Create Table dbo.Notificacao
	(
		NotificacaoId int Identity Not Null  Constraint PK_Notificacao Primary Key
		,NotificacaoProvedorId int Not Null References NotificacaoProvedor(NotificacaoProvedorId)
		,Nome varchar(128)
		,Email varchar(128)
		,Telefone varchar(32)
		,Titulo varchar(128)
		,Conteudo varchar(max)
		,NotificacaoTemplateId int References NotificacaoTemplate(NotificacaoTemplateId)
		,AnexoBlobPath varchar(max)
		,NotificacaoStatusId int Not Null References NotificacaoStatus(NotificacaoStatusId) 
		,ErrorMessage varchar(512)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
If Object_Id('NotificacaoHistorico') Is Null 
	Create Table dbo.NotificacaoHistorico
	(
		NotificacaoHistoricoId int Identity Not Null  Constraint PK_NotificacaoHistorico Primary Key
		,NotificacaoId int Not Null References Notificacao(NotificacaoId) 
		,NotificacaoStatusId int Not Null References NotificacaoStatus(NotificacaoStatusId) 
		,Resposta varchar(256)
		,Ativo bit Not Null 
		,DataCadastro datetime Not Null 
		,DataAlteracao datetime
		,UsuarioCadastro int
		,UsuarioAlteracao int
	) 
GO
GO