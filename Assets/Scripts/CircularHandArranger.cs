using Makaretu.Bridge;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class CircularHandArranger : MonoBehaviour
{
    [FormerlySerializedAs("Seat")]
    [SerializeField]
    private Seat seat;

    [FormerlySerializedAs("Path")]
    [SerializeField]
    private Transform[] path;

    [FormerlySerializedAs("Z Offset Step")]
    [SerializeField]
    [Tooltip("Use this offset variable to avoid z-fighting (clipping).")]
    private float zOffsetStep = 0.01f;

    [FormerlySerializedAs("Initial Rotation (Pre-Animation)")]
    [SerializeField]
    [Tooltip("Set this to `identity` (default) if the card deck is facing down initially.")]
    private Quaternion initialRotation = Quaternion.identity;

    [Inject]
    private IBoardResolver _boardResolver;

    [Inject]
    private ICardManager _cardManager;

    private void OnEnable()
    {
        EventBus.On<DealEvent>(HandleDealEvent);
    }

    private void OnDisable()
    {
        EventBus.Off<DealEvent>(HandleDealEvent);
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
            var card = _cardManager.GetGameObject(cards[i]);

            card.transform.rotation = initialRotation; // Handle face down.

            var t = (float)i / (cards.Count - 1);
            iTween.PutOnPath(card, path, t);

            OffsetZPosition(card, i); // To avoid z-fighting (clipping).

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

        return;

        void OffsetZPosition(GameObject card, int i)
        {
            var cardPosition = card.transform.position;
            cardPosition.z += i * zOffsetStep;
            card.transform.position = cardPosition;
        }
    }
}