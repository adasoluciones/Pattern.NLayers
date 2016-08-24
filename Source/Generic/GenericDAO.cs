using Ada.Framework.Data.DBConnector;
using Ada.Framework.Data.DBConnector.Connections;
using Ada.Framework.Data.DBConnector.Queries;
using System;
using System.Collections.Generic;

namespace Ada.Framework.Pattern.NLayers.Generic
{
    public abstract class GenericDAO<T> : IDAO<T> where T : IBusinessEntity
    {
        public abstract string NombreConexion { get; }
        public abstract T Mapear(IDictionary<string, object> registro);
        public abstract void AgregarParametros(T entidad, Query consulta);
        
        public ConexionBaseDatos Conexion { get; protected set; }

        public GenericDAO()
        {
            Conexion = ConfiguracionBaseDatosFactory.ObtenerConfiguracionDeBaseDatos().ObtenerConexionBaseDatos(NombreConexion);
            Conexion.AutoConectarse = true;
        }

        public T Agregar(T entidad)
        {
            try
            {
                Conexion.Abrir();

                DynamicQuery consulta = Conexion.CrearQueryDinamica();
                AgregarParametros(entidad, consulta);

                IDictionary<string, object> registro = consulta.Obtener();
                entidad = Mapear(registro);

                return entidad;
            }
            finally
            {
                Conexion.Cerrar();
            }
        }

        public void Eliminar(T entidad)
        {
            try
            {
                Conexion.Abrir();

                DynamicQuery consulta = Conexion.CrearQueryDinamica();
                AgregarParametros(entidad, consulta);

                consulta.Ejecutar();
            }
            finally
            {
                Conexion.Cerrar();
            }
        }

        public bool Existe(T entidad)
        {
            try
            {
                Conexion.Abrir();

                DynamicQuery consulta = Conexion.CrearQueryDinamica();
                AgregarParametros(entidad, consulta);

                int respuesta = Convert.ToInt32(consulta.Obtener()["Respuesta"].ToString());
                return (respuesta == 1);
            }
            finally
            {
                Conexion.Cerrar();
            }
        }

        public T Modificar(T entidad)
        {
            try
            {
                Conexion.Abrir();

                DynamicQuery consulta = Conexion.CrearQueryDinamica();
                AgregarParametros(entidad, consulta);

                IDictionary<string, object> registro = consulta.Obtener();
                entidad = Mapear(registro);

                return entidad;
            }
            finally
            {
                Conexion.Cerrar();
            }
        }

        public IList<T> ObtenerSegunFiltro(T filtro)
        {
            IList<T> retorno = new List<T>();

            try
            {
                Conexion.Abrir();

                DynamicQuery consulta = Conexion.CrearQueryDinamica();
                AgregarParametros(filtro, consulta);

                foreach (IDictionary<string, object> registro in consulta.Consultar())
                {
                    T entidad = Mapear(registro);
                    retorno.Add(entidad);
                }
            }
            finally
            {
                Conexion.Cerrar();
            }

            return retorno;
        }

        public T ObtenerSegunID(T entidad)
        {
            try
            {
                Conexion.Abrir();

                DynamicQuery consulta = Conexion.CrearQueryDinamica();
                AgregarParametros(entidad, consulta);

                IDictionary<string, object> registro = consulta.Obtener();
                entidad = Mapear(registro);
            }
            finally
            {
                Conexion.Cerrar();
            }

            return entidad;
        }

        public IList<T> ObtenerTodos()
        {
            IList<T> retorno = new List<T>();

            try
            {
                Conexion.Abrir();

                DynamicQuery consulta = Conexion.CrearQueryDinamica();

                foreach (IDictionary<string, object> registro in consulta.Consultar())
                {
                    T entidad = Mapear(registro);
                    retorno.Add(entidad);
                }
            }
            finally
            {
                Conexion.Cerrar();
            }

            return retorno;
        }
    }
}
