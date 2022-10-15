using Application.Interfaces.Repositories;
using Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel;
using MediatR;

namespace Application.Features.Commands.EntryCommentCommand.Delete
{
    public class DeleteEntryCommentCommandHandler : IRequestHandler<DeleteEntryCommentCommand, bool>
    {
        private readonly IEntryCommentRepository entryCommentRepository;

        public DeleteEntryCommentCommandHandler(IEntryCommentRepository entryCommentRepository)
        {
            this.entryCommentRepository = entryCommentRepository;
        }

        public async Task<bool> Handle(DeleteEntryCommentCommand request, CancellationToken cancellationToken)
        {
            var entryComment = await entryCommentRepository.GetFirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if(entryComment is null)
                return false;

            entryComment.Deleted = true;

            await entryCommentRepository.UpdateAsync(entryComment);

            return true;
        }
    }
}
