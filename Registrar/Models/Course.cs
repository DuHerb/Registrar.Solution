using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Registrar.Models
{
  public class Course
  {
    public string Name {get; set;}
    public int Id {get; set;}
    public string CourseNumber {get; set;}

    public Course(string name, string courseNum, int id = 0)
    {
      this.Name = name;
      this.CourseNumber = courseNum;
      this.Id = id;
    }

    // public static void ClearAll()
    // {
    //
    // }

    public static Course Find(int courseId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM courses WHERE id = @courseId;";
      cmd.Parameters.AddWithValue("@courseId", courseId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      rdr.Read();
      int id = rdr.GetInt32(0);
      string name = rdr.GetString(1);
      string courseNumber = rdr.GetString(2);
      Course foundCourse = new Course(name, courseNumber, id);
      conn.Close();
      return foundCourse;
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM courses;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {

        int id = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNum = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNum, id);
        allCourses.Add(newCourse);
        }
      conn.Close();
      return allCourses;
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
      cmd.CommandText = @"INSERT INTO courses (name, courseNum) VALUES (@courseName, @courseNum);";
      cmd.Parameters.AddWithValue("@courseName", this.Name);
      cmd.Parameters.AddWithValue("@courseNum", this.CourseNumber);
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
