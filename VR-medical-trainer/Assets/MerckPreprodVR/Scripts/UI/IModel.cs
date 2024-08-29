using Orbox.Async;

namespace MerckPreprodVR
{
    public interface IModel
    {
        IPromise Show();
        IPromise Hide();
    }
}