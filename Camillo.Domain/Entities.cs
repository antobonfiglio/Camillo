using System;
using System.Collections.Generic;
using Camillo.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Camillo.Domain
{

    public class Account
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
    }

    public class Patient
    {

        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicareNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string PhotoUrl { get; set; }
        public PatientStatus Status { get; set; }
        public Account Account { get; set; }
        public IList<Diagnosis> Diagnosis { get; set; }
        public IList<Treatment> Treatments { get; set; }
        public IList<Bill> Bills { get; set; }
        public IList<MedicalHistory> MedicalHistories { get; set; }
    }

    public class MedicalHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string description { get; set; }
        public int AccountId { get; set; }
        public Patient Patient { get; set; }
    }

    public class Diagnosis
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Symptoms { get; set; }
        public string Description { get; set; }
        public Severity Severity { get; set; }
        public string StaffId { get; set; }
        public Staff Staff { get; set; }
        public int PatienteId { get; set; }
        public Patient Patient { get; set; }
        public IList<Treatment> Treatments { get; set; }
    }

    public class Treatment
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public long DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public IList<Bill> Bills { get; set; }
    }

    public class Staff
    {
        public int AccountId { get; set; }
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Department { get; set; }
        public Account Account { get; set; }
    }

    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public long TreatmentId { get; set; }
        public Decimal Amount { get; set; }
        public bool IsPayed { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
    

    public class ApiUser{

        public long Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public string AppId { get; set; }
        public IList<AuthToken> AuthTokens { get; set; }
    }

    public class AuthToken
    {
        public long Id { get; set; }

        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string ApiUser_Id { get; set; }

        public ApiUser ApiUser { get; set; }
    }
}
