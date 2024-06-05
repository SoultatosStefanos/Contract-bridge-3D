using Makaretu.Bridge;

namespace Resolvers.Impl
{
    public class DeckResolver : IDeckResolver
    {
        private readonly Deck _deck = new Deck();

        public Deck GetDeck()
        {
            return _deck;
        }
    }
}