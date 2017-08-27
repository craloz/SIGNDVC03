USE [test]
GO

INSERT INTO [dbo].[Cargoes]
           ([nombre])
     VALUES
           ('Director Ejecutivo');
INSERT INTO [dbo].[Cargoes]
           ([nombre])
     VALUES
           ('Gerente de Proyectos');
INSERT INTO [dbo].[Cargoes]
           ([nombre])
     VALUES
           ('Ejecutiva de Cuentas');
INSERT INTO [dbo].[Cargoes]
           ([nombre])
     VALUES
           ('Especialista IT');

INSERT INTO [dbo].[Cargoes]
           ([nombre])
     VALUES
           ('Asistente de Direcci�n Ejecutiva');

INSERT INTO [dbo].[Cargoes]
           ([nombre])
     VALUES
           ('Coordinadora de Voluntariado');

INSERT INTO [dbo].[Cargoes]
           ([nombre])
     VALUES
           ('Coordinadora de Administraci�n');

INSERT INTO [dbo].[Cargoes]
           ([nombre])
     VALUES
           ('Asistente de Oficina');
INSERT INTO [dbo].[Cargoes]
           ([nombre])
     VALUES
           ('Asistente de Administraci�n');	   
GO


USE [test]
GO

INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Amazonas','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Anzo�tegui','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Apure','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Aragua','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Barinas','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Bol�var','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Carabobo','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Cojedes','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Delta Amacuro','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Distrito Capital','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Falc�n','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Gu�rico','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Lara','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('M�rida','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Miranda','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Monagas','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Nueva Esparta','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Portuguesa','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Sucre','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('T�chira','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Trujillo','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Vargas','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Yaracuy','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Zulia','Estado',null);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Ciudad Test','Ciudad',1);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Calle Test','Calle',25);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Casa Test','Casa',26);
INSERT INTO [dbo].[Direccions]
           ([nombre]
           ,[tipo]
           ,[Fk_Direccion])
     VALUES
           ('Urb COlinas de Test Ciudad Test Calle Test Casa Test','Add',27);

GO








USE [test]
GO

INSERT INTO [dbo].[Configuracions]
           ([sso_aporte]
           ,[sso_retencion]
           ,[rpe_aporte]
           ,[rpe_retencion]
           ,[faov_aporte]
           ,[faov_retencion]
           ,[inces_aporte]
           ,[inces_retencion]
           ,[unid_tributaria]
           ,[bonoalimentacion]
           ,[fecha_inicio_config]
           ,[fecha_fin_config])
     VALUES
           (0.5
           ,0.5
           ,0.5
           ,0.5
           ,0.5
           ,0.5
           ,0.5
           ,0.5
           ,100
           ,100
           ,GETDATE()
           ,null)
GO


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


USE [test]
GO
SET IDENTITY_INSERT [dbo].[Empleadoes] ON
INSERT INTO [dbo].[Empleadoes]
           ([EmpleadoID]
		   ,[Codigo]
		   ,[sueldo]
		   ,[Banco]
		   ,[N_Cuenta]
           ,[fecha_ingreso]
           ,[fecha_salida]
           ,[Fk_Direccion]
           ,[Fk_Persona]
           ,[Fk_Cargo])
     VALUES
           (1
		   ,'E-000001'
		   ,300000
		   ,'Banco De Venezuela'
		   , '0000-00000-000000-00000000'
           ,'2017-08-02 00:00:00.000'
           ,'2017-08-02 00:00:00.000'
           ,28
           ,1
           ,1)
SET IDENTITY_INSERT [dbo].[Empleadoes] OFF
GO


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


