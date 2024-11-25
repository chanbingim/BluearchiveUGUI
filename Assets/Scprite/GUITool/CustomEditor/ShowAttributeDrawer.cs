using Custom.Editor.Drawers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowAttributeDrawer : ComparisonAttributeDrawer<ShowIfAttribute>
{
    protected override PropertyCondition OnComparisonResult(bool result)
    {
        return result ? PropertyCondition.Valid : PropertyCondition.NonValid;
    }
}
