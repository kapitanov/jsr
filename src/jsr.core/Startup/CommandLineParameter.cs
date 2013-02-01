using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Startup
{
    [DebuggerDisplay("{name}={value}")]
    public sealed class CommandLineParameter : IEquatable<CommandLineParameter>
    {
        [NotNull]
        private readonly string _name;
        [CanBeNull]
        private readonly string _value;

        public CommandLineParameter([NotNull] string name, [CanBeNull] string value)
        {
            _name = name;
            _value = value;
        }

        [NotNull]
        public string Name { get { return _name; } }

        [CanBeNull]
        public string Value { get { return _value; } }

        public bool HasValue { get { return _value != null; } }

        #region Equality members

        public bool Equals(CommandLineParameter other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_name, other._name) && string.Equals(_value, other._value);
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
                return (_name.GetHashCode() * 397) ^ (_value != null ? _value.GetHashCode() : 0);
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