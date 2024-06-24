using ContractBridge.Core;
using Registries;
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
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private IDeck _deck;

        public ICard Card { get; private set; }

        private void Start()
        {
            Card = _deck[rank, suit];

            _cardGameObjectRegistry.Register(Card, gameObject);
        }
    }
}