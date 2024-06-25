using ContractBridge.Core;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Controllers
{
    public class ShuffleAndDealController : MonoBehaviour
    {
        [Inject]
        private IBoard _board;

        [Inject]
        private IDeck _deck;

        public void ShuffleAndDeal()
        {
            _deck.Shuffle(new Random());
            _deck.Deal(_board);
        }
    }
}