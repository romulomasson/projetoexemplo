

IF COL_LENGTH('dbo.Funcionario', 'CodigoHash') IS NULL
BEGIN
	Alter Table Funcionario Add CodigoHash Varchar(16)
END

IF COL_LENGTH('dbo.Funcionario', 'CadastroCompleto') IS NULL
BEGIN
	Alter Table Funcionario Add CadastroCompleto Bit Default(0) Not Null
END

IF COL_LENGTH('dbo.Funcionario', 'CreditoCalculado') IS NULL
BEGIN
	Alter Table Funcionario Add CreditoCalculado Bit Default(0) Not Null
END

IF COL_LENGTH('dbo.Funcionario', 'OnBoardingEnviado') IS NULL
BEGIN
	Alter Table Funcionario Add OnBoardingEnviado Bit Default(0) Not Null
END