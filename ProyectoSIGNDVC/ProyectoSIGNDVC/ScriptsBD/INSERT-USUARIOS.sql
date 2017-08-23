USE [test]
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON
INSERT INTO [dbo].[Usuarios]
           ([usuarioID]
		   ,[email]
           ,[usuario]
           ,[clave]
           ,[EmpleadoID])
     VALUES
           (1
		   ,'carlos.elp94@gmail.com'
           ,'admin'
           ,'123456'
           ,1)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO


