using Ada.Framework.Data.Notifications;

namespace Ada.Framework.Pattern.NLayers
{
    public interface IBusinessEntity
    {
        Notificacion<bool> Validar();
    }
}
