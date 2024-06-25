using ContractBridge.Core;
using Events;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Animators
{
    public class DealerChipAnimator : MonoBehaviour
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
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus?.On<BoardDealerSetEvent>(HandleBoardDealerSetEvent);
        }

        private void OnDisable()
        {
            _eventBus?.Off<BoardDealerSetEvent>(HandleBoardDealerSetEvent);
        }

        private void HandleBoardDealerSetEvent(BoardDealerSetEvent evt)
        {
            AnimateDealerChip(evt.Dealer);
        }

        private void AnimateDealerChip(Seat dealer)
        {
            var chipPlaceHolder = DispatchChipPlaceHolder(dealer);

            iTween.MoveTo(
                dealerChip,
                iTween.Hash(
                    "position", chipPlaceHolder.transform.position,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutSine,
                    "oncomplete", "OnMoveComplete",
                    "oncompletetarget", gameObject
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

        [UsedImplicitly]
        private void OnMoveComplete()
        {
            _eventBus?.Post(new DealerChipAnimatedEvent());
        }
    }
}