using System;

namespace Orbox.Async
{
    //orbox: Cannot use methods bellow because unity compiler errors
    //IPromise Then(Func<IPromise> next);
    //IPromise<TNext> Then<TNext>(Func<IPromise<TNext>> next);


    public interface IPromise
    {        
        IPromise Done(Action callback);        
        IPromise Fail(Action callback);        
        IPromise Always(Action callback);

        IPromise Then(Func<IPromise> next);
        IPromise<TNext> Then<TNext>(Func<IPromise<TNext>> next);

        IPromise AddDisposer(IDisposer disposer);
    }

    public interface IPromise<T> : IPromise
    {        
        IPromise<T> Done(Action<T> callback);

        IPromise<T> Then(Func<T, IPromise> next);
        IPromise<TNext> Then<TNext>(Func<T, IPromise<TNext>> next);
    }

}