package cookieclicker

import akka.actor.{Actor, ActorLogging, Props}
import akka.actor.ActorRef
import akka.pattern.{ask, pipe}
import akka.util.Timeout
import scala.concurrent.duration._
import scala.concurrent.ExecutionContext.Implicits.global

class ClickerStore(timer: ActorRef, cookie: ActorRef)
    extends Actor
    with ActorLogging {
  import ClickerStore._
  import Score._

  val CLICKER_PRICE = 3
  implicit val timeout = Timeout(5.seconds)

  override def receive: Receive = {
    case buy: BuyClicker =>
      val future = buy.score ? Decrease(CLICKER_PRICE)
      future.pipeTo(self)
    case true =>
      sender() ! context.system.actorOf(Props(CookieClicker(timer, cookie)))
    case false => sender() ! "Not enough points!"
  }
}

object ClickerStore {
  case class BuyClicker(score: ActorRef)
}
