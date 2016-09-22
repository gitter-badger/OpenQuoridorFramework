﻿using System.Globalization;
using System.Windows;
using Lib.Wpf.ConverterBase;
using OQF.Bot.Contracts.GameElements;

namespace OQF.PlayerVsBot.Computation
{
	internal class ComputeFigureLeftPosition : GenericTwoToOneValueConverter<PlayerState, Size, double>
	{
		protected override double Convert(PlayerState currentPlayerState, Size currentBoadSize, CultureInfo culture)
		{
			var cellWidth = currentBoadSize.Width / 11.4;
						
			return (double)currentPlayerState.Position.XCoord * 1.3 * cellWidth + 1;
		}
	}
}