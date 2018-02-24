using System;

using System.Runtime.Serialization;

namespace Domain
{
    // The token class.
    [DataContract]
    public class Token
    {
        [DataMember(Name = "access_token")]
        public string AccessToken;
    }
}
