using Orbox.Utils;

namespace MerckPreprodVR
{
    public class Storage : IStorage
    {
        private IEnumCache EnumCache;

        public Storage(IEnumCache enumCache)
        {
            EnumCache = enumCache;
        }
        
    }
}