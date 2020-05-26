using System.IO;

namespace TexasHoldemCalculator.Interfaces.ReplayEngine
{
    public interface IReplayEngineProvider
    {
        Stream WriteableStream();

        Stream ReadableStream();
    }
}