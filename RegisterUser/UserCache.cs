using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using ZeroMQ;
using Tools;


namespace UpdateUser
{
    internal class UserCache
    {
        private ZmqContext ctx = ZmqContext.Create();
        private ZmqSocket receiverSocket;
        static ConcurrentQueue<String> Updates = new ConcurrentQueue<string>();
        StringBuilder messages = new StringBuilder();
        private ZmqSocket publisherSocket;
        private ConcurrentDictionary<String, List<String>> userUpdatesHash;
        UpdatesHash hashClass;
        private Thread msgInterceptorThread;
        private Thread updateThread;
        
      
        private ZeroMQ.ZmqContext Ctx
        {
            get { return ctx; }
            set { ctx = value; }
        }
       internal UserCache()
       {
           hashClass = new UpdatesHash(ref Updates);
           receiverSocket = ctx.CreateSocket(SocketType.SUB);
           publisherSocket = ctx.CreateSocket(SocketType.PUB);
           msgInterceptorThread = new Thread(new ThreadStart(hashClass.Start));
           updateThread = new Thread(new ThreadStart(hashClass.updateChanges));

       }
        internal void Init()
        {
            receiverSocket.Connect("tcp://127.0.0.1:9001");
            const string receiverFilter = "update";
            byte[] receiverFilterBytes = ToolsCls.GetBytes(receiverFilter);
            receiverSocket.Subscribe(receiverFilterBytes);
        }
        internal void Start()
        {
            Init();
            msgInterceptorThread.Start();
            while (true)
                if (receiverSocket != null)
                    if (UserCache.Updates != null) UserCache.Updates.Enqueue(receiverSocket.Receive(Encoding.ASCII));
        }
        internal void update()
        {
            
        }

    }
}
