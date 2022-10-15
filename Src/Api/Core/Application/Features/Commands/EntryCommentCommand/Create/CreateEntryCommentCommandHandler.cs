using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Features.Commands.EntryCommentCommand.Create
{
    public class CreateEntryCommentCommandHandler : IRequestHandler<CreateEntryCommentCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IEntryCommentRepository entryCommentRepository;

        public CreateEntryCommentCommandHandler(IMapper mapper, IEntryCommentRepository entryCommentRepository)
        {
            this.mapper = mapper;
            this.entryCommentRepository = entryCommentRepository;
        }

        public async Task<Guid> Handle(CreateEntryCommentCommand request, CancellationToken cancellationToken)
        {
            var entryComment = mapper.Map<EntryComment>(request);

            await entryCommentRepository.AddAsync(entryComment);

            return entryComment.Id;
        }
    }
}
