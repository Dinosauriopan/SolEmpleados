using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEmpleados.Datos;
using WebEmpleados.Models;

namespace WebEmpleados.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
            //Creamos un objeto de la capa de datos
            D_Empleado datos = new D_Empleado();

            //Obtener la lista de los datos
            List<E_Empleado> lista = datos.ObtenerTodos();

            return View("Consulta", lista);
        }

        public ActionResult IrAgregar() 
        {
            return View("Agregar");
        }

        public ActionResult Agregar(E_Empleado objEmpleado)
        {
            try
            {
                D_Empleado datos = new D_Empleado();
                datos.Agregar(objEmpleado);
                TempData["mensaje"] = $"El empleado {objEmpleado.Nombre} fue registrado";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;

            }
            return RedirectToAction("Index");
        }

        public ActionResult IrEditar(int id)
        {
            D_Empleado datos = new D_Empleado();
            E_Empleado empleado = datos.ObtenerPorId(id); 
            return View("Editar", empleado);
        }

        public ActionResult GuardarCambios(E_Empleado empleado)
        {
            try
            {
                D_Empleado datos = new D_Empleado();
                datos.Actualizar(empleado); 
                TempData["mensaje"] = $"Los cambios para el empleado {empleado.Nombre} fueron guardados";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Buscar(string textoBusqueda)
        {
            try
            {
                D_Empleado datos = new D_Empleado();
                List<E_Empleado> lista = datos.Buscar(textoBusqueda);
                return View("Consulta", lista);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }

            
        }
    }
}