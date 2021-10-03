package cookieclicker

import akka.actor.{Actor, ActorRef}
import akka.actor.ActorLogging
import cookieclicker.Timer._
import cookieclicker.Cookie
import cookieclicker.Cookie.Click

class CookieClicker(timer: ActorRef, cookie: ActorRef)
    extends Actor
    with ActorLogging {
  override def receive: Receive = { case Tick =>
    log.info("Tick received. Sending click to cookie.")
    cookie ! Click
  }

  override def preStart(): Unit = {
    timer ! Subscribe
    super.preStart()
  }
}

object CookieClicker {
  def apply(timer: ActorRef, cookie: ActorRef): CookieClicker = {
    new CookieClicker(timer, cookie)
  }
}
