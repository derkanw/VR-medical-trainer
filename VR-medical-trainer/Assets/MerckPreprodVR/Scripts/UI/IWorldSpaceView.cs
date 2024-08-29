using System;

namespace MerckPreprodVR
{
    public interface IWorldSpaceView : IView
    {
        event Action ButtonClicked;
    }
}
