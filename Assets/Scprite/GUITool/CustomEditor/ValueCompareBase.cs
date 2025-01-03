using System;
using System.Collections.Generic;
using UnityEngine;

namespace Custom.Editor.Drawers
{
    internal abstract class ValueComparerBase
    {
        protected readonly HashSet<TypeCode> acceptedTypeCodes;


        protected ValueComparerBase()
        {
            acceptedTypeCodes = GetAcceptedTypeCodes();
        }


        protected abstract HashSet<TypeCode> GetAcceptedTypeCodes();


        internal virtual bool IsValidInput(object sourceValue, object targetValue, UnityComparisonMethod method)
        {
            return IsValidSource(sourceValue) && IsValidTarget(targetValue) && IsValidMethod(method);
        }

        internal virtual bool IsValidSource(object value)
        {
            var valueType = value?.GetType();
            var valueCode = Type.GetTypeCode(valueType);
            return acceptedTypeCodes.Contains(valueCode);
        }

        internal virtual bool IsValidTarget(object value)
        {
            return IsValidSource(value);
        }

        internal virtual bool IsValidMethod(UnityComparisonMethod method)
        {
            return true;
        }

        internal virtual bool TryCompare(object sourceValue, out bool result)
        {
            return TryCompare(sourceValue, true, out result);
        }

        internal virtual bool TryCompare(object sourceValue, object targetValue, out bool result)
        {
            return TryCompare(sourceValue, targetValue, UnityComparisonMethod.Equal, out result);
        }

        internal virtual bool TryCompare(object sourceValue, object targetValue, UnityComparisonMethod method, out bool result)
        {
            if (!IsValidInput(sourceValue, targetValue, method))
            {
                result = false;
                return false;
            }

            result = Compare(sourceValue, targetValue, method);
            return true;
        }

        internal abstract bool Compare(object sourceValue, object targetValue, UnityComparisonMethod method);
    }
}