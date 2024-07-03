using ContractBridge.Core;
using ContractBridge.Solver;
using Events;
using Extensions;
using UnityEngine;
using Zenject;

namespace Presenters
{
    // TODO

    public class MakeableContractsPresenter : MonoBehaviour
    {
        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus.On<AuctionExtrasSolutionSetEvent>(HandleAuctionExtrasSolutionSetEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionExtrasSolutionSetEvent>(HandleAuctionExtrasSolutionSetEvent);
        }

        private void HandleAuctionExtrasSolutionSetEvent(AuctionExtrasSolutionSetEvent e)
        {
            LogMakeableContracts(e.Solution);
        }

        // TODO Remove
        private static void LogMakeableContracts(IDoubleDummySolution solution)
        {
            foreach (var seat in EnumExtensions.AllValues<Seat>())
            {
                foreach (var contract in solution.MakeableContracts(seat))
                {
                    Debug.Log($"Seat: {seat} can make: {contract}");
                }
            }
        }
    }
}