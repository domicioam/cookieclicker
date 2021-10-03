package cookieclicker

import akka.actor.{Actor, ActorRef}
import Score._

class Cookie(score: ActorRef) extends Actor {
    import Cookie._
    override def receive: Receive = {
        case Click => score ! Increase
    }
}

object Cookie {
    def apply(score: ActorRef): Cookie = {
        new Cookie(score)
    }
    case class Click()
}
