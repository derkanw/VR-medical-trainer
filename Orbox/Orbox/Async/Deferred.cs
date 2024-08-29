using System;
using System.Collections.Generic;
using System.Linq;

using Orbox.Collections;

namespace Orbox.Async
{

    public class Deferred : BaseDeferred , IPromise
    {
        public IPromise Resolve()
        {
            if (State != Status.Pending)
                throw new InvalidOperationException();

            State = Status.Resolved;

            foreach (var callback in DoneCallbacks)
                callback();

            ClearCallbacks();

            return this;
        }


        public IPromise Reject()
        {
            if (State != Status.Pending)
                throw new InvalidOperationException();

            State = Status.Rejected;

            foreach (var callback in FailCallbacks)
                callback();

            ClearCallbacks();

            return this;
        }

        public virtual IPromise Promise()
        {
            return this;
        }

        public static IPromise All(params IPromise[] collection)
        {
            var deferred = new Deferred();
            var promises = Array.ConvertAll(collection, promice => promice as BaseDeferred);
           
            if (collection.Length == 0)
                deferred.Resolve();

            foreach (var promise in promises)
            {
                promise.Done(() =>
                {
                    if (deferred.State == Status.Pending && promises.All(p => IsResolved(p)))
                        deferred.Resolve();
                });

                promise.Fail(() =>
                {
                    if(deferred.State == Status.Pending)
                        deferred.Reject();
                });
            }

            return deferred;

        }

        //First will be Resolved, others well be Reseted
        public static IPromise Race(params IPromise[] collection)
        {
            var race = new Deferred();
            var promises = Array.ConvertAll(collection, promice => promice as BaseDeferred);

            BaseDeferred last = null;

            foreach (var promise in promises)
            {
                last = promise;
                var self = promise;

                if (IsResolved(last))
                    break;

                promise.Done(() =>
                {
                    foreach (var item in promises)
                        if (IsPending(item) && item != self) // TODO: item != self is always true because done
                            Reset(item);

                    race.Resolve();
                });

                promise.Fail(() =>
                {
                    if (promises.All(p => IsRejected(p)))
                        race.Reject();
                });
            }

            if (IsResolved(last))
            {
                foreach (var item in promises)
                    if (IsPending(item) && item != last)
                        Reset(item);
                        //item.ClearCallbacks();

                race.Resolve();
            }

            return race.Promise();
        }

    }

    public class Deferred<T> : BaseDeferred ,  IPromise<T>
    {
        protected T Result;

        public IPromise<T> Resolve(T result)
        {
            if (State != Status.Pending)
                throw new InvalidOperationException();

            Result = result;
            State = Status.Resolved;

            foreach (var callback in DoneCallbacks)
                callback();

            ClearCallbacks();

            return this;
        }


        public IPromise<T> Reject()
        {
            if (State != Status.Pending)
                throw new InvalidOperationException();

            State = Status.Rejected;

            foreach (var callBack in FailCallbacks)
                callBack();

            ClearCallbacks();

            return this;
        }

        public IPromise<T> Done(Action<T> callback)
        {            
            if (State == Status.Resolved)
                callback(Result);

            if (State == Status.Pending)
                DoneCallbacks.Add(() => callback(Result));
                
            return this;
        }

        public IPromise<TNext> Then<TNext>(Func<T,IPromise<TNext>> next)
        {
            var deferred = new Deferred<TNext>();

            if (Disposer != null)
            {
                deferred.Disposer = Disposer;
                deferred.Disposer.Add(deferred.ClearCallbacks);
            }

            Done(result =>
            {
                var promise = next(result);

                promise.Done(res => deferred.Resolve(res));
                promise.Fail(() => deferred.Reject());

            });

            Fail(() => deferred.Reject());

            return deferred;
        }

        public IPromise<T> Then(Func<T, IPromise> next)
        {

            var deferred = new Deferred<T>();

            if (Disposer != null)
            {
                deferred.Disposer = Disposer;
                deferred.Disposer.Add(deferred.ClearCallbacks);
            }

            Done(res =>
            {
                var promise = next(res);

                promise.Done(() => deferred.Resolve(res));
                promise.Fail(() => deferred.Reject());

            });

            Fail(() => deferred.Reject());

            return deferred;
        }

    }



}

