using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EyeTracker.Common
{
    public class ObjectSerializator<T>
    {

        private static Dictionary<Type, DataContractSerializer> m_serializers = 
            new Dictionary<Type, DataContractSerializer>();


        public static byte[] Serialize(object obj)
        {
            return null;
        }

        public static T Deserialize(byte[] arr)
        {
            return default(T);
        }

    }
}
