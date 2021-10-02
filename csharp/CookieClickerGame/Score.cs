using Akka.Actor;
using Akka.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieClickerGame
{
    public class Score : ReceiveActor
    {
        #region
        public class Increase { }
        public class Decrease
        {
            public int Amount { get; set; }

            public Decrease(int amount)
            {
                Amount = amount;
            }
        }
        public class GetScore
        {
        }
        #endregion

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
