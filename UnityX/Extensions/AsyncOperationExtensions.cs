using System.Threading.Tasks;
using UnityEngine;

namespace UnityX {
    public static class AsyncOperationExtensions {
        /// <summary>
        /// Extension method that converts an AsyncOperation into a Task.
        /// </summary>
        /// <param name="asyncOperation">The AsyncOperation to convert.</param>
        /// <returns>A Task that represents the completion of the AsyncOperation.</returns>
        public static Task AsTask(this AsyncOperation asyncOperation) {
            var taskCompleteSource = new TaskCompletionSource<bool>();
            asyncOperation.completed += _ => taskCompleteSource.SetResult(true);
            return taskCompleteSource.Task;
        }
    }
}
