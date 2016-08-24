using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ada.Framework.Data.Notifications;

namespace Ada.Framework.Pattern.NLayers.Generic
{
    public abstract class GenericBusiness<T> : IBusiness<T> where T: IBusinessEntity
    {
        protected abstract IDAO<T> DAO { get; }

        public Notificacion<T> Agregar(T entidad)
        {
            Notificacion<T> retorno = new Notificacion<T>();
            retorno.Respuesta = entidad;
            string nombreEntidad = typeof(T).Name.Replace("TO", string.Empty);

            try
            {
                Notificacion<bool> respuestaExiste = Existe(entidad);

                if (respuestaExiste.HayError || respuestaExiste.HayExcepcion)
                {
                    retorno.Unir(respuestaExiste);
                    return retorno;
                }

                if (respuestaExiste.Respuesta)
                {
                    retorno.AgregarMensaje(nombreEntidad, "Error_Ya_Existe");
                }
                else
                {
                    DAO.Agregar(entidad);
                    retorno.AgregarMensaje(nombreEntidad, "Agregar_OK");
                }
            }
            catch (Exception ex)
            {
                retorno.AgregarMensaje("Mensaje", "Agregar_Error", null, null, null, null, ex);
            }

            return retorno;
        }

        public Notificacion Eliminar(T entidad)
        {
            Notificacion retorno = new Notificacion();
            string nombreEntidad = typeof(T).Name.Replace("TO", string.Empty);

            try
            {
                Notificacion<bool> respuestaExiste = Existe(entidad);

                if (respuestaExiste.HayError || respuestaExiste.HayExcepcion)
                {
                    retorno.Unir(respuestaExiste);
                    return retorno;
                }

                if (respuestaExiste.Respuesta)
                {
                    DAO.Eliminar(entidad);
                    retorno.AgregarMensaje(nombreEntidad, "Eliminar_OK");
                }
                else
                {
                    retorno.AgregarMensaje(nombreEntidad, "Error_No_Existe");
                }
            }
            catch (Exception ex)
            {
                retorno.AgregarMensaje(nombreEntidad, "Eliminar_Error", null, null, null, null, ex);
            }

            return retorno;
        }

        public Notificacion<bool> Existe(T entidad)
        {
            Notificacion<bool> retorno = new Notificacion<bool>();
            string nombreEntidad = typeof(T).Name.Replace("TO", string.Empty);

            try
            {
                retorno.Respuesta = DAO.Existe(entidad);
            }
            catch (Exception ex)
            {
                retorno.AgregarMensaje(nombreEntidad, "Existe_Error", null, null, null, null, ex);
            }

            return retorno;
        }

        public Notificacion<T> Modificar(T entidad)
        {
            Notificacion<T> retorno = new Notificacion<T>();
            retorno.Respuesta = entidad;
            string nombreEntidad = typeof(T).Name.Replace("TO", string.Empty);

            try
            {
                Notificacion<bool> respuestaExiste = Existe(entidad);

                if (respuestaExiste.HayError || respuestaExiste.HayExcepcion)
                {
                    retorno.Unir(respuestaExiste);
                    return retorno;
                }

                if (respuestaExiste.Respuesta)
                {
                    DAO.Modificar(entidad);
                    retorno.AgregarMensaje(nombreEntidad, "Modificar_OK");
                }
                else
                {
                    retorno.AgregarMensaje(nombreEntidad, "Error_No_Existe");
                }
            }
            catch (Exception ex)
            {
                retorno.AgregarMensaje(nombreEntidad, "Modificar_Error", null, null, null, null, ex);
            }

            return retorno;
        }

        public Notificacion<IList<T>> ObtenerSegunFiltro(T filtro)
        {
            Notificacion<IList<T>> retorno = new Notificacion<IList<T>>();
            string nombreEntidad = typeof(T).Name.Replace("TO", string.Empty);

            try
            {
                retorno.Respuesta = DAO.ObtenerSegunFiltro(filtro);
            }
            catch (Exception ex)
            {
                retorno.AgregarMensaje(nombreEntidad, "ObtenerSegunFiltro_Error", null, null, null, null, ex);
            }

            return retorno;
        }

        public Notificacion<T> ObtenerSegunID(T entidad)
        {
            Notificacion<T> retorno = new Notificacion<T>();
            string nombreEntidad = typeof(T).Name.Replace("TO", string.Empty);

            try
            {
                retorno.Respuesta = DAO.ObtenerSegunID(entidad);
            }
            catch (Exception ex)
            {
                retorno.AgregarMensaje(nombreEntidad, "ObtenerSegunID_Error", null, null, null, null, ex);
            }

            return retorno;
        }

        public Notificacion<IList<T>> ObtenerTodos()
        {
            Notificacion<IList<T>> retorno = new Notificacion<IList<T>>();
            string nombreEntidad = typeof(T).Name.Replace("TO", string.Empty);

            try
            {
                retorno.Respuesta = DAO.ObtenerTodos();
            }
            catch (Exception ex)
            {
                retorno.AgregarMensaje(nombreEntidad, "ObtenerTodos_Error", null, null, null, null, ex);
            }

            return retorno;
        }
    }
}
