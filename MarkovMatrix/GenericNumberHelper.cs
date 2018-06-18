﻿using System;
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
            Type type = typeof(T);
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                    return (T)(object)(byte)value;
                case TypeCode.SByte:
                    return (T)(object)(sbyte)value;
                case TypeCode.UInt16:
                    return (T)(object)(UInt16)value;
                case TypeCode.UInt32:
                    return (T)(object)(UInt32)value;
                case TypeCode.UInt64:
                    return (T)(object)(UInt64)value;
                case TypeCode.Int16:
                    return (T)(object)(Int16)value;
                case TypeCode.Int32:
                    return (T)(object)value;
                case TypeCode.Int64:
                    return (T)(object)(Int64)value;
                case TypeCode.Decimal:
                    return (T)(object)(Decimal)value;
                case TypeCode.Double:
                    return (T)(object)(Double)value;
                case TypeCode.Single:
                default:
                    return (T)(object)(Single)value;
            }
        }

        internal static T Add<T>(T sourceValue, int valueToAdd)
        {
            #warning Add unit tests

            Type type = typeof(T);
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                    return (T)(object)((byte)(object)sourceValue + valueToAdd);
                case TypeCode.SByte:
                    return (T)(object)((sbyte)(object)sourceValue + valueToAdd);
                case TypeCode.UInt16:
                    return (T)(object)((UInt16)(object)sourceValue + valueToAdd);
                case TypeCode.UInt32:
                    return (T)(object)((UInt32)(object)sourceValue + valueToAdd);
                case TypeCode.UInt64:
                    return (T)(object)((UInt64)(object)sourceValue + (UInt64)valueToAdd);
                case TypeCode.Int16:
                    return (T)(object)((Int16)(object)sourceValue + valueToAdd);
                case TypeCode.Int32:
                    return (T)(object)((Int32)(object)sourceValue + valueToAdd);
                case TypeCode.Int64:
                    return (T)(object)((Int64)(object)sourceValue + valueToAdd);
                case TypeCode.Decimal:
                    return (T)(object)((Decimal)(object)sourceValue + valueToAdd);
                case TypeCode.Double:
                    return (T)(object)((Double)(object)sourceValue + valueToAdd);
                case TypeCode.Single:
                default:
                    return (T)(object)((Single)(object)sourceValue + valueToAdd);
            }
        }
    }
}
