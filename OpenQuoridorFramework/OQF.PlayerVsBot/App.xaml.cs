﻿using System.Globalization;
using System.Windows;
using OQF.GameEngine.Contracts.Factories;
using OQF.GameEngine.Factories;
using OQF.PlayerVsBot.Services;
using OQF.PlayerVsBot.Services.SettingsRepository;
using OQF.PlayerVsBot.ViewModels.Board;
using OQF.PlayerVsBot.ViewModels.BoardPlacement;
using OQF.PlayerVsBot.ViewModels.MainWindow;
using OQF.PlayerVsBot.Windows;
using OQF.Visualization.Common.Language;
using OQF.Visualization.Common.Language.LanguageSelection.ViewModel;

namespace OQF.PlayerVsBot
{
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{			
			base.OnStartup(e);

			IGameFactory gameFactory = new GameFactory();
			IGameService gameService = new GameService(gameFactory);
			IApplicationSettingsRepository applicationSettingsRepository = new ApplicationSettingsRepository();

			CultureManager.CurrentCulture = new CultureInfo(applicationSettingsRepository.SelectedLanguageCode);
			CultureManager.CultureChanged += () =>
											 {
											 	applicationSettingsRepository.SelectedLanguageCode = CultureManager.CurrentCulture.ToString();
											 };

			var boardViewModel = new BoardViewModel(gameService);
			var boardPlacementViewModel = new BoardPlacementViewModel(gameService, gameFactory);
			var languageSelectionViewModel = new LanguageSelectionViewModel();
			var progressFileVerifierFactory = new ProgressFileVerifierFactory();

			var mainWindowViewModel = new MainWindowViewModel(boardViewModel, 
															  boardPlacementViewModel, 
															  languageSelectionViewModel,
															  gameService, 
															  applicationSettingsRepository,
															  progressFileVerifierFactory);

			var commandLineArguments = CommandLine.Parse(e.Args);

			if (!string.IsNullOrWhiteSpace(commandLineArguments.BotPath))
			{
				var dllPath = e.Args[0];
				mainWindowViewModel.DllPathInput = dllPath;

				if (mainWindowViewModel.Start.CanExecute(null))
					mainWindowViewModel.Start.Execute(null);
			}			

			var mainWindow = new MainWindow
			{
				DataContext = mainWindowViewModel
			};
			
			mainWindow.ShowDialog();

			gameService.StopGame();			
		}		
	}
}
