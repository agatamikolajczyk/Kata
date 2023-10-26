using System;

namespace TicTacToe.Exceptions
{
    public class NonEmptyFieldException : Exception
    {
        public NonEmptyFieldException():base("NonEmptyFieldException")
        {
        }
    }
}