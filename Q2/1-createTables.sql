IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ProvaSafra')
  BEGIN
    CREATE DATABASE ProvaSafra

END

GO
    USE ProvaSafra
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProvaSafra.Clientes') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Clientes(	
	[CPF] varchar(20) NOT NULL,
	[Nome] varchar(100) NOT NULL,
	[EstadoId] int NOT NULL,	
	CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED ([CPF] ASC),
	CHECK (CPF LIKE '___.___.___-__')
)

	PRINT('Tabela nova Clientes criada em - Database: ProvaSafra');

END
ELSE
	PRINT('Tabela Clientes já existe no - Database: ProvaSafra');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProvaSafra.Telefones') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Telefones(
	[DDD] varchar(3) NOT NULL,
	[Numero] varchar(9) NOT NULL,
	[Tipo] char(1) default 'M' NOT NULL,
	[CPF] varchar(20) NOT NULL,
	CONSTRAINT [PK_Telefones] PRIMARY KEY CLUSTERED ([Numero] ASC),
	CHECK (Tipo IN ('F', 'M') ),
	CHECK (CPF LIKE '___.___.___-__')
)
	PRINT('Tabela nova Telefones criada em - Database: ProvaSafra');

END
ELSE
	PRINT('Tabela Telefones já existe no - Database: ProvaSafra');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProvaSafra.Estados') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Estados(
	[Id] int IDENTITY(1,1) NOT NULL,
	[Nome] varchar(250) NOT NULL,
	[UF] char(2) NOT NULL,
	CONSTRAINT [PK_Estados] PRIMARY KEY CLUSTERED ([Id] ASC)
)
	PRINT('Tabela nova Estados criada em - Database: ProvaSafra');

END
ELSE
	PRINT('Tabela Estados já existe no - Database: ProvaSafra');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProvaSafra.Financiamentos') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Financiamentos(
	[Id] int IDENTITY(1,1) NOT NULL,
	[CPF] varchar(20) NOT NULL,	
	[TipoFinanciamentoId] int NOT NULL,
	[ValorTotal] int NOT NULL,
	[QtdParcelas] int NOT NULL,
	[DataUltimoVencimento] datetime NOT NULL	
	CONSTRAINT [PK_Financiamentos] PRIMARY KEY CLUSTERED ([Id] ASC),
	CHECK (CPF LIKE '___.___.___-__')
 )
	PRINT('Tabela nova Financiamentos criada em - Database: ProvaSafra');

END
ELSE
	PRINT('Tabela Financiamentos já existe no - Database: ProvaSafra');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProvaSafra.TiposFinanciamento') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE TiposFinanciamento(
	[Id] int IDENTITY(1,1) NOT NULL,
	[Nome] varchar(100) NOT NULL,
	[Sigla] char(2) NOT NULL,
	CONSTRAINT [PK_TiposFinanciamento] PRIMARY KEY CLUSTERED ([Id] ASC)
)
	PRINT('Tabela nova TiposFinanciamento criada em - Database: ProvaSafra');

END
ELSE
	PRINT('Tabela TiposFinanciamento já existe no - Database: ProvaSafra');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProvaSafra.Parcelamentos') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Parcelamentos(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FinanciamentoId] int NOT NULL,
	[NumeroParcela] int NOT NULL,
	[ValorParcela] numeric(8,2) NOT NULL,
	[DataVencimento] datetime NOT NULL,
	[DataPagamento] datetime
	CONSTRAINT [PK_Parcelamentos] PRIMARY KEY CLUSTERED ([Id] ASC)
 )
	PRINT('Tabela nova Parcelamentos criada em - Database: ProvaSafra');

END
ELSE
	PRINT('Tabela Parcelamentos já existe no - Database: ProvaSafra');
GO

ALTER TABLE [dbo].[Clientes] WITH CHECK ADD CONSTRAINT [FK_Clientes_Estados_EstadoId] FOREIGN KEY([EstadoId])
 REFERENCES [dbo].[Estados] ([Id]) ON DELETE CASCADE

GO

ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Clientes_Estados_EstadoId]

GO

ALTER TABLE [dbo].[Telefones] WITH CHECK ADD CONSTRAINT [FK_Telefones_Clientes_CPF] FOREIGN KEY([CPF])
 REFERENCES [dbo].[Clientes] ([CPF]) ON DELETE CASCADE

GO

ALTER TABLE [dbo].[Telefones] CHECK CONSTRAINT [FK_Telefones_Clientes_CPF]

GO

ALTER TABLE [dbo].[Financiamentos] WITH CHECK ADD CONSTRAINT [FK_Financiamentos_Clientes_CPF] FOREIGN KEY([CPF])
 REFERENCES [dbo].[Clientes] ([CPF]) ON DELETE CASCADE

GO

ALTER TABLE [dbo].[Financiamentos] CHECK CONSTRAINT [FK_Financiamentos_Clientes_CPF]

GO

ALTER TABLE [dbo].[Financiamentos] WITH CHECK ADD CONSTRAINT [FK_Financiamentos_TiposFinanciamento_TipoFinanciamentoId] FOREIGN KEY([TipoFinanciamentoId])
 REFERENCES [dbo].[TiposFinanciamento] ([Id]) ON DELETE CASCADE

GO

ALTER TABLE [dbo].[Financiamentos] CHECK CONSTRAINT [FK_Financiamentos_TiposFinanciamento_TipoFinanciamentoId]

GO

ALTER TABLE [dbo].[Parcelamentos] WITH CHECK ADD CONSTRAINT [FK_Parcelamentos_Financiamentos_FinanciamentoId] FOREIGN KEY([FinanciamentoId])
 REFERENCES [dbo].[Financiamentos] ([Id]) ON DELETE CASCADE

GO

ALTER TABLE [dbo].[Parcelamentos] CHECK CONSTRAINT [FK_Parcelamentos_Financiamentos_FinanciamentoId]






