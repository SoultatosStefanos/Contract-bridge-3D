using Makaretu.Bridge;

public class DeckResolver : IDeckResolver
{
    private readonly Deck _deck = new Deck();

    public Deck GetDeck()
    {
        return _deck;
    }
}