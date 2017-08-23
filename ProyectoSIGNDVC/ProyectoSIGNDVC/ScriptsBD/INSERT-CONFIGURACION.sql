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


