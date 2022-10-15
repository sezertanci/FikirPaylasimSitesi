using MediatR;

namespace Common.Models.RequestModels.TagRequestModels.DeleteRequestModels
{
    public class DeleteTagCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteTagCommand()
        {

        }

        public DeleteTagCommand(Guid id)
        {
            Id = id;
        }
    }
}
