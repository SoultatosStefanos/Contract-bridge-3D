using ContractBridge.Core;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace LifeCycleManagers
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
            _eventBus?.On<SessionPhaseChangedEvent>(HandleSessionChangedEvent);
        }

        private void OnDisable()
        {
            _eventBus?.Off<SessionPhaseChangedEvent>(HandleSessionChangedEvent);
        }

        private void HandleSessionChangedEvent(SessionPhaseChangedEvent evt)
        {
            if (evt.Phase != Phase.Auction)
            {
                return;
            }

            setupCanvas.SetActive(false);
            auctionCanvas.SetActive(true);
        }
    }
}