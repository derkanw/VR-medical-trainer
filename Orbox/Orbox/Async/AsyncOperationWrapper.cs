using UnityEngine;
using System.Collections;
using System;

namespace Orbox.Async
{
    public class AsyncOperationWrapper : MonoBehaviour, IAsyncOperationWrapper
    {
        private const float maxGlobalWaitTimeInSeconds = 600f;

        public IPromise WaitForAsyncOperation(AsyncOperation operation)
        {
            Deferred deferred = new Deferred();
            StartCoroutine
            (
                WaitForAsyncOperationCoroutine
                (
                    operation,
                    maxGlobalWaitTimeInSeconds,
                    () => deferred.Resolve(),
                    () => deferred.Reject()
                )
            );
            return deferred;
        }

        public IPromise WaitForAsyncOperation(AsyncOperation operation, float timeOut)
        {
            Deferred deferred = new Deferred();
            StartCoroutine
            (
                WaitForAsyncOperationCoroutine
                (
                    operation,
                    Mathf.Min(maxGlobalWaitTimeInSeconds, timeOut),
                    () => deferred.Resolve(),
                    () => deferred.Reject()
                )
            );
            return deferred;
        }

        public IPromise WaitForAsyncOperation(WWW operation)
        {
            Deferred deferred = new Deferred();
            StartCoroutine
            (
                WaitForAsyncOperationCoroutine
                (
                    operation,
                    maxGlobalWaitTimeInSeconds,
                    () => deferred.Resolve(),
                    () => deferred.Reject()
                )
            );
            return deferred;
        }

        public IPromise WaitForAsyncOperation(WWW operation, float timeOut)
        {
            Deferred deferred = new Deferred();
            StartCoroutine
            (
                WaitForAsyncOperationCoroutine
                (
                    operation,
                    Mathf.Min(maxGlobalWaitTimeInSeconds, timeOut),
                    () => deferred.Resolve(),
                    () => deferred.Reject()
                )
            );
            return deferred;
        }

        private IEnumerator WaitForAsyncOperationCoroutine(AsyncOperation operation, float timeOut, Action onSuccess, Action onTimeout)
        {
            DateTime endWaitTime = DateTime.UtcNow.AddSeconds(timeOut);
            while (true)
            {
                if (operation.isDone)
                {
                    onSuccess();
                    break;
                }
                if (DateTime.UtcNow > endWaitTime)
                {
                    onTimeout();
                    break;
                }
                yield return null;
            }
        }

        private IEnumerator WaitForAsyncOperationCoroutine(WWW operation, float timeOut, Action onSuccess, Action onTimeout)
        {
            DateTime endWaitTime = DateTime.UtcNow.AddSeconds(timeOut);
            while (true)
            {
                if (operation.isDone)
                {
                    onSuccess();
                    break;
                }
                if (DateTime.UtcNow > endWaitTime)
                {
                    onTimeout();
                    break;
                }
                yield return null;
            }
        }
    }
}