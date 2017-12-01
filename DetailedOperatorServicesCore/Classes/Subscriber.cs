using System;
using System.Collections.Generic;

namespace DetailedOperatorServicesCore
{
    [Serializable]
    public class Subscriber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public SubscriberType Type { get; set; }

        public List<Connection> ConnectionsList = new List<Connection>();

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            if (Id == ((Subscriber)obj).Id || Number.Equals(((Subscriber)obj).Number))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
