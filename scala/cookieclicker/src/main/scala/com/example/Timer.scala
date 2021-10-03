package cookieclicker

import akka.actor.{Actor, ActorRef}
import scala.concurrent.ExecutionContext.Implicits.global
import scala.concurrent.duration._
import akka.actor.Cancellable
import akka.actor.ActorLogging

class Timer extends Actor with ActorLogging {
  import Timer._
  override def receive: Receive = waiting(Seq.empty)

  def waiting(subscribers: Seq[ActorRef]): Receive = {
    case Start(interval) =>
      log.info(s"Start received with interval: $interval.")
      val cancellable =
        context.system.scheduler
          .scheduleWithFixedDelay(1.second, interval, self, Tick)
      context.become(started(subscribers, cancellable))
    case Subscribe => 
      log.info(s"Subscribe received for actor ${sender()}")
      context.become(waiting(subscribers :+ sender()))
  }

  def started(subscribers: Seq[ActorRef], cancellable: Cancellable): Receive = {
    case Subscribe =>
      log.info(s"Subscribe received for actor ${sender()}")
      context.become(started(subscribers :+ sender(), cancellable))
    case Tick => 
      log.info("Tick received.")
      subscribers.foreach(s => s ! Tick)
    case Stop => 
        cancellable.cancel()
        context.stop(self)
  }
}

object Timer {
  case class Start(interval: FiniteDuration)
  case object Stop
  case object Tick
  case object Subscribe
}
