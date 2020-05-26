

namespace TexasHoldemCalculator.Core.Provider
{
	public class HoldemSerializer
	{
        //private IHoldemService Service
        //{
        //    get;
        //    set;
        //}

//        public HoldemSerializer(IHoldemService service)
//        {
//            this.Service = service;
//        }

//        public void Save<T>(string fileName, T objectToSave)
//        {
//#if WINDOWS_PHONE
//            using (var storageFile = this.Service.IsolatedStorage.GetUserStoreForApplication())
//            {
//                using (var stream = storageFile.CreateFile(fileName) as Stream )
//#else
//                using( var stream = new FileStream(fileName, FileMode.Create) )
//#endif
//                {
//#if BINARY_SERIALIZATION || WINDOWS_PHONE
//                    var writer = XmlDictionaryWriter.CreateBinaryWriter(stream);
//#else
//                    var writer = XmlDictionaryWriter.CreateTextWriter(stream);
//#endif
//                    var serializer = new DataContractSerializer(typeof(T));
//                    serializer.WriteObject(writer, objectToSave);
//                }
//            }
//        }

//        public T Load<T>(string name, bool useBinary) where T : class, new()
//        {
//            T loadedObject = null;
//#if WINDOWS_PHONE
//            using (var storageFile = this.Service.IsolatedStorage.GetUserStoreForApplication())
//            {
//                using (var stream = storageFile.OpenFile(name, FileMode.Open))
//#else
//                using( var stream = new FileStream(name, FileMode.Open) )
//#endif
//                {
//#if WINDOWS_PHONE
//                    var reader
//                        = ( useBinary
//                                ? XmlDictionaryReader.CreateBinaryReader(stream as Stream, XmlDictionaryReaderQuotas.Max)
//                                : XmlReader.Create(stream as Stream) );
//#else
//                    var reader
//                        = ( useBinary ? 
//                             XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max)
//                            : XmlDictionaryReader.CreateTextReader(stream, XmlDictionaryReaderQuotas.Max) );
//#endif
//                    if( stream.Length > 0 )
//                    {
//                        var serializer = new DataContractSerializer(typeof(T));
//                        loadedObject = serializer.ReadObject(reader) as T;
//                    }

//                    if( loadedObject == null )
//                        loadedObject = new T();
//                }
//            }

//            return loadedObject;
//        }
	}
}