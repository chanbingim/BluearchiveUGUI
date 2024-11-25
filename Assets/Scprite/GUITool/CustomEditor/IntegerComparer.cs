using Custom.Editor.Drawers;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Custom.Editor.Drawers
{
    internal class IntegerComparer : ValueComparerBase
    {
        protected override HashSet<TypeCode> GetAcceptedTypeCodes()
        {
            return new HashSet<TypeCode>()
            {
                TypeCode.SByte,
                TypeCode.Byte,
                TypeCode.Int16,
                TypeCode.UInt16,
                TypeCode.Int32,
                TypeCode.UInt32,
                TypeCode.Int64,
                TypeCode.UInt64
            };
        }


        internal override bool Compare(object sourceValue, object targetValue, UnityComparisonMethod method)
        {
            var realSourceValue = Convert.ToInt32(sourceValue);
            var realTargetValue = Convert.ToInt32(targetValue);
            switch (method)
            {
                case UnityComparisonMethod.Equal:
                    return realSourceValue == realTargetValue;
                case UnityComparisonMethod.Greater:
                    return realSourceValue > realTargetValue;
                case UnityComparisonMethod.Less:
                    return realSourceValue < realTargetValue;
                case UnityComparisonMethod.GreaterEqual:
                    return realSourceValue >= realTargetValue;
                case UnityComparisonMethod.LessEqual:
                    return realSourceValue <= realTargetValue;
                case UnityComparisonMethod.Mask:
                    return (realSourceValue & realTargetValue) == realTargetValue;
                default:
                    return false;
            }
        }
    }
}