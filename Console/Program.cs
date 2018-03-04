using Camillo.DataModel;
using Camillo.Domain;
using Camillo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CamilloConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<CamilloContext>());

            InsertNewPatients();
            //InsertNewPatient();
            //InsertNewStaff();


            Console.ReadKey();
        }


        public static void InsertNewPatient()
        {

            var patient = new Patient
            {
                FirstName = "Marco",
                LastName = "Rossi",
                Gender = Gender.Male,
                Address = "60 Torrens Street Werribee, VIC",
                Telephone = "0434477129",
                Status = PatientStatus.None,
                Account = new Account
                {
                    Username = "Marco01",
                    Password = "Xf!Cifr3",
                    CreationDate = DateTime.Now,
                    Email = "marco.rossi@email.com",
                }
            };

            using(var context = new CamilloContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Patients.Add(patient);
                context.SaveChanges();
            }

        }

        public static void InsertNewPatients()
        {

            var mario = new Patient
            {
                FirstName = "Mario",
                LastName = "Bianchi",
                BirthDate = new DateTime(1965,10,2),
                Gender = Gender.Male,
                Address = "60 Red Street Melbourne, VIC",
                Telephone = "0434477559",
                Status = PatientStatus.StillPatient,
                Account = new Account
                {
                    Username = "Mario002",
                    Password = "Xf!Cifr3",
                    CreationDate = DateTime.Now,
                    Email = "mario.bianchi@email.com",
                },
                PhotoUrl = "./assets/images/3.jpg"
            };

            var james = new Patient
            {
                FirstName = "James",
                LastName = "Golding",
                Gender = Gender.Male,
                BirthDate = new DateTime(1980, 8, 9),
                Address = "60 Wild Street Melbourne, VIC",
                Telephone = "04344227129",
                Status = PatientStatus.Discharged,
                Account = new Account
                {
                    Username = "James001",
                    Password = "Xf!Cifr3",
                    CreationDate = DateTime.Now,
                    Email = "james.golding@email.com",
                },
                PhotoUrl = "./assets/images/4.jpg"
            };

            var sofia = new Patient
            {
                FirstName = "Sofia",
                LastName = "Black",
                Gender = Gender.Male,
                BirthDate = new DateTime(1989, 4, 13),
                Address = "60 Torrens Street Werribee, VIC",
                Telephone = "0434477129",
                Status = PatientStatus.Admitted,
                Account = new Account
                {
                    Username = "Marco01",
                    Password = "Xf!Cifr3",
                    CreationDate = DateTime.Now,
                    Email = "sofia.black@email.com",
                },
                PhotoUrl = "./assets/images/1.jpg"
            };

            var cindy = new Patient
            {
                FirstName = "Cindy",
                LastName = "Reed",
                Gender = Gender.Female,
                BirthDate = new DateTime(1988, 3, 21),
                Address = "60 White Street Geelong, VIC",
                Telephone = "0434477129",
                Status = PatientStatus.StillPatient,
                Account = new Account
                {
                    Username = "Cindy003",
                    Password = "Xf!Cifr3",
                    CreationDate = DateTime.Now,
                    Email = "cindy.reed@email.com",
                },
                PhotoUrl = "./assets/images/2.jpg"
            };

            var kathrine = new Patient
            {
                FirstName = "Kathrine",
                LastName = "Papa",
                Gender = Gender.Male,
                BirthDate = new DateTime(1990, 1, 21),
                Address = "60 Green Road Melbourne, VIC",
                Telephone = "0434477100",
                Status = PatientStatus.Discharged,
                Account = new Account
                {
                    Username = "Jo001",
                    Password = "Xf!Cifr3",
                    CreationDate = DateTime.Now,
                    Email = "jo.papa@email.com",
                },
                PhotoUrl = "./assets/images/3.jpg"

            };

            using (var context = new CamilloContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Patients.AddRange(new List<Patient> { mario, james, sofia, cindy, kathrine });
                context.SaveChanges();
            }

        }

        public static void InsertNewStaff()
        {

            var robert = new Staff
            {
                FirstName = "Robert",
                LastName = "Smith",
                Gender = Gender.Male,
                Address = "34 Boulevard Rd, Melbourne, VIC",
                Telephone = "0434477111",
                Role = Role.Doctor,
                Account = new Account
                {
                    Username = "Robert01",
                    Password = "Xf!Cifr1",
                    CreationDate = DateTime.Now,
                    Email = "robert.smith@camillo.com.au",
                }
            };

            var carmela = new Staff
            {
                FirstName = "Carmela",
                LastName = "Rossi",
                Gender = Gender.Male,
                Address = "60 Pacific Rd, Melbourne, VIC",
                Telephone = "0434427151",
                Role = Role.Nurse,
                Account = new Account
                {
                    Username = "Carmela02",
                    Password = "Xf!Cifr2",
                    CreationDate = DateTime.Now,
                    Email = "carmela.rossi@camillo.com.au",
                }
            };

            var angela = new Staff
            {
                FirstName = "Angela",
                LastName = "Green",
                Gender = Gender.Male,
                Address = "3 Walsh Rd, Melbourne, VIC",
                Telephone = "0431378611",
                Role = Role.Receptionist,
                Account = new Account
                {
                    Username = "Robert01",
                    Password = "Xf!Cifr3",
                    CreationDate = DateTime.Now,
                    Email = "angela.green@camillo.com.au",
                }
            };

            using (var context = new CamilloContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Staffs.AddRange(new List<Staff>{ robert, carmela, angela });
                context.SaveChanges();
            }

        }

    }
}
