
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Registrar.Models
{
  public class Student
  {
    public string Name {get; set;}
    public int Id {get; set;}
    public string EnrollmentDate {get; set;}

    public Student(string name, string enrollmentDate, int id = 0)
    {
      this.Name = name;
      this.EnrollmentDate = enrollmentDate;
      this.Id = id;
    }

    // public static void ClearAll()
    // {
    //
    // }

    public static Student Find(int studentId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students WHERE id = @studentId;";
      cmd.Parameters.AddWithValue("@studentId", studentId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      rdr.Read();
      int id = rdr.GetInt32(0);
      string name = rdr.GetString(1);
      string enroll = rdr.GetString(2);
      Student foundStudent = new Student(name, enroll, id);
      conn.Close();
      return foundStudent;
    }

    public static List<Student> GetAll()
    {
      List<Student> allStudents = new List<Student> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {

        int id = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        string enrollmentDate = rdr.GetString(2);
        Student newStudent = new Student(studentName, enrollmentDate, id);
        allStudents.Add(newStudent);
        }
      conn.Close();
      return allStudents;
    }

    // public override bool Equals(System.Object otherStylist)
    // {
    //   if (!(otherStylist is Stylist))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //     Stylist newStylist = (Stylist) otherStylist;
    //     bool idEquality = (this.GetId() == newStylist.GetId());
    //     bool nameEquality = (this.GetStylistName() == newStylist.GetStylistName());
    //     return (idEquality && nameEquality);
    //   }
    // }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students (name, enrollment) VALUES (@studentName, @enrollmentDate);";
      cmd.Parameters.AddWithValue("@studentName", this.Name);
      cmd.Parameters.AddWithValue("@enrollmentDate", this.EnrollmentDate);
      cmd.ExecuteNonQuery();
      this.Id = (int) cmd.LastInsertedId;
      conn.Close();
    }

    // public List<Client> GetClients()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @id;";
    //   cmd.Parameters.AddWithValue("@id", _id);
    //   List<Client> clients = new List<Client>{};
    //   MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   while(rdr.Read())
    //   {
    //     int id = rdr.GetInt32(0);
    //     string name = rdr.GetString(1);
    //     string phone = rdr.GetString(2);
    //     int stylist_id = rdr.GetInt32(3);
    //     Client newClient = new Client(name, phone, stylist_id);
    //     newClient.SetId(id);
    //     clients.Add(newClient);
    //   }
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return clients;
    // }
  }
}
