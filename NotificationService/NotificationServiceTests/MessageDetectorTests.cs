using System;
using System.Collections.Generic;
using System.Text.Json;
using NotificationService.Services;
using NUnit.Framework;
using FluentAssertions;
using NotificationLib.Message;

namespace NotificationServiceTests
{
    public class MessageDetectorTests
    {

        [Test]
        [TestCaseSource(nameof(_correctCases))]
        public void CorrectJson_Should_Be_Detected(string json, Type expectedType)
        {
            var rootElement = JsonDocument.Parse(json).RootElement;
            var messageType = NotificationTypeDetector.DetectNotificationType(rootElement);
            messageType.Should().Be(expectedType);
        }

        [Test]
        [TestCaseSource(nameof(_incorrectCases))]
        public void IncorrectJson_Should_Be_Thrown(string json)
        {
            var rootElement = JsonDocument.Parse(json).RootElement;
            Action call = () => NotificationTypeDetector.DetectNotificationType(rootElement);
            call.Should().Throw<UnknownMessageTypeException>();
        }


        private static IEnumerable<object[]> _correctCases = new[]
        {
            new object[]
            {
                @"{
                ""pushToken"": ""string"",
                ""alert"": ""string"",
                ""priority"": 0,
                ""isBackground"": true
            }",
                typeof(IosNotificationMessage)
            },
            new object[]
            {
                @"{
                ""deviceToken"": ""string"",
                ""message"": ""string"",
                ""title"": ""string"",
                ""condition"": ""string""
            }",
                typeof(AndroidNotificationMessage)
            }
        };

        private static IEnumerable<object[]> _incorrectCases = new[]
        {
            new object[]
            {
                @"{
                ""wrongToken"": ""string"",
                ""alert"": ""string"",
                ""priority"": 0,
                ""isBackground"": true
                }", 
            },
            new object[]
            {
                "{}"
            }
        };

    }
}