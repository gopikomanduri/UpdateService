using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace UpdateUser
{
    internal class LruCache
    {
        private LinkedList<string> lruUsersList;
        public LinkedList<string> LruUsersList
        {
            get { return lruUsersList; }
            set { lruUsersList = value; }
        }
        private Dictionary<string, LinkedListNode<string>>  lruUserIndex;
        public Dictionary<string, LinkedListNode<string>> LruUserIndex
        {
            get { return lruUserIndex; }
            set { lruUserIndex = value; }
        }
        private LinkedList<string> currentloggedInUsersList;
        public LinkedList<string> CurrentloggedInUsersList
        {
            get { return currentloggedInUsersList; }
            set { currentloggedInUsersList = value; }
        }
        private Dictionary<string, LinkedListNode<string>>  currentLoggedInUserIndex;
        public Dictionary<string, LinkedListNode<string>> CurrentLoggedInUserIndex
        {
            get { return currentLoggedInUserIndex; }
            set { currentLoggedInUserIndex = value; }
        }
        private Thread th;

        public LruCache()
        {
            LruUsersList = new LinkedList<string>();
            LruUserIndex = new Dictionary<string, LinkedListNode<string>>();
        }

        internal void start()
        {
            th = new Thread(new ThreadStart(launchThread));
        }

        private void launchThread()
        {
            
        }

        internal bool IsUserExist(String userId)
        {
            if (userId == null) throw new ArgumentNullException("userId");
            var isExist = false;
            if (userId == null) throw new ArgumentNullException("userId");
            if (currentLoggedInUserIndex != null)
                isExist = currentLoggedInUserIndex.ContainsKey(userId);
            return isExist;
        }


        internal bool InsertIntoLoggedInUserList(String userId)
        {
            LinkedListNode<String> nextUserNode = null;
            if (IsUserExist(userId) == false)
            {
                //check with db , about user validation.
                LinkedListNode<String> user = new LinkedListNode<String>(userId);
                if (LruUsersList != null)
                {
                  nextUserNode   = LruUsersList.First;
                }
                if (LruUsersList != null) LruUsersList.AddFirst(user);
                LruUserIndex.Add(userId,null);
                LruUserIndex.TryGetValue()
            }
            return false;

        }
    }
}

