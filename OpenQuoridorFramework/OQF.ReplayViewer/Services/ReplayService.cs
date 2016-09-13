﻿using System;
using System.Collections.Generic;
using OQF.Contest.Contracts.Coordination;
using OQF.Contest.Contracts.GameElements;
using OQF.GameEngine.Transitions;
using OQF.Utils;

namespace OQF.ReplayViewer.Services
{
	internal class ReplayService : IReplayService
	{
		public event Action<BoardState> NewBoardStateAvailable;

		private BoardState currentBoardState;

		private IList<BoardState> allReplayStates;
		

		private int currentReplayIndex;

		public ReplayService()
		{
			currentBoardState = null;
		}
		

		public int NewReplay(IEnumerable<string> allMoves)
		{
			BuildReplayStates(allMoves);
			ShowReplayState(0);

			return allReplayStates.Count;
		}

		public void NextMove()                { ShowReplayState(currentReplayIndex+1); }
		public void PreviousMove()            { ShowReplayState(currentReplayIndex-1); }
		public void JumpToMove(int moveIndex) { ShowReplayState(moveIndex);            }

		public BoardState GetCurrentBoardState () => currentBoardState;

		private void ShowReplayState(int index)
		{
			currentReplayIndex = index;

			var nextState = allReplayStates[currentReplayIndex];

			currentBoardState = nextState;
			NewBoardStateAvailable?.Invoke(nextState);
		}

		private void BuildReplayStates(IEnumerable<string> allMoves)
		{
			allReplayStates = new List<BoardState>();


			var topPlayer = new Player(PlayerType.TopPlayer);
			var bottomPlayer = new Player(PlayerType.BottomPlayer);

			var boardState = BoardStateTransition.CreateInitialBoadState(topPlayer, bottomPlayer);
			allReplayStates.Add(boardState);

			var playerAtMove = bottomPlayer;

			foreach (var move in allMoves)
			{
				var nextMove = MoveParser.GetMove(move, boardState, playerAtMove);

				boardState = boardState.ApplyMove(nextMove);
				allReplayStates.Add(boardState);

				playerAtMove = playerAtMove == topPlayer ? bottomPlayer : topPlayer;
			}
		}
	}
}