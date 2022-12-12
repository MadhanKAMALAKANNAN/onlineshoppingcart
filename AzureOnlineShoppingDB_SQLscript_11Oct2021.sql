/****** Object:  Database [AzureOnlineShoppingDB]    Script Date: 10/12/2021 12:10:21 AM ******/
DROP DATABASE [AzureOnlineShoppingDB]
GO

/****** Object:  Database [AzureOnlineShoppingDB]    Script Date: 10/12/2021 12:10:22 AM ******/
CREATE DATABASE [AzureOnlineShoppingDB]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET  MULTI_USER 
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET ENCRYPTION ON
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET QUERY_STORE = ON
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO

-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO

ALTER DATABASE [AzureOnlineShoppingDB] SET  READ_WRITE 
GO

