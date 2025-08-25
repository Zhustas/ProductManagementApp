using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ProductManagementApp.Messages;

public class ConfirmationMessage : AsyncRequestMessage<ConfirmationMessageResult>
{

}

public enum ConfirmationMessageResult
{
    Cancel,
    Confirm
}
