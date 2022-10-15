using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.RequestModels.EntryCommentRequestModels.UpdateRequestModels;
using MediatR;

namespace Application.Features.Commands.EntryCommentCommand.Update
{
    public class UpdateEntryCommentCommandHandler : IRequestHandler<UpdateEntryCommentCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IEntryCommentRepository entryCommentRepository;

        public UpdateEntryCommentCommandHandler(IMapper mapper, IEntryCommentRepository entryCommentRepository)
        {
            this.mapper = mapper;
            this.entryCommentRepository = entryCommentRepository;
        }

        public async Task<bool> Handle(UpdateEntryCommentCommand request, CancellationToken cancellationToken)
        {
            var entryComment = await entryCommentRepository.GetFirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if(entryComment is null)
                return false;

            mapper.Map(request, entryComment);

            var result = await entryCommentRepository.UpdateAsync(entryComment);

            return result != 0;
        }
    }
}
