using Application.Interfaces.Repositories;
using Common.Models.RequestModels.TagRequestModels.DeleteRequestModels;
using MediatR;

namespace Application.Features.Commands.TagCommand.Delete
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, bool>
    {
        private readonly ITagRepository tagRepository;

        public DeleteTagCommandHandler(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.GetFirstOrDefaultAsync(x => x.Id == request.Id);

            if(tag is null)
                return false;

            tag.Deleted = true;

            var result = await tagRepository.UpdateAsync(tag);

            return result != 0;
        }
    }
}
