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
            Receive<Increase>(HandleIncrease);
            Receive<Decrease>(HandleDecrease);
            Receive<GetScore>(HandleGetScore);
        }

        private void HandleGetScore(GetScore obj)
        {
            Sender.Tell(new Result<int>(score));
        }

        private void HandleDecrease(Decrease obj)
        {
            if(obj.Amount <= score)
            {
                score -= obj.Amount;
                Sender.Tell(new Result<bool>(true));
            }

            Sender.Tell(new Result<bool>(false));
        }

        private void HandleIncrease(Increase increase)
        {
            score++;
        }
    }
}
