using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Jcw.Common
{
    public class ParallelProcessor
    {
        public static void ExecuteParallel (params Action[] methods)
        {
            // Initialize the reset events to keep track of completed threads.
            ManualResetEvent[] resetEvents = new ManualResetEvent[methods.Length];

            // Launch each method in it's own thread.
            for (int methodIndex = 0 ; methodIndex < methods.Length ; methodIndex++)
            {
                resetEvents[methodIndex] = new ManualResetEvent (false);

                ThreadPool.QueueUserWorkItem (new WaitCallback ((object index) =>
                {
                    int methodIndexInternal = (int)index;

                    // Execute the method.
                    methods[methodIndexInternal] ();

                    // Tell the calling thread that we're done.
                    resetEvents[methodIndex].Set ();
                }), methodIndex);
            }

            // Wait for all threads to execute.
            WaitHandle.WaitAll (resetEvents);
        }

        #region One Argument Method Parallel Processing

        public static void ExecuteParallel<T> (Action<T>[] methods, T[] methodArguments)
        {
            // Initialize the reset events to keep track of completed threads.
            ManualResetEvent[] resetEvents = new ManualResetEvent[methods.Length];

            // Launch each method in it's own thread.
            for (int i = 0 ; i < methods.Length ; i++)
            {
                resetEvents[i] = new ManualResetEvent (false);

                ThreadPool.QueueUserWorkItem (new WaitCallback ((object index) =>
                {
                    int methodIndex = (int)index;

                    // Execute the method.
                    methods[methodIndex] (methodArguments[methodIndex]);

                    // Tell the calling thread that we're done.
                    resetEvents[methodIndex].Set ();
                }), i);
            }

            // Wait for all threads to execute.
            WaitHandle.WaitAll (resetEvents);
        }

        #endregion

        #region Two Argument Method Parallel Processing

        public static void ExecuteParallel<T1, T2> (Action<T1, T2>[] methods, T1[] methodArguments1, T2[] methodArguments2)
        {
            // Initialize the reset events to keep track of completed threads.
            ManualResetEvent[] resetEvents = new ManualResetEvent[methods.Length];

            // Launch each method in it's own thread.
            for (int i = 0 ; i < methods.Length ; i++)
            {
                resetEvents[i] = new ManualResetEvent (false);

                ThreadPool.QueueUserWorkItem (new WaitCallback ((object index) =>
                {
                    int methodIndex = (int)index;

                    // Execute the method.
                    methods[methodIndex] (methodArguments1[methodIndex], methodArguments2[methodIndex]);

                    // Tell the calling thread that we're done.
                    resetEvents[methodIndex].Set ();
                }), i);
            }

            // Wait for all threads to execute.
            WaitHandle.WaitAll (resetEvents);
        }

        #endregion

        #region Two Argument Method And Return Value Parallel Processing

        public static void ExecuteParallel<T1, T2, TResult> (Func<T1, T2, TResult>[] methods, T1[] methodArguments1, T2[] methodArguments2,
            TResult[] methodReturnValues)
        {
            // Initialize the reset events to keep track of completed threads.
            ManualResetEvent[] resetEvents = new ManualResetEvent[methods.Length];

            // Launch each method in it's own thread.
            for (int i = 0 ; i < methods.Length ; i++)
            {
                resetEvents[i] = new ManualResetEvent (false);

                ThreadPool.QueueUserWorkItem (new WaitCallback ((object index) =>
                {
                    int methodIndex = (int)index;

                    // Execute the method.
                    methodReturnValues[methodIndex] = methods[methodIndex] (methodArguments1[methodIndex], methodArguments2[methodIndex]);

                    // Tell the calling thread that we're done.
                    resetEvents[methodIndex].Set ();
                }), i);
            }

            // Wait for all threads to execute.
            WaitHandle.WaitAll (resetEvents);
        }

        #endregion
    }
}
