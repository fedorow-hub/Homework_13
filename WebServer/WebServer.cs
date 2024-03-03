using System;
using System.Net;
using System.Threading.Tasks;

namespace WebServer
{
    public class WebServer
    {
        public event EventHandler<RequestReceiverEventArgs> RequestReceived;

        private HttpListener _listener;
        private readonly int _port;
        private bool _enabled;
        private readonly object _syncRoot = new object();
        
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (value) Start();
                else Stop();
            }
        }
        public int Port => _port;

        public WebServer(int Port) => _port = Port;

        public void Start()
        {
            if(_enabled) return;
            lock (_syncRoot)
            {
                if (_enabled) return;
                _listener = new HttpListener();
                _listener.Prefixes.Add($"http://*:{_port}/");// netsh http add urlacl url=http://*:8080/ user=Sergei1 (это надо выполнить в консоли, запущенной с правами администратора)
                _enabled = true;
            }
            ListenAsync();
        }

        public void Stop()
        {
            if (!_enabled) return;
            lock (_syncRoot)
            {
                if (!_enabled) return;
                _listener = null;
                _enabled = false;
            }
        }

        private async void ListenAsync()
        {
            var listener = _listener;
            listener.Start();

            HttpListenerContext context = null;
            while (_enabled)
            {
                var getContextTask = listener.GetContextAsync();
                if (context != null)
                    ProcessRequestAsync(context);
                
                context = await getContextTask.ConfigureAwait(false);
            }
            
            listener.Stop();
        }

        private async void ProcessRequestAsync(HttpListenerContext context)
        {
            await Task.Run(() => RequestReceived.Invoke(this, new RequestReceiverEventArgs(context)));
        }
    }

    public class RequestReceiverEventArgs : EventArgs
    {
        public HttpListenerContext Context { get; }

        public RequestReceiverEventArgs(HttpListenerContext context)
        {
            Context = context;
        }
    }
}
