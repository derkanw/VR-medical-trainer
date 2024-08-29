using UnityEngine;
using Orbox.Async;

public interface IAsyncOperationWrapper
{
    IPromise WaitForAsyncOperation(AsyncOperation operation);
    IPromise WaitForAsyncOperation(AsyncOperation operation, float timeOut);
    IPromise WaitForAsyncOperation(WWW operation);
    IPromise WaitForAsyncOperation(WWW operation, float timeOut);
}
