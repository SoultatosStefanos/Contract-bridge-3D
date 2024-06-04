using UnityEngine;
using UnityEngine.Serialization;

public class CircularHand : MonoBehaviour
{
    [FormerlySerializedAs("Cards")]
    [SerializeField]
    private Transform[] cards;

    [FormerlySerializedAs("Path")]
    [SerializeField]
    private Transform[] path;

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