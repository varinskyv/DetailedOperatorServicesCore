using System;

namespace DetailedOperatorServicesCore
{
    [Serializable]
    public enum ConnectionType
    {
        IncomingCall,
        OutgoingCall,
        IncomingSMS,
        OutgoingSMS,
        IncomingMMS,
        OutgoingMMS,
        GPRS,
        WAP,
        ServiceConnection
    }
}
