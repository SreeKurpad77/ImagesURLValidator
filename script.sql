USE [master]
GO
/****** Object:  Database [OutputDB]    Script Date: 5/12/2023 12:50:11 AM ******/
CREATE DATABASE [OutputDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OutputDB', FILENAME = N'C:\OutputDB.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OutputDB_log', FILENAME = N'C:\OutputDB_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OutputDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OutputDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OutputDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OutputDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OutputDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OutputDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OutputDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [OutputDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OutputDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OutputDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OutputDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OutputDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OutputDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OutputDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OutputDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OutputDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OutputDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OutputDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OutputDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OutputDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OutputDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OutputDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OutputDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OutputDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OutputDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OutputDB] SET  MULTI_USER 
GO
ALTER DATABASE [OutputDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OutputDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OutputDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OutputDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OutputDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OutputDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OutputDB] SET QUERY_STORE = OFF
GO
USE [OutputDB]
GO
/****** Object:  Table [dbo].[Output]    Script Date: 5/12/2023 12:50:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Output](
	[URI_Part_Number] [nvarchar](50) NULL,
	[URL_Text] [nvarchar](max) NULL,
	[URL_Correct] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [OutputDB] SET  READ_WRITE 
GO
