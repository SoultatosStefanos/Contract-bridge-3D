using ContractBridge.Core;
using Events;
using Presenters;
using Registries;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace LifeCycleManagers
{
    // NOTE: Not ideal (non-local), but what to do
    public class CardHighlightLifecycleManager : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [FormerlySerializedAs("Partner Card Highlight Presenter")]
        [SerializeField]
        private CardHighlightPresenter partnerCardHighlightPresenter;

        [Inject]
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private void OnEnable()
        {
            _eventBus.On<SessionPhaseChangedEvent>(HandleSessionPhaseChangedEvent);
            _eventBus.On<GameFollowEvent>(HandleGameFollowEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<SessionPhaseChangedEvent>(HandleSessionPhaseChangedEvent);
            _eventBus.Off<GameFollowEvent>(HandleGameFollowEvent);
        }

        private void HandleGameFollowEvent(GameFollowEvent e)
        {
            var cardGameObject = _cardGameObjectRegistry.GetGameObject(e.Card);
            var outline = cardGameObject.GetComponent<Outline>();
            outline.enabled = false;
        }

        private void HandleSessionPhaseChangedEvent(SessionPhaseChangedEvent e)
        {
            if (e.Phase != Phase.Play)
            {
                return;
            }

            if (DummySeat() == playerSeat.Partner())
            {
                partnerCardHighlightPresenter.enabled = true;
            }
        }

        private Seat DummySeat()
        {
            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            Debug.Assert(_session.Auction.FinalContract != null, "_session.Auction.FinalContract != null");
            return _session.Auction.FinalContract.Dummy();
        }
    }
}