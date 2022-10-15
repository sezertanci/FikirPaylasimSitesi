namespace WebApp.Infrastructure.Models
{
    public class FavoriteClickedEventArgs : EventArgs
    {
        public Guid Id { get; set; }
        public bool IsFaved { get; set; }

        public FavoriteClickedEventArgs()
        {

        }

        public FavoriteClickedEventArgs(Guid id, bool isFaved)
        {
            Id = id;
            IsFaved = isFaved;
        }
    }
}
