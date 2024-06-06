using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace LifeCycle
{
    public class UILifeCycleManager : MonoBehaviour
    {
        [FormerlySerializedAs("Setup Canvas")]
        [SerializeField]
        private GameObject setupCanvas;

        [FormerlySerializedAs("Auction Canvas")]
        [SerializeField]
        private GameObject auctionCanvas;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus?.On<AuctionTransitionEvent>(HandleAuctionTransitionEvent);
        }

        private void OnDisable()
        {
            _eventBus?.Off<AuctionTransitionEvent>(HandleAuctionTransitionEvent);
        }

        private void HandleAuctionTransitionEvent(AuctionTransitionEvent evt)
        {
            setupCanvas.SetActive(false);
            auctionCanvas.SetActive(true);
        }
    }
}