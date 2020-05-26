using System.IO;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    public class StartingHandSaveContext
    {
        public Stream StartingHandStream { get; set; }

        public string FileName { get; set; }

        public StartingHandSaveContext()
        {
            
        }
        
        public StartingHandSaveContext(Stream stream, string fileName)
        {
            this.StartingHandStream = stream;
            this.FileName = fileName;
        }
    }
}