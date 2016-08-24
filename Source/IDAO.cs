using Ada.Framework.Data.DBConnector;
using System.Collections.Generic;

namespace Ada.Framework.Pattern.NLayers
{
    public interface IDAO<T> where T : IBusinessEntity
    {
        ConexionBaseDatos Conexion { get; }

        T Agregar(T entidad);
        T Modificar(T entidad);
        void Eliminar(T entidad);
        bool Existe(T entidad);
        IList<T> ObtenerTodos();
        T ObtenerSegunID(T entidad);
        IList<T> ObtenerSegunFiltro(T filtro);
    }
}
