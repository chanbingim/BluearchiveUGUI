using UnityEditor;
using UnityEngine;
using Toolbox.Editor;

namespace Custom.Editor.Drawers
{
    public abstract class ComparisonAttributeDrawer<T> : AttributeDrawer<T> where T : CustomAttribute
    {
        protected override PropertyCondition OnGuiValidateSafe(SerializedProperty property, T attribute)
        {
            var sourceHandle = attribute.SourceHandle;
            if (!ValueExtractionHelper.TryGetValue(sourceHandle, property, out var value, out var hasMixedValues))
            {
                return PropertyCondition.Valid;
            }

            var comparison = (UnityComparisonMethod)attribute.Comparison;
            var targetValue = attribute.ValueToMatch;
            if (!ValueComparisonHelper.TryCompare(value, targetValue, comparison, out var result))
            {
                return PropertyCondition.Valid;
            }

            return OnComparisonResult(hasMixedValues ? false : result);
        }

        protected abstract PropertyCondition OnComparisonResult(bool result);
    }
}