using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Caching;
using Newtonsoft.Json.Linq;

namespace CacheService
{
    public class CachingClass
    {
        private CacheHolderThread cacheHolderThread;
        public CachingClass()
        {
            cacheHolderThread = new CacheHolderThread();
            Thread t = new Thread(new ThreadStart(cacheHolderThread.StartCaching));
            
        }

    }
}
