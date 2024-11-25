using UnityEditor;
using UnityEngine;

namespace Custom.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIfAttributeDrawer : ComparisonAttributeDrawer<HideIfAttribute>
    {
        protected override PropertyCondition OnComparisonResult(bool result)
        {
            return result ? PropertyCondition.NonValid : PropertyCondition.Valid;
        }
    }
}