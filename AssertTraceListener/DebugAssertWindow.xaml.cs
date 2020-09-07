using System.Windows;

namespace AssertTraceListener
{
    /// <summary>
    /// Interaction logic for DebugAssertWindow.xaml
    /// </summary>
    internal partial class DebugAssertWindow : Window
    {
        internal DebugAssertWindow(AssertionViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        internal DialogAction Action { get; private set; }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Action = DialogAction.Quit;
            Close();
        }

        private void DebugButton_Click(object sender, RoutedEventArgs e)
        {
            Action = DialogAction.Debug;
            Close();
        }

        private void IgnoreButton_Click(object sender, RoutedEventArgs e)
        {
            Action = DialogAction.Ignore;
            Close();
        }
    }
}
