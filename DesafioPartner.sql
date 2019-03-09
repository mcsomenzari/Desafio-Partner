USE [master]
GO
/****** Object:  Database [Partner]    Script Date: 08/03/2019 21:44:36 ******/
CREATE DATABASE [Partner]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Partner', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Partner.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Partner_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Partner_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Partner] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Partner].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Partner] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Partner] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Partner] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Partner] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Partner] SET ARITHABORT OFF 
GO
ALTER DATABASE [Partner] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Partner] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Partner] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Partner] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Partner] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Partner] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Partner] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Partner] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Partner] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Partner] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Partner] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Partner] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Partner] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Partner] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Partner] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Partner] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Partner] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Partner] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Partner] SET  MULTI_USER 
GO
ALTER DATABASE [Partner] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Partner] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Partner] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Partner] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Partner] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Partner] SET QUERY_STORE = OFF
GO
USE [Partner]
GO
/****** Object:  Table [dbo].[tbMarca]    Script Date: 08/03/2019 21:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbMarca](
	[MarcaId] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](150) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbPatrimonio]    Script Date: 08/03/2019 21:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbPatrimonio](
	[idTombo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](150) NOT NULL,
	[Descricao] [nvarchar](150) NOT NULL,
	[MarcaId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[delMarca]    Script Date: 08/03/2019 21:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:      Mauro Somenzari
-- Create date: 08/03/2019  
-- Description: Procedure para apagar registro da tabela tbMarca de acordo com o Id informado,
--              caso não encontre o registro, retorna um código de erro. Após conclusão, retorna
--              código de sucesso.
-- =============================================  
CREATE PROCEDURE [dbo].[delMarca]  
(  
@Id INT,  
@ReturnCode NVARCHAR(20) OUTPUT  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  
    SET @ReturnCode = 'C200'  
    IF NOT EXISTS (SELECT 1 FROM tbMarca WHERE MarcaId = @Id)  
    BEGIN  
        SET @ReturnCode ='C203'  
        RETURN  
    END  
    ELSE  
    BEGIN  
        DELETE FROM tbMarca WHERE MarcaId = @Id  
        SET @ReturnCode = 'C200'  
        RETURN  
    END  
END  
GO
/****** Object:  StoredProcedure [dbo].[delPatrimonio]    Script Date: 08/03/2019 21:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================  
-- Author:      Mauro Somenzari
-- Create date: 08/03/2019
-- Description: Procedure para apagar registro da tabela tbPatrimonio de acordo com o Id informado,
--              caso não encontre o registro, retorna um código de erro. Após conclusão, retorna
--              código de sucesso.
-- =============================================  
CREATE PROCEDURE [dbo].[delPatrimonio]  
(  
@Id INT,  
@ReturnCode NVARCHAR(20) OUTPUT  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  
    SET @ReturnCode = 'C200'  

    IF NOT EXISTS (SELECT 1 FROM tbPatrimonio WHERE idTombo = @Id)  
    BEGIN  
        SET @ReturnCode ='C203'  
        RETURN  
    END  
    ELSE  
    BEGIN  
        DELETE FROM tbPatrimonio WHERE idTombo = @Id

        SET @ReturnCode = 'C200'  
        RETURN  
    END  
END  
GO
/****** Object:  StoredProcedure [dbo].[getMarcas]    Script Date: 08/03/2019 21:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:      <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- EXEC GetUsers  
-- =============================================  
CREATE PROCEDURE [dbo].[getMarcas]
(  
@Id INT,
@ReturnCode NVARCHAR(20) OUTPUT  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  

	IF(@Id <> 0)  
    BEGIN
		IF NOT EXISTS (SELECT 1 FROM tbMarca WHERE MarcaId = @Id)  
			RETURN  


		SELECT * FROM dbo.tbMarca (NOLOCK) WHERE MarcaId = @Id
		RETURN
	END

    SELECT * FROM dbo.tbMarca (NOLOCK) ORDER BY MarcaId ASC  
END  
GO
/****** Object:  StoredProcedure [dbo].[getPatrimonios]    Script Date: 08/03/2019 21:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================  
-- Author:      Mauro Somenzari
-- Create date: 08/03/2019
-- Description: Retorno patrimônios. Todos ou de acordo o nº do tombo informado.
-- 
-- =============================================  
CREATE PROCEDURE [dbo].[getPatrimonios]
(  
@Id INT,
@ReturnCode NVARCHAR(20) OUTPUT  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  

	IF(@Id <> 0)  
    BEGIN
		IF NOT EXISTS (SELECT 1 FROM dbo.tbPatrimonio WHERE idTombo = @Id)  
			RETURN  


		SELECT * FROM dbo.tbPatrimonio (NOLOCK) WHERE idTombo = @Id
		RETURN
	END

    SELECT * FROM dbo.tbPatrimonio (NOLOCK) ORDER BY idTombo ASC  
END  
GO
/****** Object:  StoredProcedure [dbo].[getPatrimoniosPorMarca]    Script Date: 08/03/2019 21:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================  
-- Author:      Mauro Somenzari
-- Create date: 08/03/2019
-- Description: Retorna todos os patrimônios de uma marca específica.
-- 
-- =============================================  
CREATE PROCEDURE [dbo].[getPatrimoniosPorMarca]
(  
@IdMarca INT,
@ReturnCode NVARCHAR(20) OUTPUT  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  
	SET @ReturnCode = 'C200'  

	IF(@IdMarca <> 0)  
    BEGIN
		IF NOT EXISTS (SELECT 1 FROM dbo.tbPatrimonio WHERE dbo.tbPatrimonio.MarcaId = @IdMarca)  
			RETURN  


		SELECT * FROM dbo.tbPatrimonio (NOLOCK) WHERE dbo.tbPatrimonio.MarcaId = @IdMarca
		RETURN
	END
END  
GO
/****** Object:  StoredProcedure [dbo].[updMarca]    Script Date: 08/03/2019 21:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================  
-- Author:      Mauro Somenzari
-- Create date: 08/03/2019
-- Description: Insere ou altera registros de Marca.
-- =============================================  
CREATE PROCEDURE [dbo].[updMarca]  
(  
@Id INT,
@nome NVARCHAR(MAX),
@ReturnCode NVARCHAR(20) OUTPUT  
)  
AS  
BEGIN  
    SET @ReturnCode = 'C200'  

    IF(@Id <> 0)  
    BEGIN  
        IF EXISTS (SELECT 1 FROM tbMarca WHERE Nome = @nome and MarcaId <> @Id)
        BEGIN  
            SET @ReturnCode = 'C201'  
            RETURN  
        END  

		IF NOT EXISTS (SELECT 1 FROM tbMarca WHERE MarcaId = @Id)  
        BEGIN  
            SET @ReturnCode = 'C203'  
            RETURN  
        END
         
        UPDATE tbMarca SET  
        Nome = @nome
        WHERE MarcaId = @Id  
  
        SET @ReturnCode = 'C200'  
    END  
    ELSE  
    BEGIN  
        IF EXISTS (SELECT 1 FROM tbMarca WHERE Nome = @nome)  
        BEGIN  
            SET @ReturnCode = 'C201'  
            RETURN  
        END  
          
        INSERT INTO tbMarca (Nome)  
        VALUES (@nome)  
  
        SET @ReturnCode = 'C200'  
    END  
END  
GO
/****** Object:  StoredProcedure [dbo].[updPatrimonio]    Script Date: 08/03/2019 21:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================  
-- Author:      Mauro Somenzari
-- Create date: 08/03/2019
-- Description: Insere ou altera registros de Patrimônio.
-- =============================================  
CREATE PROCEDURE [dbo].[updPatrimonio]  
(  
@idTombo INT,
@Nome NVARCHAR(MAX),
@Descricao NVARCHAR(MAX),
@MarcaId INT,
@ReturnCode NVARCHAR(20) OUTPUT  
)  
AS  
BEGIN  
    
	IF NOT EXISTS (SELECT 1 FROM tbMarca WHERE MarcaId = @MarcaId)
    BEGIN  
        SET @ReturnCode = 'C204'  
        RETURN  
    END  

    IF(@idTombo <> 0)  
    BEGIN  
	       
		IF NOT EXISTS (SELECT 1 FROM tbPatrimonio WHERE idTombo = @idTombo)  
        BEGIN  
            SET @ReturnCode = 'C203'  
            RETURN  
        END
         
        UPDATE tbPatrimonio SET  
        Nome = @Nome,
		Descricao = @Descricao,
		MarcaId = @MarcaId
        WHERE idTombo = @idTombo  
  
        SET @ReturnCode = 'C200'  
    END  
    ELSE  
    BEGIN  
                 
        INSERT INTO tbPatrimonio (Nome, Descricao, MarcaId)  
        VALUES (@Nome, @Descricao, @MarcaId)  
  
        SET @ReturnCode = 'C200'  
    END  
END  
GO
USE [master]
GO
ALTER DATABASE [Partner] SET  READ_WRITE 
GO
