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
			   ,0001
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


