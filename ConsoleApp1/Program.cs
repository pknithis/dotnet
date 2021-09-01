using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static AsyncLocal<string> _asyncLocalString = new AsyncLocal<string>();

        static async Task AsyncMethodA(int id)
        {
            // Start multiple async method calls, with different AsyncLocal values.
            // We also set ThreadLocal values, to demonstrate how the two mechanisms differ.

            //CallContext.LogicalSetData("data1", "CallContext-A");

            CallContexts.ThreadView = null;
            CallContextsAsyncLocal.ThreadView = null;



            Console.WriteLine(id+" A:AsyncLocal Before: " + _asyncLocalString.Value);
            Console.WriteLine(id + " A:LogicalGCallContextetData Before: " + CallContext.LogicalGetData("data1"));
            Console.WriteLine(id + " A:ThreadView with CallContext Before: " + CallContexts.ThreadView?.name);
            Console.WriteLine(id + " A:ThreadView with AsyncLocal+Holder Before: " + CallContextsAsyncLocal.ThreadView?.name);
            
            Console.WriteLine();
            var t1 = AsyncMethodB(id);
            await t1;

            Console.WriteLine(id + " A:AsyncLocal After: " + _asyncLocalString.Value);
            Console.WriteLine(id + " A:CallContext After: " + CallContext.LogicalGetData("data1"));
            Console.WriteLine(id + " A:ThreadView with CallContext After: " + CallContexts.ThreadView?.name);
            Console.WriteLine(id + " A:ThreadView with AsyncLocal+Holder After: " + CallContextsAsyncLocal.ThreadView?.name);
            Console.WriteLine();
            Console.ReadLine();

        }

        static async Task AsyncMethodB(int id)
        {
            Console.WriteLine(id + " B:AsyncLocal Before: " + _asyncLocalString.Value);
            Console.WriteLine(id + " B:CallContext Before: " + CallContext.LogicalGetData("data1"));
            Console.WriteLine(id + " B:ThreadView with CallContext Before : " + CallContexts.ThreadView?.name);
            Console.WriteLine(id + " B:ThreadView with AsyncLocal+Holder Before: " + CallContextsAsyncLocal.ThreadView?.name);
            Console.WriteLine();

            CallContext.LogicalSetData("data1", "Value@B:"+id);
            _asyncLocalString.Value = "Value@B:" + id;
            CallContexts.ThreadView = new ThreadView("Value@B:" + id, 1);
            CallContextsAsyncLocal.ThreadView = new ThreadView("Value@B:" + id, 1);


            await AsyncMethodC(id);

            Console.WriteLine(id + "B:AsyncLocal After: " + _asyncLocalString.Value);
            Console.WriteLine(id + "B:CallContext After: " + CallContext.LogicalGetData("data1"));
            Console.WriteLine(id + "B:ThreadView with CallContext After: " + CallContexts.ThreadView.name);
            Console.WriteLine(id + "B:ThreadView with AsyncLocal+Holder After: " + CallContextsAsyncLocal.ThreadView?.name);
            Console.WriteLine();
            await Task.Delay(100);
        }


        static async Task AsyncMethodC(int id)
        {
            Console.WriteLine(id + "C:AsyncLocal Before: " + _asyncLocalString.Value);
            Console.WriteLine(id + "C:LogicalGetData Before: " + CallContext.LogicalGetData("data1"));
            Console.WriteLine(id + "C:ThreadView with CallContext Before: " + CallContexts.ThreadView.name);
            Console.WriteLine(id + "C:ThreadView with AsyncLocal+Holder Before: " + CallContextsAsyncLocal.ThreadView?.name);
            Console.WriteLine();

            CallContext.LogicalSetData("data1", "Value@C:" + id);
            _asyncLocalString.Value = "Value@C:" + id;
            CallContexts.ThreadView = new ThreadView("Value@C:" + id, 2);
            CallContextsAsyncLocal.ThreadView = new ThreadView("Value@C:" + id, 2);

            Console.WriteLine(id + "C:AsyncLocal After: " + _asyncLocalString.Value);
            Console.WriteLine(id + "C:CallContext After: " + CallContext.LogicalGetData("data1"));
            Console.WriteLine(id + "C:ThreadView with CallContext After: " + CallContexts.ThreadView.name);
            Console.WriteLine(id + "C:ThreadView with AsyncLocal+Holder After: " + CallContextsAsyncLocal.ThreadView?.name);
            Console.WriteLine();
            await Task.Delay(100);
        }

        static async Task Main(string[] args)
        {
            Task t1 = AsyncMethodA(10);
            Task t2 = AsyncMethodA(20);

            Task.WaitAll(t1,t2);
            
        }
    }
}
