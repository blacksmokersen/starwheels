public static class TeamsExtensions
{
    public static Team OppositeTeam(this Team team)
    {
        switch (team)
        {
            case Team.Blue:
                return Team.Red;
            case Team.Red:
                return Team.Blue;
            default:
                return Team.None;
        }
    }

    public static Team ToTeam(this string s)
    {
        return (Team)System.Enum.Parse(typeof(Team), s);
    }
}
