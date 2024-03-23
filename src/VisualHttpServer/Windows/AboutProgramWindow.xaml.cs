using System.Windows;

namespace VisualHttpServer.Windows
{
	/// <summary>
	/// Interaction logic for AboutProgramWindow.xaml
	/// </summary>
	public partial class AboutProgramWindow : Window
	{
		public AboutProgramWindow()
		{
			InitializeComponent();
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}