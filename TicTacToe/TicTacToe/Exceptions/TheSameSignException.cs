using System;

namespace TicTacToe.Exceptions
{
    public class TheSameSignException : Exception
    {
        public TheSameSignException() : base("TheSameSignException")
        {
        }
    }
}