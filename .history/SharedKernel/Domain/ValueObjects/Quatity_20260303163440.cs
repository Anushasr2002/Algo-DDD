using AlgoDDD.SharedKernel.Domain.ValueObjects;
using System.Collections.Generic;

namespace AlgoDDD.SharedKernel.Domain.ValueObjects
{
    public class Quantity : ValueObject
    {
        public decimal Value { get; }
        public string Unit { get; }

        public Quantity(decimal value, string unit)
        {
            Value = value;
            Unit = unit;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Unit;
        }
    }
}