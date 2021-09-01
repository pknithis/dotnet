using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class CallContextsAsyncLocal
    {
        private static readonly Lazy<LogicalCallContextAsyncLocal<ThreadView>> ThreadViewCallContext =
        new Lazy<LogicalCallContextAsyncLocal<ThreadView>>(() => new LogicalCallContextAsyncLocal<ThreadView>("ThreadView"), isThreadSafe: true);

        public static ThreadView ThreadView
        {
            get
            {
               // Console.WriteLine("Before calling get");
                var res = CallContextsAsyncLocal.ThreadViewCallContext.Value.Get();
               // Console.WriteLine("After calling get");
                return res;
            }
            set
            {
                // Console.WriteLine("Before calling set");
                CallContextsAsyncLocal.ThreadViewCallContext.Value.Set(value);
                // Console.WriteLine("After calling set");
            }
        }
    }
}
