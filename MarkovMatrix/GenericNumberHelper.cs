using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    #warning todo move class to new math assembly
    public static class GenericNumberHelper
    {
        public static bool ValidateNumberType<T>()
        {
            #warning Add unit tests
            #warning Implement
            Type type = typeof(T);

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static T GetValue<T>(int value)
        {
            #warning Add unit tests
            #warning Implement
            throw new NotImplementedException();
        }

        internal static T Add<T>(T sourceValue, int valueToAdd)
        {
            #warning Add unit tests
            #warning Implement
            throw new NotImplementedException();
        }
    }
}
