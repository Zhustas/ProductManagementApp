using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductManagementApp.Messages;

namespace ProductManagementApp.ViewModels;

public partial class DialogBoxViewModel
{
    [RelayCommand]
    public static void SendResult(ConfirmationMessageResult result)
    {
        WeakReferenceMessenger.Default.Send(new ConfirmationClosedMessage(result));
    }
}
