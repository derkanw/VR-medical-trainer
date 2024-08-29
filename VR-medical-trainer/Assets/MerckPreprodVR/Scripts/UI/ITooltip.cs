using Orbox.Async;

namespace MerckPreprodVR
{
    public interface ITooltip : IModel
    {
        void Update();
        void Reactivate();
        IPromise Complete();
    }
}