using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieClickerGame
{
    class Timer : ReceiveActor
    {
        #region Messages
        public class Start { }
        public class Tick { }
        #endregion
        public Timer()
        {
            Receive<Start>(HandleStart);
        }

        private void HandleStart(Start obj)
        {
            throw new NotImplementedException();
        }
    }
}
