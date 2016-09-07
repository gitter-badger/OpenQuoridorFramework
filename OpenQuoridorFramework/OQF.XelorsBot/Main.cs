﻿using System;
using OQF.Contest.Contracts;
using OQF.Contest.Contracts.GameElements;
using OQF.Contest.Contracts.Moves;

namespace OQF.XelorsBot
{
	public class Main : IQuoridorBot
    {
	    public event Action<Move> NextMoveAvailable;
	    public event Action<string> DebugMessageAvailable;

	    private Player mySelf;

	    public void Init(Player yourPlayer)
	    {
		    mySelf = yourPlayer;
	    }

	    public void DoMove(BoardState currentState)
	    {
			DebugMessageAvailable?.Invoke("");
		    NextMoveAvailable?.Invoke(new Capitulation(currentState, mySelf));
	    }
    }
}