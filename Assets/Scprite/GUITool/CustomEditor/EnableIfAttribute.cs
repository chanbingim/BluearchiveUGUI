using System;
using System.Diagnostics;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public class EnableIfAttribute : CustomAttribute
    {
        public EnableIfAttribute(string sourceHandle, object valueToMatch) : base(sourceHandle, valueToMatch)
        { }
    }
}
