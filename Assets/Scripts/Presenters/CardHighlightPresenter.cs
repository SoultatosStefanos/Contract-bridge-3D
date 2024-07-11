using System.Linq;
using ContractBridge.Core;
using Events;
using Registries;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Presenters
{
    public class CardHighlightPresenter : MonoBehaviour
    {
        [FormerlySerializedAs("Seat")]
        [SerializeField]
        private Seat seat;

        [Inject]
        private IBoard _board;

        [Inject]
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus.On<PlayExtrasPlaysSolutionSetEvent>(HandlePlayExtrasPlaysSolutionSetEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<PlayExtrasPlaysSolutionSetEvent>(HandlePlayExtrasPlaysSolutionSetEvent);
        }

        private void HandlePlayExtrasPlaysSolutionSetEvent(PlayExtrasPlaysSolutionSetEvent e)
        {
            var optimalPlays = e.Solution.OptimalPlays(seat);

            foreach (var card in _board.Hand(seat))
            {
                // NOTE: This is fine :)
                // ReSharper disable once PossibleMultipleEnumeration
                if (optimalPlays.Contains(card))
                {
                    HighlightCard(card);
                }
                else
                {
                    UnhighlightCard(card);
                }
            }
        }

        private void HighlightCard(ICard card)
        {
            var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);
            var outline = cardGameObject.GetComponent<Outline>();
            outline.enabled = true;
        }

        private void UnhighlightCard(ICard card)
        {
            var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);
            var outline = cardGameObject.GetComponent<Outline>();
            outline.enabled = false;
        }
    }
}