using MediatR;

namespace Common.Models.RequestModels.UserRequestModels.ConfirmEmailRequestModel
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
