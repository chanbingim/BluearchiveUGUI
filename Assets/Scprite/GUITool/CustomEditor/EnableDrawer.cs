using Custom.Editor.Drawers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnableIfAttribute))]
public class EnableDrawer : ComparisonAttributeDrawer<EnableIfAttribute>
{
    protected override PropertyCondition OnComparisonResult(bool result)
    {
        return result ? PropertyCondition.NonValid : PropertyCondition.Valid;
    }
}
