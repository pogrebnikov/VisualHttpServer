using System.Windows;

namespace VisualHttpServer.Windows;

/// <summary>
/// Interaction logic for EditRouteWindow.xaml
/// </summary>
public partial class EditRouteWindow : Window
{
    public EditRouteWindow()
    {
        InitializeComponent();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}