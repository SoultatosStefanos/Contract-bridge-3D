using Makaretu.Bridge;

namespace Events
{
    public class DealerAssignEvent
    {
        private readonly Seat _seat;

        public DealerAssignEvent(Seat seat)
        {
            _seat = seat;
        }

        public override string ToString()
        {
            return $"Events.DealerAssignEvent (Dealer: {_seat})";
        }
    }
}