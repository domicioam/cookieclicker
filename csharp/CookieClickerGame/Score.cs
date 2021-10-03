using Akka.Actor;
using Akka.Util;

namespace CookieClickerGame
{
    public class Score : ReceiveActor
    {
        #region Messages
        public record Increase;
        public record Decrease(int Amount);
        public record GetScore;
        #endregion Messages

        private int score;

        public Score()
        {
            Receive<Increase>(_ => score++);
            Receive<Decrease>(obj =>
            {
                if (obj.Amount <= score)
                {
                    score -= obj.Amount;
                    Sender.Tell(new Result<bool>(true));
                }
                else
                {
                    Sender.Tell(new Result<bool>(false));
                }
            });
            Receive<GetScore>(_ => Sender.Tell(new Result<int>(score)));
        }
    }
}
