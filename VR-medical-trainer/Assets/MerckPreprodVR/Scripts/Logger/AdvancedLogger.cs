using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MerckPreprodVR
{
    // Class that delivers logging to MonoBehaviour.
    public class AdvancedLogger : ILogger
    {
        private ILoggerView _loggerView;

        public AdvancedLogger(IViewFactory viewFactory)
        {
            _loggerView = viewFactory.CreateLoggerView();
        }
        public void Log(string message)
        {
            _loggerView.Log("Message: " + message);
        }

        public void LogError(string message)
        {
            _loggerView.Log("Error: " + message);
        }
    }
}
