using UnityEngine;

namespace MerckPreprodVR
{
    public class SimpleLogger : ILogger
    {
        public ILoggerView LoggerView { get; set; }
        
        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}