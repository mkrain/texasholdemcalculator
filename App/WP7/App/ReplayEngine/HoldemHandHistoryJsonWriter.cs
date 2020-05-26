
namespace TexasHoldemCalculator.ReplayEngine
{
    //public class HoldemHandHistoryJsonWriter : HoldemHandHistoryWriterBase
    //{
    //    private readonly JsonSerializer _jSonSerializer;

    //    public HoldemHandHistoryJsonWriter(IHoldemService service) : base(service)
    //    {
    //         _jSonSerializer = new JsonSerializer();
    //    }

    //    #region Implementation of IHandHistoryWriter

    //    protected override HandHistoryWriterCollection LoadExistingHandHistory(IHoldemIsolatedStorageFileStream fileStream)
    //    {
    //        var collection = new HandHistoryWriterCollection();

    //        try
    //        {
    //            using (var reader = new StreamReader(fileStream as Stream))
    //            {
    //                var jsonStr = reader.ReadToEnd();

    //                collection = JsonConvert.DeserializeObject<HandHistoryWriterCollection>(jsonStr);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine(e);
    //        }

    //        return collection;
    //    }

    //    protected override void SaveExistingHandHistory(IHoldemIsolatedStorageFileStream fileStream)
    //    {
    //        try
    //        {
    //            using (TextWriter writer = new StreamWriter(fileStream as Stream))
    //            {
    //                _jSonSerializer.Serialize(writer, this.History);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine(e);
    //        }
    //    }

    //    #endregion
    //}
}
