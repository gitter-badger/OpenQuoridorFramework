﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Lib.Wpf.ViewModelBase;
using OQF.PlayerVsBot.ViewModels.Board;
using OQF.PlayerVsBot.ViewModels.BoardPlacement;
using OQF.PlayerVsBot.ViewModels.MainWindow.Helper;
using OQF.Visualization.Common.Language.LanguageSelection.ViewModel;

namespace OQF.PlayerVsBot.ViewModels.MainWindow
{
	internal interface IMainWindowViewModel : IViewModel
	{
		IBoardViewModel BoardViewModel { get; }
		IBoardPlacementViewModel BoardPlacementViewModel { get; }
		ILanguageSelectionViewModel LanguageSelectionViewModel { get; }

		ICommand Start         { get; }		
		ICommand ShowAboutHelp { get; }
		ICommand Capitulate    { get; }		
		ICommand BrowseDll     { get; }

		ObservableCollection<string> DebugMessages { get; } 
		ObservableCollection<string> GameProgress  { get; }						

		bool IsAutoScrollProgressActive { get; set; }
		bool IsAutoScrollDebugMsgActive { get; set; }

		bool IsDisabledOverlayVisible { get; }

		GameStatus GameStatus { get; }

		string TopPlayerName    { get; } 	
		string TopPlayerRestTime { get; }		

		int TopPlayerWallCountLeft    { get; }
		int BottomPlayerWallCountLeft { get; }
		
		string DllPathInput { get; set; }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		////////                                                                                                 ////////
		////////                                          Captions                                               ////////
		////////                                                                                                 ////////
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////		
		
		string BrowseForBotButtonToolTipCaption { get; }
		string StartGameButtonToolTipCaption    { get; }
		string OpenInfoButtonToolTipCaption     { get; }
		string BotNameLabelCaption              { get; }
		string MaximalThinkingTimeLabelCaption  { get; }
		string WallsLeftLabelCaption            { get; }
		string ProgressCaption                  { get; }
		string AutoScrollDownCheckBoxCaption    { get; }
		string DebugCaption                     { get; }
		string CapitulateButtonCaption          { get; }
		string HeaderCaptionPlayer              { get; }
	}
}
