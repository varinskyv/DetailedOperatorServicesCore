using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace DetailedOperatorServicesCore
{
    public class LocalBase
    {
        private static LocalBase instance;

        private static string fileName;

        private LocalBase()
        {
            //
        }
        
        public static LocalBase getInstance()
        {
            if (instance == null)
                instance = new LocalBase();
            return instance;
        }

        private List<Subscriber> subscriberList = new List<Subscriber>();

        private bool IsStartTransaction = false;

        public List<Subscriber> SubscriberList
        {
            get
            {
                return subscriberList;
            }
        }

        public bool Init(string fileName)
        {
            LocalBase.fileName = fileName;

            return Deserialize(fileName, ref subscriberList);
        }

        private bool Deserialize<T>(string fileName, ref List<T> list)
        {
            bool result = false;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    using (DeflateStream decompressionStream = new DeflateStream(fs, CompressionMode.Decompress))
                    {
                        list = (List<T>)formatter.Deserialize(decompressionStream);

                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }

        public void Transaction()
        {
            IsStartTransaction = true;
        }

        public bool Commit()
        {
            IsStartTransaction = false;

            return Serialize(fileName, subscriberList);
        }

        private bool Serialize<T>(string fileName, List<T> list)
        {
            bool result = false;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    using (DeflateStream compressionStream = new DeflateStream(fs, CompressionMode.Compress))
                    {
                        formatter.Serialize(compressionStream, list);

                        result = true;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }

        public int AddSubscriber(Subscriber subscriber)
        {
            int result = 0;

            if (subscriber == null)
                return result;

            Subscriber sub = subscriberList.Find(item => item.Equals(subscriber));
            if (sub == null)
            {
                if (subscriberList.Count > 0)
                    subscriber.Id = subscriberList.Max(item => item.Id) + 1;
                else
                    subscriber.Id = 1;

                subscriberList.Add(subscriber);

                if (!IsStartTransaction)
                    Commit();

                result = subscriber.Id;
            }
            else
                result = sub.Id;

            return result;
        }
    }
}
