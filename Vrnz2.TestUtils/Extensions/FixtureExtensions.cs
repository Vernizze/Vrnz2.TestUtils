using AutoFixture;
using System;
using System.Linq;
using Vrnz2.TestUtils.Exceptions;

namespace Vrnz2.TestUtils.Extensions
{
    public static class FixtureExtensions
    {
        public static TEnum CreateEnum<TEnum>(this Fixture fixture, TEnum[] exceptValue = null)
            where TEnum : Enum
        {
            bool exitWhile = false;
            var random = new Random();
            TEnum[] values = null;
            TEnum result = default;

            var possibleValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();

            values = possibleValues.Except(exceptValue ?? Enumerable.Empty<TEnum>().ToArray()).ToArray();

            do
            {
                if (values == null || (values != null && values.Length.Equals(0)))
                    throw new EnumValueNotFound($"Enum Name => '{typeof(TEnum).Name}' - Possible Values => '{(possibleValues != null ? string.Join(" - ", possibleValues.ToList().Select(v => v.ToString()).ToArray()) : "NULL")}' - Except List => '{(exceptValue != null ? string.Join(" - ", exceptValue.ToList().Select(v => v.ToString()).ToArray()) : "NULL")}'");

                var resultCandidateIndex = values.Length.Equals(1) ? 0 : random.Next(0, values.Length - 1);

                if (Enum.IsDefined(typeof(TEnum), values[resultCandidateIndex]))
                {
                    result = values[resultCandidateIndex];

                    exitWhile = true;
                }

            } while (!exitWhile);

            return result;
        }
    }
}
