using Makaretu.Bridge;
using UnityEngine;
using UnityEngine.Serialization;

// TODO Think this through - will this script be used only to serve a model? Or do we add behaviour?
// TODO(?) Rename

public class CardBehaviour : MonoBehaviour
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