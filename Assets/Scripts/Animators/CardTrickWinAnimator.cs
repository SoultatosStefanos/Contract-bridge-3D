using ContractBridge.Core;
using Events;
using Registries;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Animators
{
    // TODO Introduce short delay

    public class CardTrickWinAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("Seat")]
        [SerializeField]
        private Seat seat;

        [FormerlySerializedAs("Tricks Placeholder")]
        [SerializeField]
        private GameObject tricksPlaceholder;

        [FormerlySerializedAs("Duration")]
        [SerializeField]
        [Tooltip("Animation duration in seconds.")]
        private float duration = 0.7f;

        [FormerlySerializedAs("Rotation")]
        [SerializeField]
        [Tooltip("Animation rotation.")]
        private Vector3 rotation = new(0, 180, 0);

        [FormerlySerializedAs("Y Stack Offset")]
        [SerializeField]
        [Tooltip("The y-axis offset for the card stack effect of each trick stash")]
        private float yStackOffset = 0.001F;

        [Inject]
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private IEventBus _eventBus;

        private float _yOffset;

        private void OnEnable()
        {
            _eventBus.On<GameTrickWonEvent>(HandleTrickWonEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<GameTrickWonEvent>(HandleTrickWonEvent);
        }

        private void HandleTrickWonEvent(GameTrickWonEvent e)
        {
            if (e.Seat != seat)
            {
                return;
            }

            AnimateTrick(e.Trick);
        }

        private void AnimateTrick(ITrick trick)
        {
            foreach (var card in trick)
            {
                var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);
                AnimateTrickCard(cardGameObject);
                _yOffset += yStackOffset; // Card stack effect.
            }
        }

        private void AnimateTrickCard(GameObject card)
        {
            var position = tricksPlaceholder.transform.position;
            position.y += _yOffset;

            iTween.MoveTo(
                card,
                iTween.Hash(
                    "position", position,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutSine
                )
            );

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