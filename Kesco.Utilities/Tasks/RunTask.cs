using System;
using System.Reflection;

namespace Kesco
{
    /// <summary>
    /// Предоставлет функции-утилиты для выполнения заданий.
    /// </summary>
    public static class Tasks
    {
        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="calculation">The calculation.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static T RunTask<T>(Func<T> calculation, TimeSpan timeout)
            where T : struct
        {
            Exception error = null;
            T result = default(T);

            IAsyncResult operation = null;

            Action runner = () =>
            {
                try
                {
                    result = calculation();
                }
                catch (Exception e) { error = e; }
            };

            // Запускаем операцию асинхронно
            operation = runner.BeginInvoke(null, null);

            // Передаём управление другим потокам 
            // и ожидаем результата выполнения операции 
            System.Threading.Thread.Sleep(0);
            if (!operation.AsyncWaitHandle.WaitOne(timeout, true))
                throw new TimeoutException();

            if (error != null)
                throw new TargetInvocationException(error);

            runner.EndInvoke(operation);

            return result;
        }

    }
}
