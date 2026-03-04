using AlgoDDD.SharedKernel.Domain.ValueObjects;
using System.Collections.Generic;

namespace AlgoDDD.SharedKernel.Domain.ValueObjects
{
    public class Symbol : ValueObject
    {
        public string Value { get; }

        public Symbol(string value)
        {
            Value = value.ToUpperInvariant();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}