using System.Linq;
using ContractBridge.Core;
using ContractBridge.Solver;
using Domain;
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

        [Inject]
        private IPlayExtras _playExtras;

        private void OnEnable()
        {
            _eventBus.On<PlayExtrasPlaysSolutionSetEvent>(HandlePlayExtrasPlaysSolutionSetEvent);

            if (_playExtras.Solution is { } solution)
            {
                HighlightOptimalPlays(solution);
            }
        }

        private void OnDisable()
        {
            _eventBus.Off<PlayExtrasPlaysSolutionSetEvent>(HandlePlayExtrasPlaysSolutionSetEvent);

            UnhighlightAllCards();
        }

        private void HandlePlayExtrasPlaysSolutionSetEvent(PlayExtrasPlaysSolutionSetEvent e)
        {
            HighlightOptimalPlays(e.Solution);
        }

        private void HighlightOptimalPlays(IDoubleDummyPlaysSolution solution)
        {
            var optimalPlays = solution.OptimalPlays(seat);

            // TODO Use priority for highlighting

            foreach (var card in _board.Hand(seat))
            {
                // NOTE: This is fine :)
                // ReSharper disable once PossibleMultipleEnumeration
                if (optimalPlays.Any(play => Equals(play.Item1, card)))
                {
                    HighlightCard(card);
                }
                else
                {
                    UnhighlightCard(card);
                }
            }
        }

        private void UnhighlightAllCards()
        {
            foreach (var card in _board.Hand(seat))
            {
                UnhighlightCard(card);
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