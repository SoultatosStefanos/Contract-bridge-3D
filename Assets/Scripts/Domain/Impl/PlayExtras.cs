using ContractBridge.Solver;
using Events;

namespace Domain.Impl
{
    public class PlayExtras : IPlayExtras
    {
        private readonly IEventBus _eventBus;

        private IDoubleDummyPlaysSolution _solution;

        public PlayExtras(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IDoubleDummyPlaysSolution Solution
        {
            get => _solution;
            set
            {
                _solution = value;

                if (_solution != null)
                {
                    _eventBus.Post(new PlayExtrasPlaysSolutionSetEvent(this, _solution));
                }
            }
        }
    }
}