
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.IO;

namespace TestingProgram
{
    public partial class Firebird
    {
        public void save_inf_file(string path, string text)
        {
            try
            {
                using (FileStream fstream = new FileStream($"{path}\\meetings.txt", FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(text);
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine("Расписание встреч сохранено");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Main(string[] args)
        {
            Firebird a = new Firebird();
            using (OdbcConnection mine = new OdbcConnection("Driver=Firebird/Interbase(r) driver; USER=SYSDBA; Password=masterkey; Database=G:\\KUSP.FDB; Charset=Win1251"))
            {
                mine.Open();


                OdbcCommand command = new OdbcCommand();
                string sqlExpression = "SELECT * FROM recordsdata where (Recordsid = 4 or recordsid=5) ;";
       



                command.CommandText = sqlExpression;
                command.Connection = mine;
                OdbcDataReader my_list;
                my_list = command.ExecuteReader();
                List<object> My_object = new List<object>();

                while (my_list.Read()) // построчно считываем данные
                    {
                    var count = my_list.FieldCount;
                    for (int i = 0; i < count; i++)
                    {
                        My_object.Add(my_list[i] +"|\t");        
                    }
                    My_object.Add("\n");

                    }

                foreach (var item in My_object)
                {
                    Console.Write($"{item}");
                }
      //          Console.WriteLine();



            }
        }
    }
}
