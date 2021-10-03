package cookieclicker

import org.scalatest.wordspec.AnyWordSpecLike
import akka.testkit.TestKit
import akka.actor.ActorSystem
import akka.testkit.TestProbe
import akka.actor.Props
import akka.testkit.ImplicitSender
import scala.concurrent.duration._
import Timer._

class TimerSpec
    extends TestKit(ActorSystem("TimerSpec"))
    with AnyWordSpecLike
    with ImplicitSender {

  "A timer" should {
    "start ticking" in {
        val timer = system.actorOf(Props[Timer])
        timer ! Start(1.seconds)
        timer ! Subscribe
        expectMsg(Tick)
    }

    "stop ticking" in {
        val timer = system.actorOf(Props[Timer])
        timer ! Start(1.seconds)
        timer ! Subscribe
        expectMsg(Tick)
        timer ! Stop
        expectNoMessage()
    }
  }
}
