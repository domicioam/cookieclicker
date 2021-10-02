using Akka.Actor;

namespace CookieClickerGame
{
    public class Cookie : ReceiveActor
    {
        #region Messages
        public class Click { }
        #endregion
        public IActorRef Score { get; }

        public Cookie(IActorRef score)
        {
            Receive<Click>(HandleClick);
            Score = score;
        }

        private void HandleClick(Click obj)
        {
            Score.Tell(new Score.Increase());
        }
    }
}
