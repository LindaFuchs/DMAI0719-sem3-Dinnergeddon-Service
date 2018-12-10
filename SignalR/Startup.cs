using Owin;

namespace SignalR
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // TODO: Add aditional configuration if needed

            app.MapSignalR();
        }
    }
}
