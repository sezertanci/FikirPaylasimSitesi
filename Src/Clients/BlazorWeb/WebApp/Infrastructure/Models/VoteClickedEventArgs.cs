namespace WebApp.Infrastructure.Models
{
    public class VoteClickedEventArgs
    {
        public Guid Id { get; set; }

        public bool IsUpVoteClicked { get; set; }
        public bool IsUpVoteDeleted { get; set; }

        public bool IsDownVoteClicked { get; set; }
        public bool IsDownVoteDeleted { get; set; }
    }
}
