using Makaretu.Bridge;
using UnityEngine;
using UnityEngine.Serialization;

public class CardWrapper : MonoBehaviour
{
    [FormerlySerializedAs("Rank")]
    [SerializeField]
    private Rank rank;

    [FormerlySerializedAs("Suit")]
    [SerializeField]
    private Suit suit;

    public Card Card { get; private set; }

    private void Start()
    {
        Card = Card.Get(rank, suit);
    }
}