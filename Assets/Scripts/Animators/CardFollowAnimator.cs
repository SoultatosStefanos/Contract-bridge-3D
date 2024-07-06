using System;
using Events;
using Extensions;
using Registries;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Animators
{
    public class CardFollowAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("Played Card Placeholder 1")]
        [SerializeField]
        private GameObject playedCardPlaceholder1;

        [FormerlySerializedAs("Played Card Placeholder 2")]
        [SerializeField]
        private GameObject playedCardPlaceholder2;

        [FormerlySerializedAs("Played Card Placeholder 3")]
        [SerializeField]
        private GameObject playedCardPlaceholder3;

        [FormerlySerializedAs("Played Card Placeholder 4")]
        [SerializeField]
        private GameObject playedCardPlaceholder4;

        [FormerlySerializedAs("Duration")]
        [SerializeField]
        [Tooltip("Animation duration in seconds.")]
        private float duration = 0.7f;

        [FormerlySerializedAs("Rotation")]
        [SerializeField]
        [Tooltip("Animation rotation.")]
        private Vector3 rotation = new(0, 0, -90);

        [FormerlySerializedAs("Player Layer")]
        [SerializeField]
        private LayerMask playerLayerMask;

        [Inject]
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private IEventBus _eventBus;

        private int _playCount;

        private void OnEnable()
        {
            _eventBus.On<GameFollowEvent>(HandleGameFollowEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<GameFollowEvent>(HandleGameFollowEvent);
        }

        private void HandleGameFollowEvent(GameFollowEvent e)
        {
            var cardGameObject = _cardGameObjectRegistry.GetGameObject(e.Card);
            AnimateCard(cardGameObject);
            IncrementPlayCount();
        }

        private void AnimateCard(GameObject card)
        {
            var placeHolder = PickPlaceHolder();

            card.layer = playerLayerMask.LayerNumber(); // Make visible to player.

            iTween.MoveTo(
                card,
                iTween.Hash(
                    "position", placeHolder.transform.position,
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

        private GameObject PickPlaceHolder()
        {
            return _playCount switch
            {
                0 => playedCardPlaceholder1,
                1 => playedCardPlaceholder2,
                2 => playedCardPlaceholder3,
                3 => playedCardPlaceholder4,
                _ => throw new IndexOutOfRangeException()
            };
        }

        private void IncrementPlayCount()
        {
            _playCount = (_playCount + 1) % 4;
        }
    }
}