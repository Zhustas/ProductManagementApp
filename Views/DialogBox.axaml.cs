using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using ProductManagementApp.Messages;
using ProductManagementApp.ViewModels;
using System.Diagnostics;

namespace ProductManagementApp;

public partial class DialogBox : Window
{
    private DialogBoxViewModel viewModel = new();

    public DialogBox()
    {
        InitializeComponent();

        DataContext = viewModel;
        WeakReferenceMessenger.Default.Register<DialogBox, ConfirmationClosedMessage>(this, static (window, message) =>
        {
            window.Close(message.Result);
        });
    }

    public void OnCancelClick(object? sender, RoutedEventArgs args)
    {
        viewModel.SendResult(ConfirmationMessageResult.Cancel);
    }

    public void OnConfirmClick(object? sender, RoutedEventArgs args)
    {
        viewModel.SendResult(ConfirmationMessageResult.Confirm);
    }
}
