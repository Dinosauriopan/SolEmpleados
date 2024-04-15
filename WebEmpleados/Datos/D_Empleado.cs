using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEmpleados.Models;

namespace WebEmpleados.Datos
{
    public class D_Empleado
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

        public List<E_Empleado> ObtenerTodos()
        {
            //Creando una lista de empleados vacia
            List<E_Empleado> lista = new List<E_Empleado>();
            //Crear la conexión
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            string query = "SELECT idEmpleado,nombre,numeroEmpleado,sueldo,fechaNacimientos,tiempoCompleto FROM Empleados";

            //Creando objeto de la clase SQLCommand
            SqlCommand comando = new SqlCommand(query, conexion);
            //ejecutar el query, creamos un objeto de la clase SqlDataReader para almacenar los resultados.
            SqlDataReader reader = comando.ExecuteReader();

            //Recorremos el conjunto de resultados
            while (reader.Read())
            {
                //Crear un empleado
                E_Empleado empleado = new E_Empleado();
                //Asignarle un valor a sus propiedades, convertir al tipo de dato que necesitamos
                empleado.IdEmpleado = Convert.ToInt32(reader["idEmpleado"]);
                empleado.Nombre = reader["nombre"].ToString();
                empleado.NumeroEmpleado = reader["numeroEmpleado"].ToString();
                empleado.Sueldo = Convert.ToDecimal(reader["sueldo"]);
                empleado.FechaNacimientos = Convert.ToDateTime(reader["fechaNacimientos"]);
                empleado.TiempoCompleto = Convert.ToBoolean(reader["tiempoCompleto"]);

                //Agregamos el empleado de la lista
                lista.Add(empleado);
            }

            conexion.Close();

            return lista;
        }
        public void Agregar(E_Empleado empleado)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                conexion.Open();
                //Creando query con parametros
                string query = "INSERT INTO Empleados(nombre,numeroEmpleado,sueldo,fechaNacimientos,tiempoCompleto)" +
                                    "VALUES(@nombre,@numeroEmpleado,@sueldo,@fechaNacimientos,@tiempoCompleto)";
                //Crear el objeto de la clase SqlCommand
                SqlCommand comando = new SqlCommand(query, conexion);
                //Asignando los valores a los parametros del query
                comando.Parameters.AddWithValue("@nombre", empleado.Nombre);
                comando.Parameters.AddWithValue("@numeroEmpleado", empleado.NumeroEmpleado);
                comando.Parameters.AddWithValue("@sueldo", empleado.Sueldo);
                comando.Parameters.AddWithValue("@fechaNacimientos", empleado.FechaNacimientos);
                comando.Parameters.AddWithValue("@tiempoCompleto", empleado.TiempoCompleto);
                //Ejecutar el query
                comando.ExecuteNonQuery();
                //Cerrar la conexion
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }

        }

        public E_Empleado ObtenerPorId(int id)
        {
            E_Empleado empleado = null;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
                string query = "SELECT idEmpleado, nombre, numeroEmpleado, sueldo, fechaNacimientos, tiempoCompleto FROM Empleados WHERE idEmpleado = @id";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    empleado = new E_Empleado();
                    empleado.IdEmpleado = Convert.ToInt32(reader["idEmpleado"]);
                    empleado.Nombre = reader["nombre"].ToString();
                    empleado.NumeroEmpleado = reader["numeroEmpleado"].ToString();
                    empleado.Sueldo = Convert.ToDecimal(reader["sueldo"]);
                    empleado.FechaNacimientos = Convert.ToDateTime(reader["fechaNacimientos"]);
                    empleado.TiempoCompleto = Convert.ToBoolean(reader["tiempoCompleto"]);
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
            return empleado;
        }

        public void Actualizar(E_Empleado empleado)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            try
            {
                
                string query = "UPDATE Empleados SET nombre = @nombre, numeroEmpleado = @numeroEmpleado, sueldo = @sueldo, fechaNacimientos = @fechaNacimientos, tiempoCompleto = @tiempoCompleto WHERE idEmpleado = @id";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", empleado.IdEmpleado);
                comando.Parameters.AddWithValue("@nombre", empleado.Nombre);
                comando.Parameters.AddWithValue("@numeroEmpleado", empleado.NumeroEmpleado);
                comando.Parameters.AddWithValue("@sueldo", empleado.Sueldo);
                comando.Parameters.AddWithValue("@fechaNacimientos", empleado.FechaNacimientos);
                comando.Parameters.AddWithValue("@tiempoCompleto", empleado.TiempoCompleto);
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }

        public List<E_Empleado> Buscar(string texto)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            List<E_Empleado> listaEmpleados = new List<E_Empleado>();
            try
            {
                conexion.Open() ;
                string query = "SELECT idEmpleado, nombre, numeroEmpleado, sueldo, fechaNacimientos,tiempoCompleto FROM Empleados WHERE nombre LIKE @texto";

                SqlCommand comando = new SqlCommand( query, conexion);
                comando.Parameters.AddWithValue("@texto", "%" + texto + "%");

                //Ejecutamos el query
                SqlDataReader reader = comando.ExecuteReader();
                //Recorremos el conjunto de resultados
                while (reader.Read())
                {
                    //Crear un empleado
                    E_Empleado empleado = new E_Empleado();
                    //Asignarle un valor a sus propiedades, convertir al tipo de dato que necesitamos
                    empleado.IdEmpleado = Convert.ToInt32(reader["idEmpleado"]);
                    empleado.Nombre = reader["nombre"].ToString();
                    empleado.NumeroEmpleado = reader["numeroEmpleado"].ToString();
                    empleado.Sueldo = Convert.ToDecimal(reader["sueldo"]);
                    empleado.FechaNacimientos = Convert.ToDateTime(reader["fechaNacimientos"]);
                    empleado.TiempoCompleto = Convert.ToBoolean(reader["tiempoCompleto"]);

                    //Agregamos el empleado de la lista
                    listaEmpleados.Add(empleado);
                }
                conexion.Close();
                return listaEmpleados;
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }

    }
}