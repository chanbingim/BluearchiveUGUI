using Toolbox.Editor;
using UnityEditor;
using UnityEngine;

namespace Custom.Editor.Drawers
{
    public abstract class AttributeDrawer<T> : ToolBoxDrawerBase where T : CustomAttribute
    {
        protected virtual PropertyCondition OnGuiValidateSafe(SerializedProperty property, T attribute)
        {
            return PropertyCondition.Valid;
        }

        public sealed override PropertyCondition OnGuiValidate(SerializedProperty property)
        {
            return OnGuiValidate(property, PropertyUtility.GetAttribute<T>(property));
        }

        public sealed override PropertyCondition OnGuiValidate(SerializedProperty property, CustomAttribute attribute)
        {
            return OnGuiValidate(property, attribute as T);
        }

        public PropertyCondition OnGuiValidate(SerializedProperty property, T attribute)
        {
            if (attribute == null)
            {
                return PropertyCondition.Valid;
            }

            return OnGuiValidateSafe(property, attribute);
        }
    }
}
