using UnityEngine;
using Zenject;

// TODO This will be part of the UI
public class DealController : MonoBehaviour
{
    [Inject]
    private IBoardResolver _boardResolver;

    [Inject]
    private IDeckResolver _deckResolver;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        var board = _boardResolver.GetBoard();
        var deck = _deckResolver.GetDeck();

        deck.Shuffle()
            .Deal(board);

        EventBus.Post(new DealEvent());
    }
}