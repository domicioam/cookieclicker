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
    case class Click()
}
