/*
 * Bill Nicholson
 * nicholdw@ucmail.uc.edu
 */
using System;
using System.Threading;

namespace MultiThreaded_ThreadSafeDataStructuresInCSharp
{
    /// <summary>
    /// The count variable will be hammered by each instance of this thread and it will eventually get corrupted.
    /// Then we fix the problem by deploying the Interlocked.Increment() method, yay!
    /// </summary>
    class ThreadIntegerDemo
    {
        // private non-static class members. We will not be sharing them between threads
        private String threadName;
        private Thread thread;
        /*
        The count variable will be hammered by each instance of this thread and it will get corrupted.
        */
        private static int count = 0; // a static class member that we will be sharing between threads
        public int GetCount() { return count; }
        /// <summary>
        /// Set up a thread that we can spawn
        /// </summary>
        /// <param name="threadName">The friendly name of the thread</param>
        /// <returns></returns>
        public Thread Demo(String threadName)
        {
            this.threadName = threadName;
            thread = new Thread(this.ThreadStart);
            thread.Start();
            return thread;
        }
        /// <summary>
        /// What happens in the thread
        /// </summary>
        private void ThreadStart()
        {
            Console.WriteLine(threadName + " is starting: count = " + count + " I am " + Thread.CurrentThread.ThreadState);
            Thread.Sleep(50);
            for (int i = 0; i < 1000; i++) {
                count++;    // looks harmless? Not in a multi-threaded environment!
                // The .Net API comes to the rescue!
                // https://docs.microsoft.com/en-us/dotnet/api/system.threading.interlocked.increment?redirectedfrom=MSDN&view=netframework-4.8#System_Threading_Interlocked_Increment_System_Int32__
                // Interlocked.Increment(ref count);   // Pass an int by reference
            }
            Console.WriteLine(threadName + " is done: count = " + count + " I am " + Thread.CurrentThread.ThreadState);
        }
    }
}