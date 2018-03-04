namespace Camillo.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Birthday_Photo_MedicalHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            AddColumn("dbo.Patients", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Patients", "PhotoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalHistories", "AccountId", "dbo.Patients");
            DropIndex("dbo.MedicalHistories", new[] { "AccountId" });
            DropColumn("dbo.Patients", "PhotoUrl");
            DropColumn("dbo.Patients", "BirthDate");
            DropTable("dbo.MedicalHistories");
        }
    }
}
