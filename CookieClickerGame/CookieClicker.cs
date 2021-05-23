using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieClickerGame
{
    class CookieClicker : ReceiveActor
    {
        public CookieClicker()
        {
            Receive<Timer.Tick>(HandleTick);
        }

        private void HandleTick(Timer.Tick obj)
        {
            throw new NotImplementedException();
        }
    }
}
