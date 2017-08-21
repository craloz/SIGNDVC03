USE [test]
GO
SET IDENTITY_INSERT [dbo].[Empleadoes] ON
INSERT INTO [dbo].[Empleadoes]
           ([EmpleadoID]
		   ,[sueldo]
           ,[fecha_ingreso]
           ,[fecha_salida]
           ,[Fk_Direccion]
           ,[Fk_Persona]
           ,[Fk_Cargo])
     VALUES
           (1
		   ,300000
           ,'2017-08-02 00:00:00.000'
           ,'2017-08-02 00:00:00.000'
           ,28
           ,1
           ,1)
SET IDENTITY_INSERT [dbo].[Empleadoes] OFF
GO


