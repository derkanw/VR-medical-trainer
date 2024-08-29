using System;
using System.Collections.Generic;

using Orbox.Collections;

namespace Orbox.Async
{

    public abstract class BaseDeferred: IPromise
    {
        protected enum Status
        {
            Pending,
            Resolved,
            Rejected
        }

        protected Status State;
        protected IDisposer Disposer;

        protected List<Action> DoneCallbacks = new ZeroList<Action>();
        protected List<Action> FailCallbacks = new ZeroList<Action>();


        public BaseDeferred()
        {
            State = Status.Pending;
        }

        public IPromise Done(Action callback)
        {
            if (State == Status.Resolved)
                callback();

            if (State == Status.Pending)
                DoneCallbacks.Add(callback);
                          
            return this;
        }

        public IPromise Fail(Action callback)
        {
            if (State == Status.Rejected)
                callback();

            if (State == Status.Pending)
                FailCallbacks.Add(callback);

            return this;
        }

        public IPromise Always(Action callback)
        {            
            if (State == Status.Rejected || State == Status.Resolved)
                callback();

            if (State == Status.Pending)
            {
                DoneCallbacks.Add(callback);
                FailCallbacks.Add(callback);
            }
                
            return this;
        }

        public IPromise Then(Func<IPromise> next)
        {
            var deferred = new Deferred();

            if(Disposer != null)
            {
                deferred.Disposer = Disposer;
                deferred.Disposer.Add(deferred.ClearCallbacks);
            }

            Done(() =>
            {
                var promise = next();

                promise.Done(() => deferred.Resolve());
                promise.Fail(() => deferred.Reject());

            });

            Fail(() => deferred.Reject());

            return deferred;
        }

        public IPromise<TNext> Then<TNext>(Func<IPromise<TNext>> next)
        {
            var deferred = new Deferred<TNext>();

            if (Disposer != null)
            {
                deferred.Disposer = Disposer;
                deferred.Disposer.Add(deferred.ClearCallbacks);
            }

            Done(() =>
            {
                var promise = next();

                promise.Done(res => deferred.Resolve(res));
                promise.Fail(() => deferred.Reject());

            });

            Fail(() => deferred.Reject());

            return deferred;
        }





        public IPromise AddDisposer(IDisposer disposer)
        {
            var deferred = new Deferred();

            deferred.Disposer = disposer;
            deferred.Disposer.Add(deferred.ClearCallbacks);

            Done(() => deferred.Resolve());
            Fail(() => deferred.Reject());            

            return deferred;
        }

        //orbox: Cannot use methods bellow because unity compiler errors
        //IPromise Then(Func<IPromise> next);
        //IPromise<TNext> Then<TNext>(Func<IPromise<TNext>> next);

        protected virtual void ClearCallbacks()
        {
            DoneCallbacks.Clear();
            FailCallbacks.Clear();
        }

        protected static void Reset(BaseDeferred baseDeferred)
        {
            baseDeferred.ClearCallbacks();
        }

        protected static bool IsResolved(BaseDeferred baseDeferred)
        {
            return baseDeferred.State == Status.Resolved;
        }

        protected static bool IsPending(BaseDeferred baseDeferred)
        {
            return baseDeferred.State == Status.Pending;

        }

        protected static bool IsRejected(BaseDeferred baseDeferred)
        {
            return baseDeferred.State == Status.Rejected;
        }

    }



}

