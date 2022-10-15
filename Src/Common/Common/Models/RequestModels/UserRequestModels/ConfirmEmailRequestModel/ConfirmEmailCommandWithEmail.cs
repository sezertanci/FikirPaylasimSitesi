using MediatR;

namespace Common.Models.RequestModels.UserRequestModels.ConfirmEmailRequestModel
{
    public class ConfirmEmailCommandWithEmail : IRequest<bool>
    {
        public string NewEmailAddress { get; set; }
    }
}
