/*
 * Bill Nicholson
 * nicholdw@ucmail.uc.edu
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreaded_ThreadSafeDataStructuresInCSharp
{
    /// <summary>
    /// The List object will be hammered by each instance of this thread and it will eventually get corrupted.
    /// Then we fix the problem by deploying a lock on the code that access the List object, yay!
    /// </summary>
    class ThreadListDemo
    {
        private static Object myLock = new object();    // Will be a nice lock to share for all instances of this class.

        // private non-static class members. We will not be sharing them between threads
        private String threadName;
        private Thread thread;
        /*
        The count variable will be hammered by each instance of this thread and it will get corrupted.
        */
        private static List<int> myList = new List<int>(); // a static class member that we will be sharing between threads
        public static int GetListCount() { return myList.Count; }
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
            Console.WriteLine(threadName + " is starting: count = " + GetListCount() + " I am " + Thread.CurrentThread.ThreadState);
            Thread.Sleep(50);
            for (int i = 0; i < 1000; i++) {
                myList.Add(1);    // looks harmless? Not in a multi-threaded environment!

                // Let's get a lock before using it
/*                lock(myLock) {
                    myList.Add(1);
                } */
            }
            Console.WriteLine(threadName + " is done: List count = " + GetListCount() + " I am " + Thread.CurrentThread.ThreadState);
        }
    }
}