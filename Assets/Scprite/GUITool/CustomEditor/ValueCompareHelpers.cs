using Custom.Editor.Drawers;
using System;
using UnityEngine;

namespace Custom.Editor.Drawers
{
    public static class ValueComparisonHelper
    {
        static ValueComparisonHelper()
        {
            comparers = new ValueComparerBase[]
            {
                new BoolComparer(),
                new IntegerComparer(),
                new FloatComparer(),
                new StringComparer(),
                new ObjectComparer()
            };
        }

        private readonly static ValueComparerBase[] comparers;


        public static bool TryCompare(object sourceValue, object targetValue, out bool result)
        {
            return TryCompare(sourceValue, targetValue, UnityComparisonMethod.Equal, out result);
        }

        public static bool TryCompare(object sourceValue, object targetValue, UnityComparisonMethod method, out bool result)
        {
            foreach (var comparer in comparers)
            {
                if (comparer.TryCompare(sourceValue, targetValue, method, out result))
                {
                    return true;
                }
            }

            result = false;
            return false;
        }
    }
}