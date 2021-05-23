using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieClickerGame
{
    public class Timer : ReceiveActor
    {
        #region Messages
        public class Start
        {
            public Start(TimeSpan interval)
            {
                Interval = interval;
            }

            public TimeSpan Interval { get; }
        }
        public class Stop { }
        public class Tick { }
        #endregion

        private ICancelable cancelable;

        public Timer()
        {
            Receive<Start>(HandleStart);
        }

        private void Started()
        {
            Receive<Stop>(HandleStop);
            Receive<Tick>(HandleTick);
        }

        private void HandleStart(Start msg)
        {
            cancelable = Context
                            .System
                            .Scheduler
                            .ScheduleTellRepeatedlyCancelable(
                                msg.Interval,
                                msg.Interval,
                                Self,
                                new Tick(),
                                ActorRefs.NoSender);

            BecomeStacked(Started);
        }

        private void HandleTick(Tick msg)
        {
            // publish tick to all listeners
            throw new NotImplementedException();
        }

        private void HandleStop(Stop obj)
        {
            cancelable?.Cancel();
            UnbecomeStacked();
        }

        protected override void PostStop()
        {
            base.PostStop();
            cancelable?.Cancel();
        }
    }
}
