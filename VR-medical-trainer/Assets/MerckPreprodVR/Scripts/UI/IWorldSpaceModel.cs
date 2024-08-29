using System;

namespace MerckPreprodVR
{
    public interface IWorldSpaceModel : IModel
    {
        event Action ButtonClicked;
    }
}