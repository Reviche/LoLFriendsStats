public class MatchDto
{
    public MetadataDto Metadata { get; set; }
    public InfoDto Info { get; set; }
}

public class MetadataDto
{
    public string DataVersion { get; set; }
    public string MatchId { get; set; }
    public List<string> Participants { get; set; }
}

public class InfoDto
{
    public long GameCreation { get; set; }
    public long GameDuration { get; set; }
    public string GameMode { get; set; }
    public List<ParticipantDto> Participants { get; set; }
}

public class ParticipantDto
{
    public string Puuid { get; set; }
    public bool Win { get; set; }
    public string ChampionName { get; set; }
    // Add other properties as needed
}
