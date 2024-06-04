using UnityEngine;
using UnityEngine.Serialization;

// TODO Read from Hand, (by seat), use ICardManager
public class CircularHandArranger : MonoBehaviour
{
    [FormerlySerializedAs("Cards")]
    [SerializeField]
    private Transform[] cards;

    [FormerlySerializedAs("Path")]
    [SerializeField]
    private Transform[] path;

    [FormerlySerializedAs("Z Offset Step")]
    [SerializeField]
    [Tooltip("Use this offset variable to avoid z-fighting (clipping).")]
    private float zOffsetStep = 0.01f;

    private void Start()
    {
        ArrangeCardsOnSpline();
    }

    private void ArrangeCardsOnSpline()
    {
        for (var i = 0; i < cards.Length; i++)
        {
            var t = (float)i / (cards.Length - 1);

            iTween.PutOnPath(cards[i].gameObject, path, t);

            var cardPosition = cards[i].position;
            cardPosition.z += i * zOffsetStep;
            cards[i].position = cardPosition;

            iTween.LookTo(
                cards[i].gameObject,
                iTween.Hash(
                    "looktarget", transform.position,
                    "axis", "y"
                )
            );
        }
    }
}