using Events;
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

        [Inject]
        private IBoardResolver _boardResolver;

        [Inject]
        private ICardMapper _cardMapper;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus.On<DealEvent>(HandleDealEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<DealEvent>(HandleDealEvent);
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

                card.transform.rotation = initialRotation; // Handle face down.

                var t = (float)i / (cards.Count - 1);
                iTween.PutOnPath(card, path, t);

                iTween.LookTo(
                    card,
                    iTween.Hash(
                        "looktarget", transform.position,
                        "axis", "y",
                        "time", 0.5f,
                        "easetype", iTween.EaseType.easeInOutQuad
                    )
                );
            }
        }
    }
}