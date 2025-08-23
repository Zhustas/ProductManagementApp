using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ProductManagementApp.ViewModels;

namespace ProductManagementApp.Messages;

public class ConfirmationMessage : AsyncRequestMessage<ConfirmationMessageResult>
{

}

public enum ConfirmationMessageResult
{
    Cancel,
    Confirm
}
