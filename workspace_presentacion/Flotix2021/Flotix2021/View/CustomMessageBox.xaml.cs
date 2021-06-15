using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Flotix2021.View
{

	public enum EnumPredefinedButtons
	{
		Ok,
		OkCancel,
		YesNo,
		YesNoCancel,
		No,
	}

	public enum EnumDialogResults
	{
		None = 0,
		Ok = 1,
		Cancel = 2,
		Yes = 3,
		No = 4,
		Button1 = 1,
		Button2 = 2,
		Button3 = 3,
	}

	public enum EnumLanguages
	{
		Spain = 0,
		English = 1,
	}

	/// <summary>
	/// Lógica de interacción para CustomMessageBox.xaml
	/// </summary>
	public partial class CustomMessageBox
	{

		private EnumDialogResults _customDialogResult;
		private System.Windows.Threading.DispatcherTimer _timerAutoClose;
		private System.Windows.Threading.DispatcherTimer _timerEnableButtonsAfter;

		public CustomMessageBox()
		{
			InitializeComponent();

			Closing += CustomMessagBox_Closing;
		}

		/// <summary>
		/// Ensure the timers are stopped before closing the window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CustomMessagBox_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (_timerEnableButtonsAfter != null)
			{
				_timerEnableButtonsAfter.Stop();
				_timerEnableButtonsAfter = null;
			}
			if (_timerAutoClose != null)
			{
				_timerAutoClose.Stop();
				_timerAutoClose = null;
			}

		}

		/// <summary>
		/// Property offering the caption of the Window (the Title)
		/// </summary>
		public string Caption
		{
			get { return TbCaption.Text; }
			set { TbCaption.Text = value; }
		}

		/// <summary>
		/// Property showing a header in the instructions section
		/// </summary>
		public string InstructionHeading
		{
			get { return TbInstructionHeading.Text; }
			set { TbInstructionHeading.Text = value; }
		}

		/// <summary>
		/// Property showing the instructions
		/// </summary>
		public string InstructionText
		{
			get { return TbInstructionText.Text; }
			set { TbInstructionText.Text = value; }
		}

		/// <summary>
		/// If you want the dialog to automatically shut down after x seconds
		/// </summary>
		public int AutoCloseDialogTime
		{
			set
			{
				_timerAutoClose = new System.Windows.Threading.DispatcherTimer();
				_timerAutoClose.Tick += TimerAutoCloseTick;
				_timerAutoClose.Interval = new TimeSpan(0, 0, 0, value);
				_timerAutoClose.Start();
			}
		}

		/// <summary>
		/// Buttons will be disabled at first and enable after the time elapsed
		/// </summary>
		public int EnableButtonsAfterTime
		{
			set
			{
				Button1.IsEnabled = false;
				Button2.IsEnabled = false;
				Button3.IsEnabled = false;

				_timerEnableButtonsAfter = new System.Windows.Threading.DispatcherTimer();
				_timerEnableButtonsAfter.Tick += _timerEnableButtonsAfter_Tick;
				_timerEnableButtonsAfter.Interval = new TimeSpan(0, 0, 0, value);
				_timerEnableButtonsAfter.Start();
			}
		}

		private void _timerEnableButtonsAfter_Tick(object sender, EventArgs e)
		{
			_timerEnableButtonsAfter.Stop();
			Button1.IsEnabled = true;
			Button2.IsEnabled = true;
			Button3.IsEnabled = true;
		}

		private void TimerAutoCloseTick(object sender, EventArgs e)
		{
			_timerAutoClose.Stop();
			DialogResult = false;
		}

		/// <summary>
		/// The result of the dialog (the button that was pressed)
		/// </summary>
		public EnumDialogResults CustomCustomDialogResult { get { return _customDialogResult; } }

		/// <summary>
		/// Set the buttons to predefined values
		/// </summary>
		/// <param name="buttons"></param>
		public void SetButtonsPredefined(EnumPredefinedButtons buttons)
		{
			SetButtonsPredefined(buttons, EnumLanguages.Spain);
		}
		public void SetButtonsPredefined(EnumPredefinedButtons buttons, EnumLanguages language)
		{
			Button1.Visibility = Visibility.Collapsed;
			Button1.Tag = EnumDialogResults.None;
			Button2.Visibility = Visibility.Collapsed;
			Button2.Tag = EnumDialogResults.None;
			Button3.Visibility = Visibility.Collapsed;
			Button3.Tag = EnumDialogResults.None;

			switch (buttons)
			{
				case EnumPredefinedButtons.Ok:
					Button1.Visibility = Visibility.Visible;
					Button1.Content = "Aceptar";
					Button1.Tag = EnumDialogResults.Ok;
					break;
				case EnumPredefinedButtons.OkCancel:
					Button1.Visibility = Visibility.Visible;
					Button1.Content = language == EnumLanguages.Spain ? "Cancelar" : "Cancel";
					Button1.Tag = EnumDialogResults.Cancel;

					Button2.Visibility = Visibility.Visible;
					Button2.Content = "Aceptar";
					Button2.Tag = EnumDialogResults.Ok;
					break;
				case EnumPredefinedButtons.YesNo:
					Button1.Visibility = Visibility.Visible;
					Button1.Content = language == EnumLanguages.Spain ? "No" : "No";
					Button1.Tag = EnumDialogResults.No;

					Button2.Visibility = Visibility.Visible;
					Button2.Content = language == EnumLanguages.Spain ? "Sí" : "Yes";
					Button2.Tag = EnumDialogResults.Yes;
					break;
				case EnumPredefinedButtons.YesNoCancel:
					Button1.Visibility = Visibility.Visible;
					Button1.Content = language == EnumLanguages.Spain ? "Cancelar" : "Cancel";
					Button1.Tag = EnumDialogResults.Cancel;

					Button2.Visibility = Visibility.Visible;
					Button2.Content = language == EnumLanguages.Spain ? "No" : "No";
					Button2.Tag = EnumDialogResults.No;

					Button3.Visibility = Visibility.Visible;
					Button3.Content = language == EnumLanguages.Spain ? "Sí" : "Yes";
					Button3.Tag = EnumDialogResults.Yes;
					break;
				case EnumPredefinedButtons.No:
					break;
			}
		}


		/// <summary>
		/// Set custom buttons
		/// </summary>
		/// <param name="captionLeft"></param>
		/// <param name="captionMiddle"></param>
		/// <param name="captionRight"></param>
		/// <param name="resultLeft"></param>
		/// <param name="resultMiddle"></param>
		/// <param name="resultRight"></param>
		public void SetButtonsCustoms(string captionLeft, string captionMiddle, string captionRight,
									  EnumDialogResults resultLeft, EnumDialogResults resultMiddle, EnumDialogResults resultRight)
		{
			Button1.Visibility = Visibility.Collapsed;
			Button1.Tag = EnumDialogResults.None;
			Button2.Visibility = Visibility.Collapsed;
			Button2.Tag = EnumDialogResults.None;
			Button3.Visibility = Visibility.Collapsed;
			Button3.Tag = EnumDialogResults.None;

			if (!string.IsNullOrWhiteSpace(captionRight))
			{
				Button1.Visibility = Visibility.Visible;
				Button1.Content = captionRight;
				Button1.Tag = resultRight;
			}

			if (!string.IsNullOrWhiteSpace(captionMiddle))
			{
				Button2.Visibility = Visibility.Visible;
				Button2.Content = captionMiddle;
				Button2.Tag = resultMiddle;
			}

			if (!string.IsNullOrWhiteSpace(captionLeft))
			{
				Button3.Visibility = Visibility.Visible;
				Button3.Content = captionLeft;
				Button3.Tag = resultLeft;
			}
		}

		#region -- Button click events --

		//When a button is clicked, we need to set the custom dialogresult and set the realDialogResult to True to close the dialog
		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			_customDialogResult = (EnumDialogResults)Button1.Tag;
			DialogResult = true;
		}

		private void Button2_Click(object sender, RoutedEventArgs e)
		{
			_customDialogResult = (EnumDialogResults)Button2.Tag;
			DialogResult = true;
		}

		private void Button3_Click(object sender, RoutedEventArgs e)
		{
			_customDialogResult = (EnumDialogResults)Button3.Tag;
			DialogResult = true;
		}

		#endregion // -- Button click events --

		private void CustomMessagBox_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			//otherwise the messagebox is un-movable
			DragMove();
		}
	}
}
