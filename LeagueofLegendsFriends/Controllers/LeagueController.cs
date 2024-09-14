using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class LeagueController : ControllerBase
{
    private readonly RiotApiService _riotApiService;

    public LeagueController(RiotApiService riotApiService)
    {
        _riotApiService = riotApiService;
    }

    [HttpGet("{gameName}/{tagLine}")]
    public async Task<IActionResult> GetSummonerStats(string gameName, string tagLine)
    {
        var summoner = await _riotApiService.GetSummonerByRiotId(gameName, tagLine);
        
        if (summoner == null)
        {
            return NotFound("Summoner not found or an error occurred while fetching summoner data.");
        }

        return Ok(summoner);
    }
    
    [HttpGet("summoner/by-puuid/{puuid}")]
    public async Task<IActionResult> GetSummonerByPuuid(string puuid)
    {
        var summoner = await _riotApiService.GetSummonerByPuuid(puuid);
    
        if (summoner == null)
        {
            return NotFound("Summoner not found.");
        }

        return Ok(summoner);
    }

    
    [HttpGet("{summonerId}/ranked")]
    public async Task<IActionResult> GetRankedStats(string summonerId)
    {
        var rankedStats = await _riotApiService.GetRankedDataBySummonerId(summonerId);
    
        if (rankedStats == null || rankedStats.Count == 0)
        {
            return NotFound("Ranked data not found.");
        }

        return Ok(rankedStats);
    }
    
    [HttpGet("{puuid}/matches")]
    public async Task<IActionResult> GetMatchHistory(string puuid, int count = 5)
    {
        var matchHistory = await _riotApiService.GetMatchHistoryByPuuid(puuid, count);
    
        if (matchHistory == null || matchHistory.Count == 0)
        {
            return NotFound("No match history found.");
        }

        return Ok(matchHistory);
    }

[HttpGet("matches/{matchId}")]
public async Task<IActionResult> GetMatchDetails(string matchId)
{
    var matchDetails = await _riotApiService.GetMatchDetails(matchId);
    if (matchDetails == null)
    {
        return NotFound("Match not found.");
    }

    return Ok(matchDetails);
}

    
}