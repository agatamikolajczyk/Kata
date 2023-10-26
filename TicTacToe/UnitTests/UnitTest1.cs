using System;
using NUnit.Framework;
using Shouldly;
using TicTacToe.Domain;
using TicTacToe.Exceptions;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void if_circle_added_first_returns_success()
        {
            Game game = new Game();
            game.SetTic(2, 2);

            game.GetBoard()[2, 2].ShouldBe(Game.Sign.Tic);
            game.GetState().ShouldBe(Game.State.Tic);
        }

        [Test]
        public void if_set_sign_to_non_empty_field_should_return_exception()
        {
            Game game = new Game();
            game.SetTic(2, 2);

            Assert.Throws<NonEmptyFieldException>(() => game.SetTic(2, 2));
        }
        
        [Test]
        public void if_set_the_same_sign_two_times_should_return_exception()
        {
            Game game = new Game();
            game.SetTic(2, 2);

            Assert.Throws<InvalidOperationException>(() => game.SetTic(1, 1));
        }
        
        [Test]
        public void if_set_the_same_sign_in_horizontal_line_should_win()
        {
            Game game = new Game();
            game.SetTic(0, 0);
            game.SetTac(1, 0);
            game.SetTic(0, 1);
            game.SetTac(1, 1);
            game.SetTic(0, 2);

            game.GetState().ShouldBe(Game.State.TicWin);
        }
        
        [Test]
        public void if_set_the_same_sign_in_vertical_line_should_win()
        {
            Game game = new Game();
            game.SetTic(0, 0);
            game.SetTac(1, 0);
            game.SetTic(0, 1);
            game.SetTac(1, 1);
            game.SetTic(0, 2);

            game.GetState().ShouldBe(Game.State.TicWin);
        }
        
        [Test]
        public void if_set_the_same_sign_in_cross_line_should_win()
        {
            Game game = new Game();
            game.SetTic(0, 0);
            game.SetTac(1, 0);
            game.SetTic(1, 1);
            game.SetTac(1, 2);
            game.SetTic(2, 2);

            game.GetState().ShouldBe(Game.State.TicWin);
        }
        
        [Test]
        public void if_set_the_same_sign_in_cross_line2_should_win()
        {
            Game game = new Game();
            game.SetTic(0, 2);
            game.SetTac(1, 0);
            game.SetTic(1, 1);
            game.SetTac(1, 2);
            game.SetTic(2, 0);

            game.GetState().ShouldBe(Game.State.TicWin);
        }
    }
}