using MediatR;

namespace Common.Models.RequestModels.TagRequestModels.UpdateRequestModels
{
    public class UpdateTagCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public UpdateTagCommand()
        {

        }

        public UpdateTagCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
