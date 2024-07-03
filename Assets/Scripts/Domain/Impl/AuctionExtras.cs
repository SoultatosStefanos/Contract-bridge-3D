using ContractBridge.Core;
using ContractBridge.Solver;
using Events;

namespace Domain.Impl
{
    public class AuctionExtras : IAuctionExtras
    {
        private readonly IEventBus _eventBus;
        private IDoubleDummySolution _solution;

        public AuctionExtras(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IContract PickedContract { get; set; }

        public IDoubleDummySolution Solution
        {
            get => _solution;
            set
            {
                _solution = value;

                if (_solution != null)
                {
                    _eventBus.Post(new AuctionExtrasSolutionSetEvent(this, _solution));
                }
            }
        }
    }
}