using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class CallContexts
    {
        private static readonly Lazy<LogicalCallContext<ThreadView>> ThreadViewCallContext =
        new Lazy<LogicalCallContext<ThreadView>>(() => new LogicalCallContext<ThreadView>("ThreadView"), isThreadSafe: true);

        public static ThreadView ThreadView
        {
            get
            {
                return CallContexts.ThreadViewCallContext.Value.Get();
            }
            set
            {
                CallContexts.ThreadViewCallContext.Value.Set(value);
            }
        }
    }

    public class ThreadView
    {
        public string name;
        int id;

        public ThreadView(string s, int i)
        {
            this.name = s;
            this.id = i;
        }
    }
}
