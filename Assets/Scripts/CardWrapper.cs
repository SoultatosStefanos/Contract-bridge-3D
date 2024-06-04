using Makaretu.Bridge;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class CardWrapper : MonoBehaviour
{
    [FormerlySerializedAs("Rank")]
    [SerializeField]
    private Rank rank;

    [FormerlySerializedAs("Suit")]
    [SerializeField]
    private Suit suit;

    private ICardManager _cardManager;

    public Card Card { get; private set; }

    private void Start()
    {
        Card = Card.Get(rank, suit);

        _cardManager?.Register(Card, gameObject);
    }

    [Inject]
    public void Construct(ICardManager cardManager)
    {
        _cardManager = cardManager;
    }
}