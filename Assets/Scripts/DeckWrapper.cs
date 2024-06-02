using Makaretu.Bridge;
using UnityEngine;

public class DeckWrapper : MonoBehaviour
{
    public Deck Deck { get; private set; }

    private void Start()
    {
        Deck = new Deck();
    }
}