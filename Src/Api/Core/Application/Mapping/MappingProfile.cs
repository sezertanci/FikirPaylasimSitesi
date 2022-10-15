using AutoMapper;
using Common.Events.EntryCommentFavoriteEvent;
using Common.Events.EntryCommentVoteEvent;
using Common.Events.EntryFavoriteEvent;
using Common.Events.EntryVoteEvent;
using Common.Models.Queries;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel;
using Common.Models.RequestModels.EntryCommentRequestModels.UpdateRequestModels;
using Common.Models.RequestModels.EntryRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels;
using Common.Models.RequestModels.EntryRequestModels.UpdateRequestModels;
using Common.Models.RequestModels.TagRequestModels.CreateRequestModels;
using Common.Models.RequestModels.TagRequestModels.UpdateRequestModels;
using Common.Models.RequestModels.UserRequestModels.CreateRequestModels;
using Common.Models.RequestModels.UserRequestModels.UpdateRequestModels;
using Domain.Models;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEntryCommand, Entry>().ReverseMap();
            CreateMap<UpdateEntryCommand, Entry>().ReverseMap();
            CreateMap<Entry, GetEntriesViewModel>();

            CreateMap<CreateEntryTagCommand, EntryTag>().ReverseMap();

            CreateMap<CreateEntryFavoriteCommand, CreateEntryFavoriteEvent>().ReverseMap();
            CreateMap<EntryFavorite, CreateEntryFavoriteEvent>().ReverseMap();

            CreateMap<CreateEntryVoteCommand, CreateEntryVoteEvent>().ReverseMap();

            CreateMap<DeleteEntryVoteCommand, DeleteEntryVoteEvent>().ReverseMap();

            CreateMap<DeleteEntryFavoriteCommand, DeleteEntryFavoriteEvent>().ReverseMap();

            CreateMap<CreateTagCommand, Tag>().ReverseMap();
            CreateMap<UpdateTagCommand, Tag>().ReverseMap();
            CreateMap<GetTagsViewModel, Tag>().ReverseMap();

            CreateMap<CreateEntryCommentCommand, EntryComment>().ReverseMap();
            CreateMap<UpdateEntryCommentCommand, EntryComment>().ReverseMap();

            CreateMap<CreateEntryCommentVoteCommand, CreateEntryCommentVoteEvent>().ReverseMap();

            CreateMap<EntryCommentFavorite, CreateEntryCommentFavoriteEvent>().ReverseMap();
            CreateMap<CreateEntryCommentFavoriteCommand, CreateEntryCommentFavoriteEvent>().ReverseMap();

            CreateMap<DeleteEntryCommentFavoriteCommand, DeleteEntryCommentFavoriteEvent>().ReverseMap();

            CreateMap<DeleteEntryCommentVoteCommand, DeleteEntryCommentVoteEvent>().ReverseMap();

            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<UpdateUserCommand, User>().ReverseMap();
            CreateMap<LoginUserViewModel, User>().ReverseMap();
            CreateMap<UserDetailViewModel, User>().ReverseMap();

            //CreateMap<Entry, GetEntriesViewModel>()
            //    .ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count));
        }
    }
}
