using Akka.Actor;
using System;
using System.Collections.Generic;

namespace CookieClickerGame
{
    public class Timer : ReceiveActor
    {
        #region Messages
        public record Start(TimeSpan Interval);
        public class Stop { }
        public class Tick { }
        public class Subscribe { }
        #endregion

        private ICancelable cancelable;
        protected HashSet<IActorRef> Subscribers;

        public Timer()
        {
            Subscribers = new HashSet<IActorRef>();
            Receive<Start>(HandleStart);
        }

        private void Started()
        {
            Receive<Subscribe>(HandleSubscribe);
            Receive<Stop>(HandleStop);
            Receive<Tick>(HandleTick);
        }

        private void HandleSubscribe(Subscribe obj)
        {
            Subscribers.Add(Sender);
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
            foreach (var sub in Subscribers)
                sub.Tell(msg);
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
