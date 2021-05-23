using Akka.Actor;
using Akka.TestKit.Xunit2;
using CookieClickerGame;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CookieClickerGameTests
{
    public class TimerTests : TestKit
    {
        [Fact]
        public void Should_start_timer()
        {
            var timer = Sys.ActorOf(Props.Create(() => new Timer()), "timer");
            var start = new Timer.Start(TimeSpan.FromSeconds(5));

        }
    }
}
