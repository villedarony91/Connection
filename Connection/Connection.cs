using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Diagnostics;
using System.Configuration;
using System.Data;
using System.Windows;
namespace Connection
{
    /// <summary>
    /// Maneja las funciones básicas de base de datos CRUD y la conexión
    /// </summary>
    public class ConnectDB
    {
        TextWriterTraceListener myTraceWriter = new TextWriterTraceListener();

        /// <summary>
        /// Obtiene la cadena de conexión del app.config
        /// </summary>
        /// <returns>Una conexión lista para ser usada</returns>
        private OleDbConnection getConnectionString()
        {
            Decryption dec = new Decryption();
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = dec.decryption(ConfigurationManager.ConnectionStrings["tests"].ToString());
            return connection;
        }

        /// <summary>
        /// Ejecuta consulta de select
        /// </summary>
        /// <param name="query">La consulta</param>
        /// <returns>Datatable con el resultadao</returns>
        public System.Data.DataTable executeQuery(String query)
        {
            DataTable table = new DataTable();
            OleDbConnection connection = getConnectionString();
            using (connection)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    table.Load(command.ExecuteReader());
                    return table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error durante la consulta a la base de datos" + ex.ToString());
                    return table;
                }
            }
        }

        /// <summary>
        /// Revisa si la consulta devuelve algún valor
        /// </summary>
        /// <param name="query">consulta a realizar</param>
        /// <returns>Verdadero si devuelve algún valor</returns>
        public Boolean returnValue(String query)
        {
            OleDbConnection connection = getConnectionString();
            using (connection)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    if (command.ExecuteReader().HasRows)
                    {
                        MessageBox.Show("True");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("False");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error durante la consulta a la base de datos " + ex.ToString());
                    return false;
                }
            }
        }

        /// <summary>
        /// Realiza las operaciones de insert y update
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transact"></param>
        /// <returns>número de filas afectadas por el comando</returns>
        public int insertOrUpdate(String query, Boolean transact)
        {
            OleDbConnection connection = getConnectionString();
            OleDbTransaction myTransaction = new OleDbTransaction();
            int rowsAffected = 0;
            using (connection)
            {
                try
                {
                    if (transact)
                    {
                        myTransaction = connection.BeginTransaction();
                    }
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    rowsAffected = command.ExecuteNonQuery();
                    if (transact)
                    {
                        myTransaction.Commit();
                    }
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error durante la escritura a la base de datos " + ex.ToString());
                    if (transact)
                    {
                        myTransaction.Rollback();
                    }
                    return rowsAffected;
                }
            }

        }

    }
}
