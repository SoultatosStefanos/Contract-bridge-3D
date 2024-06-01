using Makaretu.Bridge;
using UnityEngine;

// TODO Think this through - will this script be used only to serve a model? Or do we add behaviour?
// TODO(?) Rename

public class DeckBehaviour : MonoBehaviour
{
    public Deck Deck { get; private set; }

    private void Start()
    {
        Deck = new Deck();
    }
}