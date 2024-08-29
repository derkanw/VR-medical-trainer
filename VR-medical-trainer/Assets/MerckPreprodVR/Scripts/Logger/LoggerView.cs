using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Orbox.Async;

namespace MerckPreprodVR
{
    public class LoggerView : MonoBehaviour, ILoggerView
    {
        [SerializeField]
        private Text logText;
        public Text LogText
        {
            get => logText;
            set => logText = value;
        }

        [SerializeField]
        private int visibleMessageCount = 40;
        public int VisibleMessageCount
        {
            get => visibleMessageCount;
            set => visibleMessageCount = value;
        }

        private int _lastMessageCount;

        private List<string> _log = new List<string>();

        private static StringBuilder _stringBuilder = new StringBuilder();

        void Awake()
        {
            if (logText == null)
            {
                logText = GetComponent<Text>();
            }

            lock (_log)
            {
                _log?.Clear();
            }

            Log("Log console initialized.");
        }
        private void UpdateText()
        {
            lock (_log)
            {
                if (_lastMessageCount != _log.Count)
                {
                    _stringBuilder.Clear();
                    var startIndex = Mathf.Max(_log.Count - visibleMessageCount, 0);
                    for (int i = startIndex; i < _log.Count; ++i)
                    {
                        _stringBuilder.Append($"{i:000}> {_log[i]}\n");
                    }

                    var text = _stringBuilder.ToString();

                    if (logText)
                    {
                        logText.text = text;
                    }
                    else
                    {
                        Debug.Log(text);
                    }
                }

                _lastMessageCount = _log.Count;
            }
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
        }

        public void Enable()
        {
            this.gameObject.SetActive(true);
        }

        public void Disable()
        {
            this.gameObject.SetActive(false);
        }

        public void Log(string message)
        {
            lock (_log)
            {
                _log ??= new List<string>();

                _log.Add(message);
                UpdateText();
            }
        }
    }
}
