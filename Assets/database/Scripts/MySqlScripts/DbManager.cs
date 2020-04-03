using MySql.Data.MySqlClient;
using System;
using UnityEngine;
using UnityEngine.UI;



public class DbManager : MonoBehaviour
{

    public DatabaseConnection databaseConnection;

    [Header("User Data")]
    public InputField userName;
    public InputField userMail;

    MySqlDataReader rdr;


    public void InsertData()
    {
        if (userName.text != "" && userMail.text != "")
        {
            if (databaseConnection.OpenConnection(databaseConnection))
            {
                MySqlCommand comm = databaseConnection.dbSQLConnection.CreateCommand();
                comm.CommandText = "INSERT INTO testdetails(name,mail) " +
                           "Values(@name,@mail)";
                comm.Parameters.AddWithValue("@name", userName.text);
                comm.Parameters.AddWithValue("@mail", userMail.text);

                try
                {
                    if (comm.ExecuteNonQuery() == 0)
                    {
                        print("The Query Not Completed");
                    }

                    else
                    {
                        print("Mission Completed");

                    }
                }
                catch (Exception e)
                {
                    print(e.Message);
                }
            }
            else
            {
                print("Error at connection");

            }
        }

    }

    public void UpdateData()
    {
        if (userName.text != "" && userMail.text != "")
        {
            if (databaseConnection.OpenConnection(databaseConnection))
            {
                MySqlCommand comm = databaseConnection.dbSQLConnection.CreateCommand();
                comm.CommandText = "Update testdetails Set name ='" + userName.text + "'" +
                    ",mail='" + userMail.text + "' Where name ='" + userName.text + "'";
                try
                {
                    if (comm.ExecuteNonQuery() == 0)
                    {
                        print("Sorry Query not completed");
                    }

                    else
                    {
                        print("Data Updated successfuly");

                    }
                }
                catch (Exception e)
                {
                    print(e.Message);
                }
            }
            else
            {
                print("Sorry there's problem with connection");
            }
        }



    }

    public void SelectUser()
    {
        if (userName.text != "")
        {
            if (databaseConnection.OpenConnection(databaseConnection))
            {

                string query = "SELECT mail,name from testdetails" +
                    " WHERE name = '" + userName.text + "'";
                rdr = excuteQuery(query);

                try
                {

                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        print("logined");
                        rdr.Close();
                        databaseConnection.CloseConnection();
                    }
                }
                catch (Exception e)
                {
                    print(e.Message);
                }
            }
            else
            {
                databaseConnection.CloseConnection();
                return;
            }
        }
    }

    public void DeleteUser()
    {
        if (userName.text != "")
        {
            if (databaseConnection.OpenConnection(databaseConnection))
            {
                MySqlCommand comm = databaseConnection.dbSQLConnection.CreateCommand();
                comm.CommandText = "Delete From testdetails Where name= '" + userName.text + "' ";
                try
                {
                    if (comm.ExecuteNonQuery() == 0)
                    {
                        print("Sorry Query not completed");

                    }
                    else
                    {
                        print("Data Deleted successfuly");

                    }
                }
                catch (Exception e)
                {
                    print(e.Message);

                }
            }
            else
            {
                print("Sorry there's problem with connection");

            }
        }

    }

    MySqlDataReader excuteQuery(string query)
    {
        MySqlCommand comm = new MySqlCommand(query, databaseConnection.dbSQLConnection);
        comm.CommandTimeout = 60;
        return comm.ExecuteReader();
    }
}