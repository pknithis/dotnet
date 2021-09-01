using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class LogicalCallContextAsyncLocal<T>
    {
        private readonly string logicalDataName;
        private readonly AsyncLocal<Holder> asyncLocal;

        public LogicalCallContextAsyncLocal(string logicalDataName)
        {
            //Console.WriteLine("In the constructor LogicalCallContextAsyncLocal");
            this.logicalDataName = logicalDataName;
            this.asyncLocal = new AsyncLocal<Holder>();
        }

        public void Set(T value)
        {
            //Console.WriteLine("Set = LogicalCallContextAsyncLocal");
            if (this.asyncLocal.Value == null)
            {
                this.asyncLocal.Value = new Holder { Id = default(T) };
            }

            this.asyncLocal.Value.Id = value;
        }

        public T Get()
        {
            //Console.WriteLine("Get = LogicalCallContextAsyncLocal");
            return this.asyncLocal.Value.Id;
        }

        public void Reset()
        {
            this.asyncLocal.Value = new Holder { Id = default(T) };
        }

        protected class Holder : MarshalByRefObject
        {
            public virtual T Id { get; set; }
        }
    }
}
