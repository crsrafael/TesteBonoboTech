USE [master]
GO
/****** Object:  Database [Bonobo]    Script Date: 04/12/2021 12:25:30 ******/
CREATE DATABASE [Bonobo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Bonobo', FILENAME = N'D:\BD\SqlServer\Bonobo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Bonobo_log', FILENAME = N'D:\BD\SqlServer\Bonobo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Bonobo] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Bonobo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Bonobo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Bonobo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Bonobo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Bonobo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Bonobo] SET ARITHABORT OFF 
GO
ALTER DATABASE [Bonobo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Bonobo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Bonobo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Bonobo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Bonobo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Bonobo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Bonobo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Bonobo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Bonobo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Bonobo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Bonobo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Bonobo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Bonobo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Bonobo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Bonobo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Bonobo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Bonobo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Bonobo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Bonobo] SET  MULTI_USER 
GO
ALTER DATABASE [Bonobo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Bonobo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Bonobo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Bonobo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Bonobo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Bonobo] SET QUERY_STORE = OFF
GO
USE [Bonobo]
GO
/****** Object:  User [Bonobo]    Script Date: 04/12/2021 12:25:30 ******/
CREATE USER [Bonobo] FOR LOGIN [Bonobo] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [Bonobo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [Bonobo]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [Bonobo]
GO
/****** Object:  Table [dbo].[CONTATOS]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONTATOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [nvarchar](50) NULL,
	[Contato] [nvarchar](50) NULL,
	[ConvidadoId] [int] NULL,
 CONSTRAINT [PK_CONTATOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CONVIDADOS]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONVIDADOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NULL,
	[Apelido] [nvarchar](50) NULL,
	[DataDeNascimento] [date] NULL,
 CONSTRAINT [PK__CONVIDAD__3214EC0718B5A5DC] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_altera_contato]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_altera_contato]
	-- Add the parameters for the stored procedure here
	@Id int,
	@Tipo nvarchar(50),
	@Contato nvarchar(100),
	@ConvidadoId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE CONTATOS SET Tipo = @Tipo, Contato = @Contato, ConvidadoId = @ConvidadoId  WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_altera_convidado]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_altera_convidado]
	-- Add the parameters for the stored procedure here
	@Id int,
	@Nome nvarchar(100),
	@Apelido nvarchar(50),
	@DataDeNascimento date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE CONVIDADOS SET Nome = @Nome, Apelido = @Apelido, DataDeNascimento = @DataDeNascimento WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insere_contato]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_insere_contato] 
	@Tipo nvarchar(50),
	@Contato nvarchar(100),
	@ConvidadoId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO CONTATOS (Tipo, Contato, ConvidadoId) VALUES(@Tipo, @Contato, @ConvidadoId)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insere_convidado]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_insere_convidado] 
	@Nome nvarchar(100),
	@Apelido nvarchar(50),
	@DataDeNascimento date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO CONVIDADOS(Nome, Apelido, DataDeNascimento) VALUES(@Nome, @Apelido, @DataDeNascimento)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_remove_contato]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_remove_contato]
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM CONTATOS WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_remove_convidado]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_remove_convidado]
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM CONVIDADOS WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_seleciona_contato]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_seleciona_contato]
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM CONTATOS WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_seleciona_contato_convidado]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_seleciona_contato_convidado]
	-- Add the parameters for the stored procedure here
	@ConvidadoId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM CONTATOS WHERE ConvidadoId = @ConvidadoId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_seleciona_convidado]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_seleciona_convidado]
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM CONVIDADOS WHERE id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_seleciona_convidados]    Script Date: 04/12/2021 12:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_seleciona_convidados]
	-- Add the parameters for the stored procedure here
	@Pesquisa nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if(@Pesquisa = '')
	begin
		SELECT * FROM CONVIDADOS;
	end
	else
	begin
		SELECT * FROM CONVIDADOS WHERE Nome like '%' + @Pesquisa + '%' OR Apelido like '%' + @Pesquisa + '%';
	end
END
GO
USE [master]
GO
ALTER DATABASE [Bonobo] SET  READ_WRITE 
GO
