using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Registrar.Models
{
  public class Major
  {
    public string Name {get; set;}
    public int Id {get; set;}

    public Major(string name, int id = 0)
    {
      this.Name = name;
      this.Id = id;
    }

    // public static void ClearAll()
    // {
    //
    // }

    public static Major Find(int majorId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM majors WHERE id = @majorId;";
      cmd.Parameters.AddWithValue("@majorId", majorId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      rdr.Read();
      int id = rdr.GetInt32(0);
      string name = rdr.GetString(1);
      Major foundMajor = new Major(name, id);
      conn.Close();
      return foundMajor;
    }

    public static List<Major> GetAll()
    {
      List<Major> allMajors = new List<Major> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM majors;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {

        int id = rdr.GetInt32(0);
        string majorName = rdr.GetString(1);
        Major newMajor = new Major(majorName, id);
        allMajors.Add(newMajor);
        }
      conn.Close();
      return allMajors;
    }

    public static void AssignCourse(int majorId, int courseId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO majors_courses (major_id, course_id) VALUES (@majorId, @courseId);";
      cmd.Parameters.AddWithValue("@majorId", majorId);
      cmd.Parameters.AddWithValue("@courseId", courseId);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Course> GetCourses(int majorId)
    {
      List<Course> majorCourses = new List<Course> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT courses.* FROM majors JOIN majors_courses ON (majors.id = majors_courses.major_id) JOIN courses ON (majors_courses.course_id = courses.id) WHERE majors.id = @majorId;";
      cmd.Parameters.AddWithValue("@majorId", majorId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {

        int id = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, id);
        majorCourses.Add(newCourse);
        }
      conn.Close();
      return majorCourses;
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
      cmd.CommandText = @"INSERT INTO majors (name) VALUES (@majorName);";
      cmd.Parameters.AddWithValue("@majorName", this.Name);
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
