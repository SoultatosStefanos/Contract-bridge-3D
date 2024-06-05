using Events;
using Makaretu.Bridge;
using Resolvers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Arrangers
{
    public class DealerChipArranger : MonoBehaviour
    {
        [FormerlySerializedAs("Dealer Chip")]
        [SerializeField]
        private GameObject dealerChip;

        [FormerlySerializedAs("Duration")]
        [SerializeField]
        [Tooltip("Animation duration in seconds.")]
        private float duration = 2.5f;

        [FormerlySerializedAs("East Chip Placeholder")]
        [SerializeField]
        private GameObject eastChipPlaceholder;

        [FormerlySerializedAs("North Chip Placeholder")]
        [SerializeField]
        private GameObject northChipPlaceholder;

        [FormerlySerializedAs("South Chip Placeholder")]
        [SerializeField]
        private GameObject southChipPlaceholder;

        [FormerlySerializedAs("West Chip Placeholder")]
        [SerializeField]
        private GameObject westChipPlaceholder;

        [Inject]
        private IBoardResolver _boardResolver;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus.On<DealerAssignEvent>(HandleAssignDealerEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<DealerAssignEvent>(HandleAssignDealerEvent);
        }

        private void HandleAssignDealerEvent(DealerAssignEvent evt)
        {
            ArrangeDealerChip();
        }

        private void ArrangeDealerChip()
        {
            var board = _boardResolver.GetBoard();
            var dealer = board.Dealer;

            var chipPlaceHolder = DispatchChipPlaceHolder(dealer);

            iTween.MoveTo(
                dealerChip,
                iTween.Hash(
                    "position", chipPlaceHolder.transform.position,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutSine
                )
            );

            return;

            GameObject DispatchChipPlaceHolder(Seat dealerSeat)
            {
                return dealerSeat switch
                {
                    Seat.North => northChipPlaceholder,
                    Seat.South => southChipPlaceholder,
                    Seat.West => westChipPlaceholder,
                    Seat.East => eastChipPlaceholder,
                    _ => null
                };
            }
        }
    }
}