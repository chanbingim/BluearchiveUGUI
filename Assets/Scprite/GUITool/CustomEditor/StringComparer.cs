using System;
using System.Collections.Generic;
using UnityEngine;

namespace Custom.Editor.Drawers
{
    internal class StringComparer : ValueComparerBase
    {
        protected override HashSet<TypeCode> GetAcceptedTypeCodes()
        {
            return new HashSet<TypeCode>()
            {
                TypeCode.Char,
                TypeCode.String
            };
        }


        internal override bool IsValidMethod(UnityComparisonMethod method)
        {
            return method == UnityComparisonMethod.Equal;
        }

        internal override bool Compare(object sourceValue, object targetValue, UnityComparisonMethod method)
        {
            switch (method)
            {
                case UnityComparisonMethod.Equal:
                    return sourceValue.Equals(targetValue);
                default:
                    return false;
            }
        }
    }
}