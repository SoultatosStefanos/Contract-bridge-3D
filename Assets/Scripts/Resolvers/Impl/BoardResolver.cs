using Makaretu.Bridge;

namespace Resolvers.Impl
{
    public class BoardResolver : IBoardResolver
    {
        private readonly Board _board = new Board();

        public Board GetBoard()
        {
            return _board;
        }
    }
}