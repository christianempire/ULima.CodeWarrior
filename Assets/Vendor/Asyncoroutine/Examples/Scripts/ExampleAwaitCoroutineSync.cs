using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Asyncoroutine.Example
{
    public class ExampleAwaitCoroutineSync : MonoBehaviour
    {
        private async void Awake()
        {
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            // It's not guaranteed that it will be completed on main thread because of 'ConfigureAwait(false)'
            await Task.Delay(1000).ConfigureAwait(false); 
            Debug.Log("Delay ConfigureAwait(false)");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            await new WaitForMainThread();
            UnityWebRequest unityWebRequest = UnityWebRequest.Get("https://api.ipify.org?format=json");
            await unityWebRequest.SendWebRequest();
            Debug.Log(unityWebRequest.downloadHandler.text);
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}