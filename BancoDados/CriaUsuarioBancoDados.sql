USE [master]
GO

CREATE LOGIN [AppCadastro] WITH PASSWORD=N'Pas$w0rd', DEFAULT_DATABASE=[AppCadastro], DEFAULT_LANGUAGE=[Português (Brasil)], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO

USE [AppCadastro]
GO

CREATE USER [AppCadastro] FOR LOGIN [AppCadastro]
GO

USE [AppCadastro]
GO

ALTER ROLE [db_owner] ADD MEMBER [AppCadastro]
GO
