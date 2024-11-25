using System;
using System.Diagnostics;

namespace UnityEngine
{
    /// <param name="sourceHandle">L-value or the mask if <see cref="Comparison"/> is set to <see cref="UnityComparisonMethod.Mask"/></param>
    /// <param name="valueToMatch">R-value or the flag if <see cref="Comparison"/> is set to <see cref="UnityComparisonMethod.Mask"/></param>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public abstract class CustomAttribute : PropertyAttribute
    {
        public string SourceHandle { get; private set; }
        public object ValueToMatch { get; private set; }

        public CustomAttribute(string sourceHandle, object valueToMatch)
        {
            //트리거
            SourceHandle = sourceHandle;

            //값
            ValueToMatch = valueToMatch;
        }

        //값 탐지 조건
        public UnityComparisonMethod Comparison { get; set; } = UnityComparisonMethod.Equal;
    }

    public enum UnityComparisonMethod
    {
        Equal,
        Greater,
        Less,
        GreaterEqual,
        LessEqual,
        Mask
    }
}