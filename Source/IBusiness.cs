using Ada.Framework.Data.Notifications;
using System.Collections.Generic;

namespace Ada.Framework.Pattern.NLayers
{
    public interface IBusiness<T> where T: IBusinessEntity
    {
        Notificacion<T> Agregar(T entidad);
        Notificacion<T> Modificar(T entidad);
        Notificacion Eliminar(T entidad);
        Notificacion<bool> Existe(T entidad);
        Notificacion<IList<T>> ObtenerTodos();
        Notificacion<T> ObtenerSegunID(T entidad);
        Notificacion<IList<T>> ObtenerSegunFiltro(T filtro);
    }
}
