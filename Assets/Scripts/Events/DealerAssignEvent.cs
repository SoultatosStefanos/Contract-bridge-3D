using Makaretu.Bridge;

namespace Events
{
    public class DealerAssignEvent
    {
        public DealerAssignEvent(Seat seat)
        {
            Seat = seat;
        }

        public Seat Seat { get; }

        public override string ToString()
        {
            return $"Events.DealerAssignEvent (Dealer: {Seat})";
        }
    }
}