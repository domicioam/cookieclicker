package cookieclicker

import org.scalatest.wordspec.AnyWordSpecLike
import akka.testkit.{TestKit, TestProbe, ImplicitSender}
import akka.actor.{ActorSystem, Props, ActorRef}
import cookieclicker.Score._
import ClickerStore._
import scala.concurrent.duration._

class ClickerStoreSpec
    extends TestKit(ActorSystem("ClickerStoreSpec"))
    with AnyWordSpecLike
    with ImplicitSender {
  "A cookie store" should {
    "sell cookie clicker" in {
        val score = system.actorOf(Props[Score])
        val timer = system.actorOf(Props[Timer])
        val cookie = system.actorOf(Props(Cookie(score)))
        val cookieStore = system.actorOf(Props(ClickerStore(timer, score)))

        score ! Increase
        score ! Increase
        score ! Increase

        cookieStore ! BuyClicker(score)
        expectMsgType[ActorRef]

        score ! GetScore
        val result = expectMsgType[Int]
        assert(result == 0)
    }
  }
}
