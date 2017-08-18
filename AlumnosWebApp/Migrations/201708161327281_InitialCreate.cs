namespace AlumnosWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alumnoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        FotoURL = c.String(),
                        Usuario = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TareaAlumnoes",
                c => new
                    {
                        IdTarea = c.Int(nullable: false),
                        IdAlumno = c.Int(nullable: false),
                        Mensaje = c.String(),
                        ArchivoURL = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        Calificacion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdTarea, t.IdAlumno })
                .ForeignKey("dbo.Alumnoes", t => t.IdAlumno, cascadeDelete: true)
                .ForeignKey("dbo.Tareas", t => t.IdTarea, cascadeDelete: true)
                .Index(t => t.IdTarea)
                .Index(t => t.IdAlumno);
            
            CreateTable(
                "dbo.Tareas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        ArchivoURL = c.String(),
                        FechaPublicacion = c.DateTime(nullable: false),
                        FechaLimite = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TareaAlumnoes", "IdTarea", "dbo.Tareas");
            DropForeignKey("dbo.TareaAlumnoes", "IdAlumno", "dbo.Alumnoes");
            DropIndex("dbo.TareaAlumnoes", new[] { "IdAlumno" });
            DropIndex("dbo.TareaAlumnoes", new[] { "IdTarea" });
            DropTable("dbo.Tareas");
            DropTable("dbo.TareaAlumnoes");
            DropTable("dbo.Alumnoes");
        }
    }
}
