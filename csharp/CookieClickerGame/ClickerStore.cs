using Akka.Actor;
using Akka.Util;

namespace CookieClickerGame
{
    public class ClickerStore : ReceiveActor
    {
        #region Messages
        public record BuyClicker(IActorRef score);
        #endregion

        private const int CLICKER_PRICE = 3;
        IActorRef sender;

        public ClickerStore(IActorRef timer, IActorRef cookie)
        {
            Timer = timer;
            Cookie = cookie;

            Receive<BuyClicker>(msg => {
                this.sender = Sender;
                msg.score.Ask<Result<bool>>(new Score.Decrease(CLICKER_PRICE)).PipeTo(Self);
            });
            
            Receive<Result<bool>>(msg => msg.IsSuccess, _ => {
                var clicker = Context.System.ActorOf(Props.Create(() => new CookieClicker(Timer, Cookie)));
                this.sender.Tell(new Result<IActorRef>(clicker));
            });
            
            Receive<Result<bool>>(_ => this.sender.Tell(new Result<string>("Not enough points!")));
        }

        public IActorRef Timer { get; }
        public IActorRef Cookie { get; }
    }
}
