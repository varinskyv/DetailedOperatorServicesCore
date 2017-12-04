using System;

namespace DetailedOperatorServicesCore
{
    [Serializable]
    public class Connection
    {
        public int Id { get; set; }
        public ConnectionType Type { get; set; }
        public DateTime Date { get; set; }
        public string IOTarget { get; set; }
        public decimal Cost { get; set; }
        public int Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            if (Id == ((Connection)obj).Id)
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
