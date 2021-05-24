using Akka.Actor;
using Akka.TestKit.Xunit2;
using Akka.Util;
using CookieClickerGame;
using Xunit;

namespace CookieClickerGameTests
{
    public class ClickerStoreTests : TestKit
    {
        [Fact]
        public void Should_buy_clicker()
        {
            var score = Sys.ActorOf(Props.Create(() => new Score()), "score");
            var timer = Sys.ActorOf(Props.Create(() => new Timer()), "timer");
            var cookie = Sys.ActorOf(Props.Create(() => new Cookie(score)), "cookie");
            var cookieStore = Sys.ActorOf(Props.Create(() => new ClickerStore(timer, cookie)), "cookieStore");
         
            score.Tell(new Score.Increase());
            score.Tell(new Score.Increase());
            score.Tell(new Score.Increase());
         
            var buyClicker = new ClickerStore.BuyClicker(score);
            cookieStore.Tell(buyClicker);
            ExpectMsg<Result<IActorRef>>();

            score.Tell(new Score.GetScore());
            var result = ExpectMsg<Result<int>>();
            Assert.Equal(0, result.Value);
        }
    }
}
