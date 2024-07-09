using ContractBridge.Core;
using Events;
using Extensions;
using Registries;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Debug = System.Diagnostics.Debug;

namespace Animators
{
    public class DummyHandAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("Seat")]
        [SerializeField]
        private Seat seat;

        [FormerlySerializedAs("Path")]
        [SerializeField]
        private Transform[] path;

        [FormerlySerializedAs("Rotation")]
        [SerializeField]
        private Vector3 rotation = new(0, 0, 0);

        [FormerlySerializedAs("Duration")]
        [SerializeField]
        [Tooltip("Animation duration in seconds.")]
        private float duration = 0.5f;

        [FormerlySerializedAs("Player Layer Mask")]
        [SerializeField]
        private LayerMask playerLayerMask;

        [Inject]
        private IBoard _board;

        [Inject]
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private void OnEnable()
        {
            _eventBus.On<SessionPhaseChangedEvent>(HandleSessionChangedEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<SessionPhaseChangedEvent>(HandleSessionChangedEvent);
        }

        private void HandleSessionChangedEvent(SessionPhaseChangedEvent e)
        {
            if (e.Phase != Phase.Play)
            {
                return;
            }

            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            Debug.Assert(_session.Auction.FinalContract != null, "_session.Auction.FinalContract != null");

            if (_session.Auction.FinalContract.Dummy() == seat)
            {
                AnimateCardsOnSpline();
            }
        }

        private void AnimateCardsOnSpline()
        {
            var hand = _board.Hand(seat);

            for (var i = 0; i < hand.Count; i++)
            {
                var card = _cardGameObjectRegistry.GetGameObject(hand[i]);

                card.layer = playerLayerMask.LayerNumber(); // Make visible to player.

                var t = (float)i / (hand.Count - 1);
                iTween.PutOnPath(card, path, t);

                iTween.RotateTo(
                    card,
                    iTween.Hash(
                        "rotation", rotation,
                        "time", duration,
                        "easetype", iTween.EaseType.easeInOutSine
                    )
                );
            }
        }
    }
}