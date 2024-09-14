using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Web;
using Microsoft.Extensions.Logging;

public class RiotApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly ILogger<RiotApiService> _logger;

    public RiotApiService(HttpClient httpClient, IConfiguration configuration, ILogger<RiotApiService> logger)
    {
        _httpClient = httpClient;
        _apiKey = configuration["RiotApiKey"];
        _logger = logger;
    }

    public async Task<SummonerDto> GetSummonerByRiotId(string gameName, string tagLine)
    {
        try
        {
            var encodedGameName = HttpUtility.UrlEncode(gameName);
            var encodedTagLine = HttpUtility.UrlEncode(tagLine);

            var response = await _httpClient.GetAsync($"https://americas.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{encodedGameName}/{encodedTagLine}?api_key={_apiKey}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error fetching data from Riot API. Status code: {response.StatusCode}, Response: {await response.Content.ReadAsStringAsync()}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Successfully fetched data from Riot API: {content}");
            return JsonConvert.DeserializeObject<SummonerDto>(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching summoner data from Riot API.");
            throw;
        }
    }
    
    public async Task<SummonerDto> GetSummonerByPuuid(string puuid)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SummonerDto>(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching summoner data by PUUID");
            throw;
        }
    }

    
    public async Task<List<RankedStatsDto>> GetRankedDataBySummonerId(string summonerId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://na1.api.riotgames.com/lol/league/v4/entries/by-summoner/{summonerId}?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<RankedStatsDto>>(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching ranked data");
            throw;
        }
    }
    
    public async Task<List<string>> GetMatchHistoryByPuuid(string puuid, int count = 20)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://americas.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?start=0&count={count}&api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching match history");
            throw;
        }
    }

    public async Task<MatchDto> GetMatchDetails(string matchId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://americas.api.riotgames.com/lol/match/v5/matches/{matchId}?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MatchDto>(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching match details");
            throw;
        }
    }


}
