using AutoFixture;
using FluentAssertions;
using System;
using Vrnz2.TestUtils.Exceptions;
using Vrnz2.TestUtils.Extensions;
using Xunit;

namespace Vrnz2.TestUtils.Tests.Extensions
{
    public enum TCriteriaData
    {
        Undefined = 0,

        String = 1,
        Integer = 2,
        DateTime = 3,
        Decimal = 4,
        Combo = 5,
        DualListBox = 6,
        Memo = 7,
        PeriodNumber = 8,
        PeriodDate = 9,
        NegativeInteger = 10,
        NumberRange = 11,
        Boolean = 12,
        Float = 13,
        Double = 14,
        TreeView = 15,
        RadioGroup = 16
    }

    public enum TConfig
    {
        Undefined = 0,

        String = 1,
        Integer = 2,
        DateTime = 3,
        Decimal = 4,
        Combo = 5,
        DualListBox = 6,
        Memo = 7,
        PeriodNumber = 8,
        PeriodDate = 9,
        NegativeInteger = 10,
        NumberRange = 11,
        Boolean = 12,
        Float = 13,
        Double = 14,
        Time = 15,
        ComplexString = 16,
        IntegerAndRadio = 17
    }

    public enum TCriteria
    {
        Undefined = 0,
        QuotationFinalized = 1,
    }

    public class FixtureExtensions
        : AbstractTest
    {
        [Fact]
        public void CreateEnum_When_GettingTCriteriaDataValue_Should_Be_ValueReturn()
        {
            // Arrange

            // Act
            var response = Fixture.CreateEnum<TCriteriaData>();

            // Assert
            response.Should().Be(response);
        }

        [Fact]
        public void CreateEnum_When_GettingTCriteriaDataValueWithExceptList_Should_Be_ValueReturn()
        {
            // Arrange

            // Act
            var response = Fixture.CreateEnum(new TCriteriaData[] { TCriteriaData.Undefined });

            // Assert
            response.Should().Be(response);
        }

        [Fact]
        public void CreateEnum_When_GettingTConfigValue_Should_Be_ValueReturn()
        {
            // Arrange

            // Act
            var response = Fixture.CreateEnum<TConfig>();

            // Assert
            response.Should().Be(response);
        }

        [Fact]
        public void CreateEnum_When_GettingTConfigValueWithExceptList_Should_Be_ValueReturn()
        {
            // Arrange

            // Act
            var response = Fixture.CreateEnum(new TCriteria[] { TCriteria.Undefined });

            // Assert
            response.Should().Be(response);
        }

        [Fact]
        public void CreateEnum_When_GettingTCriteriaValue_Should_Be_ValueReturn()
        {
            // Arrange

            // Act
            var response = Fixture.CreateEnum<TConfig>();

            // Assert
            response.Should().Be(response);
        }

        [Fact]
        public void CreateEnum_When_GettingTCriteriaValueWithExceptList_Should_Be_ValueReturn()
        {
            // Arrange

            // Act
            var response = Fixture.CreateEnum(new TCriteria[] { TCriteria.Undefined });

            // Assert
            response.Should().Be(response);
        }

        [Fact]
        public void CreateEnum_When_GettingTCriteriaValueWithExceptListContaoningAllElementsOfEnum_Should_Be_Error()
        {
            // Arrange

            // Act
            Action act = () => Fixture.CreateEnum(new TCriteria[] { TCriteria.Undefined, TCriteria.QuotationFinalized });

            // Assert
            act.Should().ThrowExactly<EnumValueNotFound>();
        }
    }
}
