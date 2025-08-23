using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductManagementApp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApp.ViewModels;

public partial class DialogBoxViewModel
{
    [RelayCommand]
    public void SendResult(ConfirmationMessageResult result)
    {
        WeakReferenceMessenger.Default.Send(new ConfirmationClosedMessage(result));
    }
}
