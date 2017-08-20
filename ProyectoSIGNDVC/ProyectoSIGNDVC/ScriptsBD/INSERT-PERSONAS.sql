USE [test]
GO
SET IDENTITY_INSERT [dbo].[Personas] ON
INSERT INTO [dbo].[Personas]
           ([PersonaID],
		   [nombre]
           ,[apellido]
           ,[cedula]
           ,[sexo]
           ,[fecha_nacimiento])
     VALUES
           (1,
		   'NombrePrueba',
			'ApellidoPrueba',
			21194178,
			'Masculino',
			'2017-08-02 00:00:00.000')
SET IDENTITY_INSERT [dbo].[Personas] OFF
GO


