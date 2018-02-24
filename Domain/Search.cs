using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace Domain {
    // The search class used on the search operation.
    [DataContract]
    public class Search {
        [DataMember]
        public string Language;
        
        [DataMember]
        public string Currency;

        [DataMember(Name = "destination")]
        public string Destination;

        [DataMember]
        public string DateFrom;

        [DataMember(Name = "DateTO")]
        public string DateTo;

        [DataMember(Name = "Occupancy")]
        public SearchOccupancy SearchOccupancy;    
    }
}