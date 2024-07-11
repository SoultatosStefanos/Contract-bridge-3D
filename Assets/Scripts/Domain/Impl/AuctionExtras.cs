using ContractBridge.Core;
using ContractBridge.Solver;
using Events;

namespace Domain.Impl
{
    public class AuctionExtras : IAuctionExtras
    {
        private readonly IEventBus _eventBus;

        private IDoubleDummyContractsSolution _solution;

        public AuctionExtras(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IContract PickedContract { get; set; }

        public IDoubleDummyContractsSolution Solution
        {
            get => _solution;
            set
            {
                _solution = value;

                if (_solution != null)
                {
                    _eventBus.Post(new AuctionExtrasContractsSolutionEvent(this, _solution));
                }
            }
        }

        // private void LogOptimalMoves()
        // {
        //     foreach (var seat in EnumExtensions.AllValues<Seat>())
        //     {
        //         Debug.Log($"----- Seat: {seat} -----");
        //
        //         foreach (var contract in _solution.MakeableContracts(seat))
        //         {
        //             Debug.Log($"-- Contract: {contract} --");
        //
        //             foreach (var card in _solution.OptimalPlays(contract))
        //             {
        //                 Debug.Log($"Optimal play: {card}");
        //             }
        //         }
        //     }
        // }
    }
}