using Bowling.Api;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Bowling.Test
{
    public class ScoreCalculatorShould
    {
        private ICalculatorService _service;
        private readonly ILogger<CalculatorService> _logger;

        public ScoreCalculatorShould()
        {
            _logger = Substitute.For<ILogger<CalculatorService>>();
            _service = new CalculatorService(_logger);
        }


        [Theory]
        [MemberData(nameof(TestDates.TestData), MemberType = typeof(TestDates))]
        public void ReturnCorrectGameDateWhenInputIsValid(List<int> pinsDowned, List<string> score, bool IsGameCompleted)
        {

            var scoreResponse = new ScoreResponse();

            var result = _service.CalculateScore(pinsDowned, scoreResponse);

            Assert.True(result);
            Assert.Equal(score, scoreResponse.FrameProgressScores);
            Assert.Equal(IsGameCompleted, scoreResponse.IsGameCompleted);
        }

        public static class TestDates
        {
            private static readonly List<object[]> _data = new List<object[]>
            {
                new object[] { new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, new List<string>() { "30", "60", "90", "120", "150", "180", "210", "240", "270", "300" }, true},
                new object[] { new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new List<string>() { "2", "4", "6", "8", "10", "12" }, false},
                new object[] { new List<int>() { 1, 1, 1, 1, 9, 1, 2, 8, 9, 1, 10, 10 }, new List<string>() { "2", "4", "16", "35", "55", "*", "*" }, false},
                new object[] { new List<int>() { 10, 9, 1, 4, 3, 10, 4, 6, 3, 2, 5, 3, 8, 2, 1, 1, 10, 10, 10 }, new List<string>() { "20", "34", "41", "61", "74", "79", "87", "98", "100", "130" }, true},
                new object[] { new List<int>() { 10 }, new List<string>() { "*" }, false},
            };

            public static IEnumerable<object[]> TestData
            {
                get { return _data; }
            }
        }

        [Fact]
        public void ReturnFalseWhenInputIsNull()
        {
            List<int> pinsDowned = null;
            var scoreResponse = new ScoreResponse();

            var result = _service.CalculateScore(pinsDowned, scoreResponse);

            Assert.False(result);
        }

        [Fact]
        public void ReturnFalseWhenInputIsEmpty()
        {
            List<int> pinsDowned = new List<int>();
            var scoreResponse = new ScoreResponse();

            var result = _service.CalculateScore(pinsDowned, scoreResponse);

            Assert.False(result);
        }

        [Fact]
        public void ReturnFalseWhenInputHasTooManyElement()
        {
            List<int> pinsDowned = new List<int>() { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 };
            var scoreResponse = new ScoreResponse();

            var result = _service.CalculateScore(pinsDowned, scoreResponse);

            Assert.False(result);
        }

        [Fact]
        public void ReturnFalseWhenInputContainIncorrectPinDowedNumber()
        {
            List<int> pinsDowned = new List<int>() { 6, 6 };
            var scoreResponse = new ScoreResponse();

            var result = _service.CalculateScore(pinsDowned, scoreResponse);

            Assert.False(result);
        }
    }
}
