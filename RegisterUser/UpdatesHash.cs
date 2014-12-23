using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CacheService;
using Newtonsoft.Json.Linq;

namespace UpdateUser
{
    public class UpdatesHash
    {
        private readonly ConcurrentQueue<string> _obj;
        private static ConcurrentDictionary<string, List<string>> updatesIndex = new ConcurrentDictionary<string, List<string>>();
        private static ConcurrentDictionary<string, string> GuidVsUpdates = new ConcurrentDictionary<string, string>();
        private static ConcurrentQueue<String> updatesQueue = null;
        private CachingClass obj = new CachingClass();

        public UpdatesHash() 
        {
        }
        public UpdatesHash(ref ConcurrentQueue<String> obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            _obj = obj;
            updatesQueue = obj;
        }

        public void updateChanges()
        {
            
        }
        public void Start()
        {
            String str = String.Empty;
            const string receiverFilter = "update";
            int receiverFilterLength = receiverFilter.Length;
             
            while (true)
            {
                List<string> updatesList = new List<string>();
                if (updatesQueue != null) updatesQueue.TryDequeue(out str);
                if(str == null)
                    continue;
                var guid = Guid.NewGuid().ToString();
                str.Remove(0, receiverFilterLength);
                String UserId = JObject.Parse(str)["userid"].ToString();
                bool isExist = updatesIndex.ContainsKey(UserId);
                updatesList.Add(guid);
                List<String> associatedList = new List<string>();
                if (isExist == true)
                {
                    updatesIndex.TryRemove(UserId, out associatedList);
                  //  updatesIndex.TryGetValue(UserId, out updatesList);
                }
                associatedList.Add(guid);
                GuidVsUpdates.TryAdd(guid, str);
                updatesIndex.TryAdd(UserId, associatedList);
            }
        }
    }
}
