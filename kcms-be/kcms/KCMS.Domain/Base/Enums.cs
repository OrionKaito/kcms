namespace KCMS.Domain.Base
{
    public enum ArticleType
    {
        News,
        Highlight,
        SoiKeo,
        Commentary
    }

    public enum MatchType
    {
        Hot,
        Playing,
        BLVAT
    }

    public enum MatchTime
    {
        Today,
        Tomorrow
    }

    public enum MatchStatus
    {
        Comin_Soon,
        First_Half,
        Half_Time,
        Second_half,
        Extra_Time,
        Finished
    }

    public enum AdvertisingStatus
    {
        Active,
        InActice
    }
}
