using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Diagnostics;
using System.Configuration;

namespace Connection
{
    static class ConnectDB 
    {

        private static OleDbConnection getConnection()
        {
            Decryption dec = new Decryption();
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = dec.decryption(ConfigurationManager.ConnectionStrings["tests"].ToString());
            return connection;
        }

        public static void ConnectToDB()
        {
            OleDbConnection connection = getConnection();
            TextWriterTraceListener myTraceListener = new TextWriterTraceListener("trace.log", "myTraceListener");
            myTraceListener.WriteLine("Starting trace");
            using(connection){
                try
                {
                    connection.Open();
                    myTraceListener.WriteLine("State "+connection.State.ToString());
                    Debug.WriteLine("** Conection opened **");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("debug");
                    myTraceListener.WriteLine("A problem occurred during conection"+ex.ToString());
                }

            }
            myTraceListener.Flush();
            myTraceListener.Dispose();
        }

    }
}
