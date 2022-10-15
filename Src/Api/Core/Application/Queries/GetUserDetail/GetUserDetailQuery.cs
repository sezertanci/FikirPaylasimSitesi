using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.Queries;
using Domain.Models;
using MediatR;

namespace Application.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailViewModel>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public GetUserDetailQuery(Guid userId, string userName = null)
        {
            UserId = userId;
            UserName = userName;
        }

        public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
        {
            private readonly IUserRepository userRepository;
            private readonly IMapper mapper;

            public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                this.userRepository = userRepository;
                this.mapper = mapper;
            }

            public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
            {
                var user = new User();

                if(request.UserId != Guid.Empty)
                    user = await userRepository.GetByIdAsync(request.UserId);
                else if(!string.IsNullOrEmpty(request.UserName))
                    user = await userRepository.GetFirstOrDefaultAsync(x => x.UserName == request.UserName);
                else throw new Exception("Invalid UserName or UserId");

                return mapper.Map<UserDetailViewModel>(user);
            }
        }
    }
}
