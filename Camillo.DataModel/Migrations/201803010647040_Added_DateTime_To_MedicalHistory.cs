namespace Camillo.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_DateTime_To_MedicalHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalHistories", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicalHistories", "Date");
        }
    }
}
