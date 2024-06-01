using Makaretu.Bridge;
using UnityEngine;
using UnityEngine.Serialization;

public class CardBehaviour : MonoBehaviour
{
    [FormerlySerializedAs("Rank")] [SerializeField]
    private Rank rank;

    [FormerlySerializedAs("Suit")] [SerializeField]
    private Suit suit;

    private Card Card { get; set; }

    private void Start()
    {
        Card = Card.Get(rank, suit);

        Debug.Log("Hello, I'm " + Card);
    }
}