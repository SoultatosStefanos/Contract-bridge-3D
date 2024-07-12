using System;
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

        [FormerlySerializedAs("Low Priority Color")]
        [SerializeField]
        private Color lowPriorityColor = Color.yellow;

        [FormerlySerializedAs("Medium Priority Color")]
        [SerializeField]
        private Color mediumPriorityColor = new(255, 165, 0);

        [FormerlySerializedAs("High Priority Color")]
        [SerializeField]
        private Color highPriorityColor = Color.green;

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
            var optimalPlaysArray = optimalPlays as (ICard, Priority)[] ?? optimalPlays.ToArray();

            foreach (var card in _board.Hand(seat))
            {
                var (cardPlay, priority) = optimalPlaysArray.FirstOrDefault(play => Equals(play.Item1, card));

                if (cardPlay != null) // Non-default value.
                {
                    HighlightCard(card, priority);
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

        private void HighlightCard(ICard card, Priority priority)
        {
            var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);
            var outline = cardGameObject.GetComponent<Outline>();
            outline.enabled = true;
            outline.OutlineColor = ColorCodeBy(priority);
        }

        private Color ColorCodeBy(Priority priority)
        {
            return priority switch
            {
                Priority.Low => lowPriorityColor,
                Priority.Medium => mediumPriorityColor,
                Priority.High => highPriorityColor,
                _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, null)
            };
        }

        private void UnhighlightCard(ICard card)
        {
            var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);
            var outline = cardGameObject.GetComponent<Outline>();
            outline.enabled = false;
        }
    }
}