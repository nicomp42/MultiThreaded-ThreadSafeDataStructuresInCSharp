/*
 * Bill Nicholson
 * nicholdw@ucmail.uc.edu
 */
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreaded_ThreadSafeDataStructuresInCSharp
{
    class MainForDemo
    {
        /// <summary>
        /// Demo for the ThreadDemo class
        /// </summary>
        /// <param name="args">None</param>
        static void Main(string[] args)
        {
            ExecuteIntegerDemo();
            //ExecuteListDemo();

        }
        private static void ExecuteIntegerDemo() {
            // Spawn three threads. Each one *should* increment the counter 1000 times so we should end up with 3000, right?
            ThreadIntegerDemo demo1 = new ThreadIntegerDemo();
            Thread bob = demo1.Demo("Bob the Thread");
            ThreadIntegerDemo demo2 = new ThreadIntegerDemo();
            Thread carol = demo2.Demo("Carol the Thread");
            ThreadIntegerDemo demo3 = new ThreadIntegerDemo();
            Thread alice = demo3.Demo("Alice the Thread");
            // Now we patiently wait for the threads to finish
            bob.Join();
            carol.Join();
            alice.Join();
            Console.WriteLine("count is now " + demo3.GetCount());
        }
        private static void ExecuteListDemo()
        {
            // Spawn three threads. Each one *should* add 1000 items to the list so we should end up with 3000, right?
            ThreadListDemo demo1 = new ThreadListDemo();
            Thread bob = demo1.Demo("Bob the Thread");
            ThreadListDemo demo2 = new ThreadListDemo();
            Thread carol = demo2.Demo("Carol the Thread");
            ThreadListDemo demo3 = new ThreadListDemo();
            Thread alice = demo3.Demo("Alice the Thread");
            // Now we patiently wait for the threads to finish
            bob.Join();
            carol.Join();
            alice.Join();
            Console.WriteLine("List count is now " + ThreadListDemo.GetListCount());// Should be 3000
        }

    }
}