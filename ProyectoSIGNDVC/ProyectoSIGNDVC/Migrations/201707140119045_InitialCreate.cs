namespace ProyectoSIGNDVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        PersonaID = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        apellido = c.String(),
                        cedula = c.Int(nullable: false),
                        fecha_nacimiento = c.DateTime(nullable: false),
                        CargaID = c.Int(),
                        monto_poliza = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Empleado_EmpleadoID = c.Int(),
                    })
                .PrimaryKey(t => t.PersonaID)
                .ForeignKey("dbo.Empleadoes", t => t.Empleado_EmpleadoID)
                .Index(t => t.Empleado_EmpleadoID);
            
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        CargoID = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                    })
                .PrimaryKey(t => t.CargoID);
            
            CreateTable(
                "dbo.Direccions",
                c => new
                    {
                        DireccionID = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        tipo = c.String(),
                        Fk_Direccion = c.Int(),
                    })
                .PrimaryKey(t => t.DireccionID)
                .ForeignKey("dbo.Direccions", t => t.Fk_Direccion)
                .Index(t => t.Fk_Direccion);
            
            CreateTable(
                "dbo.Empleadoes",
                c => new
                    {
                        EmpleadoID = c.Int(nullable: false, identity: true),
                        sueldo = c.Int(nullable: false),
                        fecha_ingreso = c.DateTime(nullable: false, storeType: "date"),
                        fecha_salida = c.DateTime(nullable: false),
                        Fk_Direccion = c.Int(nullable: false),
                        Fk_Persona = c.Int(nullable: false),
                        Fk_Cargo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpleadoID)
                .ForeignKey("dbo.Cargoes", t => t.Fk_Cargo)
                .ForeignKey("dbo.Direccions", t => t.Fk_Direccion)
                .ForeignKey("dbo.Personas", t => t.Fk_Persona)
                .Index(t => t.Fk_Direccion)
                .Index(t => t.Fk_Persona)
                .Index(t => t.Fk_Cargo);
            
            CreateTable(
                "dbo.Pagoes",
                c => new
                    {
                        PagoID = c.Int(nullable: false, identity: true),
                        numero_ref = c.Int(nullable: false),
                        f_pago = c.DateTime(nullable: false),
                        monto = c.Int(nullable: false),
                        aprobado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PagoID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        usuarioID = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        usuario = c.String(nullable: false),
                        clave = c.String(nullable: false),
                        EmpleadoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.usuarioID)
                .ForeignKey("dbo.Empleadoes", t => t.EmpleadoID)
                .Index(t => t.EmpleadoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "EmpleadoID", "dbo.Empleadoes");
            DropForeignKey("dbo.Empleadoes", "Fk_Persona", "dbo.Personas");
            DropForeignKey("dbo.Empleadoes", "Fk_Direccion", "dbo.Direccions");
            DropForeignKey("dbo.Empleadoes", "Fk_Cargo", "dbo.Cargoes");
            DropForeignKey("dbo.Personas", "Empleado_EmpleadoID", "dbo.Empleadoes");
            DropForeignKey("dbo.Direccions", "Fk_Direccion", "dbo.Direccions");
            DropIndex("dbo.Usuarios", new[] { "EmpleadoID" });
            DropIndex("dbo.Empleadoes", new[] { "Fk_Cargo" });
            DropIndex("dbo.Empleadoes", new[] { "Fk_Persona" });
            DropIndex("dbo.Empleadoes", new[] { "Fk_Direccion" });
            DropIndex("dbo.Direccions", new[] { "Fk_Direccion" });
            DropIndex("dbo.Personas", new[] { "Empleado_EmpleadoID" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Pagoes");
            DropTable("dbo.Empleadoes");
            DropTable("dbo.Direccions");
            DropTable("dbo.Cargoes");
            DropTable("dbo.Personas");
        }
    }
}
