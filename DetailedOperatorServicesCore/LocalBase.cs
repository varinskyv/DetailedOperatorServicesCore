using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace DetailedOperatorServicesCore
{
    public class LocalBase
    {
        private static LocalBase instance;

        private static string fileName;

        private LocalBase(string fileName)
        {
            LocalBase.fileName = fileName;

            Init();
        }
        
        public static LocalBase getInstance()
        {
            if (instance == null)
                instance = new LocalBase(fileName);
            return instance;
        }

        private List<Subscriber> subscriberList = new List<Subscriber>();

        public List<Subscriber> SubscriberList
        {
            get
            {
                return subscriberList;
            }
        }

        private void Init()
        {
            Deserialize(fileName, ref subscriberList);
        }

        private void Deserialize<T>(string fileName, ref List<T> list)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    using (DeflateStream decompressionStream = new DeflateStream(fs, CompressionMode.Decompress))
                    {
                        list = (List<T>)formatter.Deserialize(decompressionStream);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Commit()
        {
            Serialize(fileName, subscriberList);
        }

        private void Serialize<T>(string fileName, List<T> list)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    using (DeflateStream compressionStream = new DeflateStream(fs, CompressionMode.Compress))
                    {
                        formatter.Serialize(compressionStream, list);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
