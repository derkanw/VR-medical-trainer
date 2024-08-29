using BNG;
using Orbox.Signals;
using Orbox.Utils;

namespace MerckPreprodVR
{
    public class UserInput : IUserInput,  IUpdatable
    {
        private InputBridge _inputBridge;
        private bool _isLocked;
        public UserInput(IEventPublisher eventsProvider, InputBridge inputBridge)
        {
            _inputBridge = inputBridge;
            eventsProvider.Add(this);
        }
        
        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        public void Update()
        {
            if (_isLocked) return;
            
            _inputBridge.UpdateInputs();
        }
    }
}