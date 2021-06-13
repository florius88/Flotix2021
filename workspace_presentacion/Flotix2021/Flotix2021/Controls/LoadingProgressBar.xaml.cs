using System.Windows;
using System.Windows.Input;

namespace Flotix2021.Controls
{
    /// <summary>
    /// Lógica de interacción para LoadingProgressBar.xaml
    /// </summary>
    public partial class LoadingProgressBar
    {
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoadingProgressBar), new UIPropertyMetadata(false));

        public static readonly DependencyProperty ClosePanelCommandProperty =
            DependencyProperty.Register("ClosePanelCommand", typeof(ICommand), typeof(LoadingProgressBar));

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingProgressBar"/> class.
        /// </summary>
        public LoadingProgressBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the close panel command.
        /// </summary>
        /// <value>The close panel command.</value>
        public ICommand ClosePanelCommand
        {
            get { return (ICommand)GetValue(ClosePanelCommandProperty); }
            set { SetValue(ClosePanelCommandProperty, value); }
        }

        /// <summary>
        /// Called when [close click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            if (ClosePanelCommand != null)
            {
                ClosePanelCommand.Execute(null);
            }
        }
    }
}