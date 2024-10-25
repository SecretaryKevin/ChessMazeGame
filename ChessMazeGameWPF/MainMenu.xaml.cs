using System.Windows;
using System.Windows.Controls;

namespace ChessMazeGameWPF;

public partial class MainMenu : Page
{
    public MainMenu()
    {
        InitializeComponent();
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void NextPageButton_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to the level
        NavigationService.Navigate(new GamePage());
    }
}