namespace ProyectoSIGNDVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1234 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Personas", name: "Empleado_EmpleadoID", newName: "Fk_EmpleadoID");
            RenameIndex(table: "dbo.Personas", name: "IX_Empleado_EmpleadoID", newName: "IX_Fk_EmpleadoID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Personas", name: "IX_Fk_EmpleadoID", newName: "IX_Empleado_EmpleadoID");
            RenameColumn(table: "dbo.Personas", name: "Fk_EmpleadoID", newName: "Empleado_EmpleadoID");
        }
    }
}
