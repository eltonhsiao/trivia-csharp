using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using System;
using System.IO;

namespace Trivia
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    internal class GameTest
    {
        [Test]
        public void TwoPlayerWithInOutPenaltyBox()
        {
            var writer = new StringWriter();
            Console.SetOut(writer);

            var game = new Game();
            game.add("John");
            game.add("Mary");

            game.roll(5);
            game.wasCorrectlyAnswered();

            // get into penalty box
            game.roll(4);
            game.wrongAnswer();

            game.roll(4);
            game.wasCorrectlyAnswered();

            // not getting out of penalty box
            game.roll(2);
            game.wasCorrectlyAnswered();

            game.roll(6);
            game.wasCorrectlyAnswered();

            // odd number gets out of penalty box
            game.roll(9);
            game.wasCorrectlyAnswered();

            game.roll(2);
            game.wasCorrectlyAnswered();

            game.roll(1);
            game.wasCorrectlyAnswered();

            Approvals.Verify(writer.ToString());
        }
    }
}