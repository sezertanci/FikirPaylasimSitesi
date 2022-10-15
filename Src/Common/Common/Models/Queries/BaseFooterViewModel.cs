﻿namespace Common.Models.Queries;

public class BaseFooterVoteViewModel
{
    public VoteType VoteType { get; set; }
}

public class BaseFooterFavoritedViewModel
{
    public bool Favorited { get; set; }
    public int FavoritedCount { get; set; }
}

public class BaseFooterRateFavoritedViewModel : BaseFooterFavoritedViewModel
{
    public VoteType VoteType { get; set; }
}
