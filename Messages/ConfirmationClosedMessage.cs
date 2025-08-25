namespace ProductManagementApp.Messages;

public class ConfirmationClosedMessage(ConfirmationMessageResult confirmationMessageResult)
{
    public ConfirmationMessageResult Result { get; } = confirmationMessageResult;
}
