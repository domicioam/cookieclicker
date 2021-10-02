package cookieclicker

import akka.actor.{Actor}

class Score extends Actor {
    import Score._
  override def receive: Receive = behaviors(0)

  def behaviors(score: Int): Receive = {
    case Increase => context.become(behaviors(score + 1))
    case Decrease(amount) => 
        if (amount <= score) {
            sender() ! true
            context.become(behaviors(score - amount))
        } else {
            sender() ! false
        }
    case GetScore => sender() ! score
  }
}

object Score {
    case class Increase()
    case class Decrease(amount: Int)
    case class GetScore()
}
