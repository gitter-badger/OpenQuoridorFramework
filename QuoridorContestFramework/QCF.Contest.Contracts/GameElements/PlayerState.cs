﻿using QCF.Contest.Contracts.Coordination;

namespace QCF.Contest.Contracts.GameElements
{
	public class PlayerState
	{
	    public PlayerState(Player player, FieldCoordinate position, int wallsToPlace)
		{
			Player = player;
			Position = position;
			WallsToPlace = wallsToPlace;
		}

		public Player          Player       { get; }
		public FieldCoordinate Position     { get; }
		public int             WallsToPlace { get; }
	}
}