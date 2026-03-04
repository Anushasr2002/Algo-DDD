using AlgoDDD.SharedKernel.Domain.ValueObjects;  // This should match the namespace of ValueObject
using System.Collections.Generic;

namespace AlgoDDD.SharedKernel.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}