using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApp.Messages;

public class ConfirmationClosedMessage(ConfirmationMessageResult confirmationMessageResult)
{
    public ConfirmationMessageResult Result { get; } = confirmationMessageResult;
}
