package cookieclicker

import org.scalatest.wordspec.AnyWordSpecLike
import akka.testkit.TestKit
import akka.actor.ActorSystem
import akka.testkit.TestProbe
import akka.actor.Props
import Cookie._

class CookieSpec
    extends TestKit(ActorSystem("CookieSpec"))
    with AnyWordSpecLike {

  "A cookie" must {
    "increase score" in {
      val score = TestProbe()
      val cookie = system.actorOf(Props(new Cookie(score.ref)))
      cookie ! Click
      score.expectMsg(Score.Increase)
    }
  }
}
