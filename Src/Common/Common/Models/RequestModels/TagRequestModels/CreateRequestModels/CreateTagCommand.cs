using MediatR;

namespace Common.Models.RequestModels.TagRequestModels.CreateRequestModels
{
    public class CreateTagCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public CreateTagCommand()
        {

        }

        public CreateTagCommand(string name)
        {
            Name = name;
        }
    }
}
