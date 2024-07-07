using System.Collections;
using ContractBridge.Core;
using Events;
using Registries;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Animators
{
    public class CardTrickWinAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("Seat")]
        [SerializeField]
        private Seat seat;

        [FormerlySerializedAs("Tricks Inter-Placeholder")]
        [SerializeField]
        private GameObject tricksInterPlaceholder;

        [FormerlySerializedAs("Tricks Placeholder")]
        [SerializeField]
        private GameObject tricksPlaceholder;

        [FormerlySerializedAs("Duration")]
        [SerializeField]
        [Tooltip("Animation duration in seconds.")]
        private float duration = 0.7f;

        [FormerlySerializedAs("Delay")]
        [SerializeField]
        [Tooltip("Delay before starting the animation.")]
        private float delay = 1f;

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

        private void Start()
        {
            _yOffset = yStackOffset;
        }

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

            StartCoroutine(AnimateTrick(e.Trick));
        }

        // NOTE: This is fine.
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator AnimateTrick(ITrick trick)
        {
            yield return new WaitForSeconds(delay);

            foreach (var card in trick)
            {
                var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);
                AnimateTrickCard(cardGameObject);
                _yOffset += yStackOffset; // Card stack effect.
            }
        }

        private void AnimateTrickCard(GameObject card)
        {
            var position1 = tricksInterPlaceholder.transform.position;

            var position2 = tricksPlaceholder.transform.position;
            position2.y += _yOffset;

            iTween.MoveTo(
                card,
                iTween.Hash(
                    "path", new[] { position1, position2 },
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