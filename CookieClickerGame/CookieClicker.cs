using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieClickerGame
{
    public class CookieClicker : ReceiveActor
    {
        public CookieClicker(IActorRef timer, IActorRef cookie)
        {
            timer.Tell(new Timer.Subscribe());
            Receive<Timer.Tick>(HandleTick);
            Cookie = cookie;
        }

        public IActorRef Cookie { get; }

        private void HandleTick(Timer.Tick obj)
        {
            Cookie.Tell(new Cookie.Click());
        }
    }
}
