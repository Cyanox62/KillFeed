using Exiled.API.Features;

namespace KillFeed
{
    public class KillFeed : Plugin<Config>
    {
        private EventHandlers ev;
        internal static KillFeed instance;

        public override void OnEnabled()
        {
            base.OnEnabled();

            instance = this;
            ev = new EventHandlers();

            Exiled.Events.Handlers.Server.SendingConsoleCommand += ev.OnConsoleCommand;
            Exiled.Events.Handlers.Player.Died += ev.OnPlayerDeath;
            Exiled.Events.Handlers.Server.RestartingRound += ev.OnRoundRestart;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            Exiled.Events.Handlers.Server.SendingConsoleCommand -= ev.OnConsoleCommand;
            Exiled.Events.Handlers.Player.Died -= ev.OnPlayerDeath;
            Exiled.Events.Handlers.Server.RestartingRound -= ev.OnRoundRestart;

            ev = null;
        }

        public override string Author => "Cyanox";
    }
}
