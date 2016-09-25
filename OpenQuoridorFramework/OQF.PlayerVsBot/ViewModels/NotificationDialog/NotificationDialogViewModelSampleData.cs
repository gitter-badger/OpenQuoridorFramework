﻿using System.ComponentModel;

#pragma warning disable 0067

namespace OQF.PlayerVsBot.ViewModels.NotificationDialog
{
	internal class NotificationDialogViewModelSampleData : INotificationDialogViewModel
	{
		public NotificationDialogViewModelSampleData()
		{			
			Message = "example-message";
			ButtonCaption = "ok";
		}
		
		public string Message       { get; }
		public string ButtonCaption { get; }

		public void Dispose() {}
		public event PropertyChangedEventHandler PropertyChanged;		
	}
}