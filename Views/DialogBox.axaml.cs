using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using ProductManagementApp.Messages;
using ProductManagementApp.ViewModels;

namespace ProductManagementApp;

public partial class DialogBox : Window
{
    public DialogBox()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<DialogBox, ConfirmationClosedMessage>(this, static (window, message) =>
        {
            window.Close(message.Result);
        });
    }

    public void OnCancelClick(object? sender, RoutedEventArgs args)
    {
		DialogBoxViewModel.SendResult(ConfirmationMessageResult.Cancel);
    }

    public void OnConfirmClick(object? sender, RoutedEventArgs args)
    {
		DialogBoxViewModel.SendResult(ConfirmationMessageResult.Confirm);
    }
}
