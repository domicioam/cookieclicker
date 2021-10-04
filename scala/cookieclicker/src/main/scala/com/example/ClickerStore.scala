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

  override def receive: Receive = { case buy: BuyClicker =>
    log.info(s"Received message to buy clicker: $buy")
    val future = buy.score ? Decrease(CLICKER_PRICE)
    context.become(piped(sender()))
    future.pipeTo(self)
  }

  def piped(sender: ActorRef): Receive = {
    case true =>
        log.info("Clicker bought with success!")
      sender ! context.system.actorOf(Props(CookieClicker(timer, cookie)))
    case false => 
        log.info("Unable to buy clicker!")
        sender ! "Not enough points!"
  }
}

object ClickerStore {
  def apply(timer: ActorRef, cookie: ActorRef): ClickerStore = {
    new ClickerStore(timer, cookie)
  }
  case class BuyClicker(score: ActorRef)
}
