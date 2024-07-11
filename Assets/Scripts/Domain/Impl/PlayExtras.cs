using ContractBridge.Core;
using ContractBridge.Solver;
using Events;
using Extensions;
using UnityEngine;

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
                    LogPlays();
                }
            }
        }

        // TODO Remove
        // NOTE For debugging only
        private void LogPlays()
        {
            foreach (var seat in EnumExtensions.AllValues<Seat>())
            {
                Debug.Log("\n----------------\n");

                Debug.Log($"Seat: ${seat} should play the following...");

                foreach (var card in _solution.OptimalPlays(seat))
                {
                    Debug.Log(card.ToString());
                }

                Debug.Log("\n----------------\n");
            }
        }
    }
}