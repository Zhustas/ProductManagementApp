using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using ProductManagementApp.Messages;
using ProductManagementApp.ViewModels;

namespace ProductManagementApp;

public partial class MainWindow : Window
{
	public static readonly ProductListView ProductListView = new();

	public MainWindow()
	{
		InitializeComponent();
		WeakReferenceMessenger.Default.Register<MainWindow, ConfirmationMessage>(this, static (w, m) =>
		{
			DialogBox dialog = new()
			{
				DataContext = new DialogBoxViewModel()
			};
			m.Reply(dialog.ShowDialog<ConfirmationMessageResult>(w));
		});

		Content = ProductListView;
	}
}
