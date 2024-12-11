using Swack.Logic;
using Swack.Logic.Models;
using Swack.UI.ViewModels;
using System.Windows;

namespace Swack.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var user = new User("andi", "https://robohash.org/andi");
        var messagingLogic = new SimulatedMessagingLogic(user);

        var viewModel = new MainViewModel(messagingLogic);
        DataContext = viewModel;

        Loaded += async (sender, eventArgs) => await viewModel.InitializeAsync();
    }
}