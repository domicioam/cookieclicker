import org.scalatest.wordspec.AnyWordSpecLike
import akka.testkit.TestKit
import akka.actor.ActorSystem
import akka.testkit.TestProbe
import akka.actor.Props
import akka.testkit.ImplicitSender
import scala.concurrent.duration._
import cookieclicker.Score
import cookieclicker.Timer
import cookieclicker.Cookie
import cookieclicker.CookieClicker
import cookieclicker.Timer._
import cookieclicker.Score._

class CookieClickerSpec
    extends TestKit(ActorSystem("TimerSpec"))
    with AnyWordSpecLike
    with ImplicitSender {
  
   "A cookie clicker" should {
     "click cookie automatically" in {
       val score = TestProbe()
       val timer = system.actorOf(Props[Timer])
       val cookie = system.actorOf(Props(Cookie(score.ref)))
       val cookieClicker = system.actorOf(Props(CookieClicker(timer, cookie)))

      within(2500.millis) {
        timer ! Start(1.second)
        score.receiveN(2, 2500.millis)
        timer ! Stop
        score.expectNoMessage()
      }
     }
   }
}
