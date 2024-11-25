using Custom.Editor.Drawers;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Custom.Editor.Drawers
{
    internal class BoolComparer : ValueComparerBase
    {
        protected override HashSet<TypeCode> GetAcceptedTypeCodes()
        {
            return new HashSet<TypeCode>()
            {
                TypeCode.Boolean
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