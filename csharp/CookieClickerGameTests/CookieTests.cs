using Akka.Actor;
using Akka.TestKit.Xunit2;
using CookieClickerGame;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CookieClickerGameTests
{
    public class CookieTests : TestKit
    {
        [Fact]
        public void Should_send_increase_score()
        {
            // create cookie
            var score = CreateTestProbe();
            var cookie = Sys.ActorOf(Props.Create(() => new Cookie(score)), "cookie");

            // send click
            var click = new Cookie.Click();
            cookie.Tell(click);
            
            // check increase score
            score.ExpectMsg<Score.Increase>();
        }
    }
}
