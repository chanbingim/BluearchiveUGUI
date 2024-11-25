using System;
using System.Collections.Generic;
using UnityEngine;

namespace Custom.Editor.Drawers
{
    internal class FloatComparer : ValueComparerBase
    {
        protected override HashSet<TypeCode> GetAcceptedTypeCodes()
        {
            return new HashSet<TypeCode>()
            {
                TypeCode.Single,
                TypeCode.Double,
                TypeCode.Decimal
            };
        }


        internal override bool IsValidMethod(UnityComparisonMethod method)
        {
            return method != UnityComparisonMethod.Mask;
        }

        internal override bool Compare(object sourceValue, object targetValue, UnityComparisonMethod testMethod)
        {
            var realSourceValue = Convert.ToSingle(sourceValue);
            var realTargetValue = Convert.ToSingle(targetValue);
            switch (testMethod)
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
                default:
                    return false;
            }
        }
    }
}