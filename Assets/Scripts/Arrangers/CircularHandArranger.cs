using Events;
using JetBrains.Annotations;
using Makaretu.Bridge;
using Mappers;
using Resolvers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Arrangers
{
    public class CircularHandArranger : MonoBehaviour
    {
        [FormerlySerializedAs("Seat")]
        [SerializeField]
        private Seat seat;

        [FormerlySerializedAs("Path")]
        [SerializeField]
        private Transform[] path;

        [FormerlySerializedAs("Initial Rotation (Pre-Animation)")]
        [SerializeField]
        [Tooltip("Set this to `identity` (default) if the card deck is facing down initially.")]
        private Quaternion initialRotation = Quaternion.identity;

        [FormerlySerializedAs("Duration")]
        [SerializeField]
        [Tooltip("Animation duration in seconds.")]
        private float duration = 0.5f;

        [Inject]
        private IBoardResolver _boardResolver;

        [Inject]
        private ICardMapper _cardMapper;

        private int _cardsArrangedCount;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus?.On<DealEvent>(HandleDealEvent);
        }

        private void OnDisable()
        {
            _eventBus?.Off<DealEvent>(HandleDealEvent);
        }

        private void HandleDealEvent(DealEvent evt)
        {
            ArrangeCardsOnSpline();
        }

        private void ArrangeCardsOnSpline()
        {
            var board = _boardResolver.GetBoard();
            var hand = board.Hands[seat];
            var cards = hand.Cards;

            for (var i = 0; i < cards.Count; i++)
            {
                var card = _cardMapper.GetGameObject(cards[i]);

                card.layer = gameObject.layer; // Mine! (Culling mask from other players perspective).

                card.transform.rotation = initialRotation; // Handle face down (if must).

                var t = (float)i / (cards.Count - 1);
                iTween.PutOnPath(card, path, t);

                iTween.LookTo(
                    card,
                    iTween.Hash(
                        "looktarget", transform.position,
                        "axis", "y",
                        "time", duration,
                        "easetype", iTween.EaseType.easeInOutQuad,
                        "oncomplete", "OnLookComplete",
                        "oncompletetarget", gameObject,
                        "oncompleteparams", cards.Count
                    )
                );
            }
        }

        [UsedImplicitly]
        private void OnLookComplete(int cardsCount)
        {
            if (cardsCount == ++_cardsArrangedCount)
            {
                _eventBus?.Post(new CardsArrangedEvent());
            }
        }
    }
}