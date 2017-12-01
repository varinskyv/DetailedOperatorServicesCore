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
    }
}
