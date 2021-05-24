using Akka.Actor;
using Akka.TestKit.Xunit2;
using CookieClickerGame;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CookieClickerGameTests
{
    public class CookieClickerTests : TestKit
    {
        [Fact]
        public void Should_send_click_to_cookie()
        {
            var timerProps = CreateTestProbe();
            var cookieProps = CreateTestProbe();
            var cookieClicker = Sys.ActorOf(Props.Create(() => new CookieClicker(timerProps, cookieProps)), "cookie");
            
            timerProps.ExpectMsg<Timer.Subscribe>();
            cookieClicker.Tell(new Timer.Tick());
            cookieProps.ExpectMsg<Cookie.Click>();
        }
    }
}
