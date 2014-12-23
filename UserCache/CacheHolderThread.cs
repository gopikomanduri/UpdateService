using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using Caching;
using Newtonsoft.Json.Linq;

namespace CacheService
{
    public class CacheHolderThread
    {
        private LRUCache<string, JObject> users;
        public LRUCache<string, JObject> Users
        {
            get { return users; }
            set { users = value; }
        }

        private LRUCache<string, string> emailCache;
        public LRUCache<string, string> EmailCache
        {
            get { return emailCache; }
            set { emailCache = value; }
        }
        private LRUCache<string, string> phoneNumberCache;
        public LRUCache<string, string> PhoneNumberCache
        {
            get { return phoneNumberCache; }
            set { phoneNumberCache = value; }
        }
        public CacheHolderThread()
        {
           
        }
        
        public void StartCaching()
        {
            Users = new LRUCache<string, JObject>(5);  
        }

        public void AddUserWithUseridAsKey(string key,JObject value)
        {
            Users.Add(key,value);
        }
        public void AddUserWithEmailAsKey(string key, string value)
        {
            EmailCache.Add(key,value);
        }
        public void AddUserWithNumberAsKey(string key, string value)
        {
            PhoneNumberCache.Add(key,value);
        }

        public string GetUserIdFromMail(string key)
        {
            string userId = string.Empty;
            EmailCache.TryGetValue(key,out userId);
            return userId;
        }

        public string GetuserIdFromPhoneNumber(string number)
        {
            string userId = string.Empty;
            PhoneNumberCache.TryGetValue(number, out userId);
            return userId;
        }

        public JObject GetUserFriends(string userId)
        {
            JObject details = new JObject();
            users.TryGetValue(userId, out details);
            return details;
        }


    }
}
