using Akka.Actor;
using Akka.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookieClickerGame
{
    public class ClickerStore : ReceiveActor
    {
        private const int CLICKER_PRICE = 3;
        #region Messages
        public class BuyClicker
        {
            public IActorRef score { get; }
            public BuyClicker(IActorRef score)
            {
                this.score = score;
            }
        }

        #endregion
        public ClickerStore()
        {
            ReceiveAsync<BuyClicker>(HandleBuyClickerAsync);
        }

        private async Task HandleBuyClickerAsync(BuyClicker msg)
        {
            var result = await msg.score.Ask<Result<bool>>(new Score.Decrease(CLICKER_PRICE));

            if (result.IsSuccess)
            {
                var clicker = Context.System.ActorOf(Props.Create(() => new CookieClicker()));
                Sender.Tell(new Result<IActorRef>(clicker));
            }
            else
            {
                Sender.Tell(new Result<string>("Not enough points!"));
            }
        }
    }
}
