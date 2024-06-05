using Makaretu.Bridge;
using Mappers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Wrappers
{
    public class CardWrapper : MonoBehaviour
    {
        [FormerlySerializedAs("Rank")]
        [SerializeField]
        private Rank rank;

        [FormerlySerializedAs("Suit")]
        [SerializeField]
        private Suit suit;

        [Inject]
        private ICardMapper _cardMapper;

        public Card Card { get; private set; }

        private void Start()
        {
            Card = Card.Get(rank, suit);

            _cardMapper?.MapGameObject(Card, gameObject);
        }
    }
}