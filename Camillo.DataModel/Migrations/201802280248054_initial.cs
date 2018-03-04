namespace Camillo.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        LastLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        TreatmentId = c.Long(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPayed = c.Boolean(nullable: false),
                        PaymentDate = c.DateTime(),
                        PaymentMethod = c.String(),
                        PatientId = c.Int(nullable: false),
                        Patient_AccountId = c.Int(),
                        Treatment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_AccountId)
                .ForeignKey("dbo.Treatments", t => t.Treatment_Id)
                .Index(t => t.Patient_AccountId)
                .Index(t => t.Treatment_Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MedicareNumber = c.String(),
                        Gender = c.Int(nullable: false),
                        Address = c.String(),
                        Telephone = c.String(),
                        Status = c.Int(nullable: false),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Symptoms = c.String(),
                        Description = c.String(),
                        Severity = c.Int(nullable: false),
                        StaffId = c.String(),
                        PatienteId = c.Int(nullable: false),
                        Patient_AccountId = c.Int(),
                        Staff_AccountId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_AccountId)
                .ForeignKey("dbo.Staffs", t => t.Staff_AccountId)
                .Index(t => t.Patient_AccountId)
                .Index(t => t.Staff_AccountId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        Address = c.String(),
                        Telephone = c.String(),
                        Department = c.String(),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Treatments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        DiagnosisId = c.Long(nullable: false),
                        PatientId = c.Int(nullable: false),
                        Diagnosis_Id = c.Int(),
                        Patient_AccountId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diagnosis", t => t.Diagnosis_Id)
                .ForeignKey("dbo.Patients", t => t.Patient_AccountId)
                .Index(t => t.Diagnosis_Id)
                .Index(t => t.Patient_AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Treatments", "Patient_AccountId", "dbo.Patients");
            DropForeignKey("dbo.Treatments", "Diagnosis_Id", "dbo.Diagnosis");
            DropForeignKey("dbo.Bills", "Treatment_Id", "dbo.Treatments");
            DropForeignKey("dbo.Diagnosis", "Staff_AccountId", "dbo.Staffs");
            DropForeignKey("dbo.Staffs", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Diagnosis", "Patient_AccountId", "dbo.Patients");
            DropForeignKey("dbo.Bills", "Patient_AccountId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Treatments", new[] { "Patient_AccountId" });
            DropIndex("dbo.Treatments", new[] { "Diagnosis_Id" });
            DropIndex("dbo.Staffs", new[] { "Account_Id" });
            DropIndex("dbo.Diagnosis", new[] { "Staff_AccountId" });
            DropIndex("dbo.Diagnosis", new[] { "Patient_AccountId" });
            DropIndex("dbo.Patients", new[] { "Account_Id" });
            DropIndex("dbo.Bills", new[] { "Treatment_Id" });
            DropIndex("dbo.Bills", new[] { "Patient_AccountId" });
            DropTable("dbo.Treatments");
            DropTable("dbo.Staffs");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.Patients");
            DropTable("dbo.Bills");
            DropTable("dbo.Accounts");
        }
    }
}
