using System;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Util
{
    public static class Verify
    {
        public static void ArgumentNotNull([CanBeNull] object argument, [InvokerParameterName] string argumentName)
        {
            if (ReferenceEquals(argument, null))
            {
                ThrowHelper.ThrowArgumentNotNullException(argumentName);
            }
        }

        public static void ArgumentNotNullOrEmpty([CanBeNull] string argument, [InvokerParameterName] string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                ThrowHelper.ThrowArgumentNotNullException(argumentName);
            }
        }
        
        private static class ThrowHelper
        {
            public static void ThrowArgumentNotNullException(string argumentName)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}