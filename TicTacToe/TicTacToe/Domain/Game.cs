using System.ComponentModel.Design;
using Stateless;
using TicTacToe.Exceptions;

namespace TicTacToe.Domain
{
    public class Game
    {
        public enum Sign
        {
            Tic,
            Tac
        }

        public enum Trigger
        {
            Tic,
            Tac,
            TicWin,
            TacWin,
            Finished
        }

        public enum State
        {
            New,
            Tic,
            Tac,
            TicWin,
            TacWin,
            Finished
        }

        private readonly Sign?[,] _board = new Sign?[3, 3];
        private State _state = State.New;

        StateMachine<State, Trigger> _machine;

        private StateMachine<State, Trigger>.TriggerWithParameters _ticSignTrigger;
        private StateMachine<State, Trigger>.TriggerWithParameters _tacSignTrigger;
        private StateMachine<State, Trigger>.TriggerWithParameters _tacWinTrigger;
        private StateMachine<State, Trigger>.TriggerWithParameters _ticWinTrigger;

        public Game()
        {
            _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);

            _ticSignTrigger = _machine.SetTriggerParameters(Trigger.Tic);
            _tacSignTrigger = _machine.SetTriggerParameters(Trigger.Tac);
            _tacWinTrigger = _machine.SetTriggerParameters(Trigger.TacWin);
            _ticWinTrigger = _machine.SetTriggerParameters(Trigger.TicWin);

            _machine.Configure(State.New)
                .Permit(Trigger.Tac, State.Tac)
                .Permit(Trigger.Tic, State.Tic);

            _machine.Configure(State.Tac)
                .Permit(Trigger.Tic, State.Tic)
                .Permit(Trigger.TacWin, State.TacWin)
                .Permit(Trigger.Finished, State.Finished);

            _machine.Configure(State.Tic)
                .Permit(Trigger.Tac, State.Tac)
                .Permit(Trigger.Finished, State.Finished)
                .Permit(Trigger.TicWin, State.TicWin);
        }

        public void SetTac(int horizontalPosition, int verticalPosition)
        {
            SetSign(Sign.Tac, horizontalPosition, verticalPosition);
            _machine.Fire(_tacSignTrigger);
            if (CheckIsFinished(Sign.Tac))
            {
                _machine.Fire(_tacWinTrigger);
            }
        }

        public void SetTic(int horizontalPosition, int verticalPosition)
        {
            SetSign(Sign.Tic, horizontalPosition, verticalPosition);
            _machine.Fire(_ticSignTrigger);
            if (CheckIsFinished(Sign.Tic))
            {
                _machine.Fire(_ticWinTrigger);
            }
        }


        private void SetSign(Sign sign, int horizontalPosition, int verticalPosition)
        {
            if (_board[horizontalPosition, verticalPosition] != null)
                throw new NonEmptyFieldException();

            _board[horizontalPosition, verticalPosition] = sign;
        }

        private bool CheckIsFinished(Sign sign)
        {
            for (int i = 0; i < 3; i++)
            {
                if (_board[i, 0] == sign)
                {
                    if (_board[i, 1] == sign)
                    {
                        if (_board[i, 2] == sign)
                        {
                            return true;
                        }
                    }
                }
                
                if (_board[0, i] == sign)
                {
                    if (_board[1, i] == sign)
                    {
                        if (_board[2, i] == sign)
                        {
                            return true;
                        }
                    }
                }
            }
            
            if (_board[0,0] == sign)
            {
                if (_board[1,1] == sign)
                {
                    if (_board[2,2] == sign)
                    {
                        return true;
                    }
                }
            }
            
            if (_board[0,2] == sign)
            {
                if (_board[1,1] == sign)
                {
                    if (_board[2,0] == sign)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Sign?[,] GetBoard()
        {
            return _board;
        }

        public State GetState()
        {
            return _state;
        }
    }
}