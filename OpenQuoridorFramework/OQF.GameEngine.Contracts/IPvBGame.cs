using System;
using OQF.Bot.Contracts.GameElements;
using OQF.Bot.Contracts.Moves;

namespace OQF.GameEngine.Contracts
{
	public interface IPvBGame
	{
		event Action<Player, WinningReason, Move> WinnerAvailable;
		event Action<BoardState> NextBoardstateAvailable;
		event Action<string> DebugMessageAvailable;

		void ReportHumanMove (Move move);
		
		void StopGame();
	}
}