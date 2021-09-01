using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class LogicalCallContext<T>
    {
        private readonly string logicalDataName;

        public LogicalCallContext(string logicalDataName)
        {
            this.logicalDataName = logicalDataName;
        }

        public void Set(T value)
        {
            var holder = CallContext.LogicalGetData(logicalDataName) as Holder;
            if (holder == null)
            {
                CallContext.LogicalSetData(logicalDataName, new Holder { Id = value });
            }
            else
            {
                try
                {
                    holder.Id = value;
                }
                catch (AppDomainUnloadedException)
                {
                    CallContext.LogicalSetData(logicalDataName, new Holder { Id = value });
                }
            }
        }

        public T Get()
        {
            var holder = CallContext.LogicalGetData(logicalDataName) as Holder;
            if (holder != null)
            {
                try
                {
                    return holder.Id;
                }
                catch (AppDomainUnloadedException)
                {
                    CallContext.FreeNamedDataSlot(logicalDataName);
                    return default(T);
                }
            }

            return default(T);
        }

        public void Reset()
        {
            CallContext.FreeNamedDataSlot(logicalDataName);
        }

        protected class Holder : MarshalByRefObject
        {
            public virtual T Id { get; set; }
        }
    }
}
