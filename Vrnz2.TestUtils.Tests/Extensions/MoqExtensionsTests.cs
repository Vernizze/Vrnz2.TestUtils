﻿using Moq;
using System.Collections;
using System.Collections.Generic;
using Vrnz2.TestUtils.Extensions;
using Xunit;

namespace Vrnz2.TestUtils.Tests.Extensions
{
    public class MoqExtensionsTests
    {
        [Fact]
        public void SetupIgnoreArgs_action_should_ignore_arguments()
        {
            var mock = new Mock<IDummy>();
            mock
                .SetupIgnoreArgs(x => x
                    .Execute(null, null, null))
                .Verifiable();

            mock.Object.Execute("dummy input",
                new[] { "dummy value" },
                new Dictionary<string, object>
                {
                    {"dummy key", new object()},
                });

            mock.Verify();
        }

        [Fact]
        public void SetupIgnoreArgs_function_should_ignore_arguments()
        {
            var mock = new Mock<IDummy>();
            mock
                .SetupIgnoreArgs(x => x
                    .Get(null, null, null))
                .Verifiable();

            mock.Object.Get("dummy input",
                new[] { "dummy value" },
                new Dictionary<string, object>
                {
                    {"dummy key", new object()},
                });

            mock.Verify();
        }

        [Fact]
        public void VerifyIgnoreArgs_action_should_ignore_arguments()
        {
            var mock = new Mock<IDummy>();

            mock.Object.Execute("dummy input",
                new[] { "dummy value" },
                new Dictionary<string, object>
                {
                    {"dummy key", new object()},
                });

            mock.VerifyIgnoreArgs(x => x
                .Execute(null, null, null));
        }

        [Fact]
        public void VerifyIgnoreArgs_function_should_ignore_arguments()
        {
            var mock = new Mock<IDummy>();

            mock.Object.Get("dummy input",
                new[] { "dummy value" },
                new Dictionary<string, object>
                {
                    {"dummy key", new object()},
                });

            mock.VerifyIgnoreArgs(x => x
                .Get(null, null, null));
        }

        public interface IDummy
        {
            void Execute(string input, IList values,
                IDictionary<string, object> settings);

            string Get(string input, IList values,
                IDictionary<string, object> settings);
        }
    }
}
