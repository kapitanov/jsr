using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace JavaScript.Runtime
{
    [DebuggerDisplay("{name}={value}")]
    public sealed class CommandLineParameter : IEquatable<CommandLineParameter>
    {
        [NotNull]
        private readonly string name;
        [CanBeNull]
        private readonly string value;

        public CommandLineParameter([NotNull]string name, [CanBeNull]string value)
        {
            this.name = name;
            this.value = value;
        }

        [NotNull]
        public string Name { get { return name; } }

        [CanBeNull]
        public string Value { get { return value; } }

        public bool HasValue { get { return value != null; } }

        #region Equality members

        public bool Equals(CommandLineParameter other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(name, other.name) && string.Equals(value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is CommandLineParameter && Equals((CommandLineParameter)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (name.GetHashCode() * 397) ^ (value != null ? value.GetHashCode() : 0);
            }
        }

        public static bool operator ==(CommandLineParameter left, CommandLineParameter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CommandLineParameter left, CommandLineParameter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}