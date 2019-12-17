using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Roguelike
{   [DataContract]
    class BaseEntity
    {   [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Point Coords { get; set; }
        [DataMember]
        public Point PrevCoords { get; set; }
        [DataMember]
        public char Symbol { get; set; }
    }
}
