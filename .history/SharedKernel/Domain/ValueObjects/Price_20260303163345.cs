using AlgoDDD.SharedKernel.Domain.ValueObjects;
using System.Collections.Generic;

namespace AlgoDDD.SharedKernel.Domain.ValueObjects
{
    public class Price : ValueObject
    {
        public decimal Value { get; }
        public string Currency { get; }

        public Price(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }
    }
}