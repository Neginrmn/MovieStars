using System.Text.Json;

namespace MovieStars.API;

public static class Fetch
{
  public static HttpClient client = new HttpClient();
  public const string API_KEY = "d194eb72915bc79fac2eb1a70a71ddd3";
  public static string Data { get; set; }
  public static PosterSet posterSet = new PosterSet();
  public static Movie movie = new Movie();
  public static CastCrew castCrew = new CastCrew();
  public static VideoSet videoSet = new VideoSet();
  public static ResultSet resultSet = new ResultSet();
  public static Actor actor = new Actor();
  public static ProfileSet profileSet = new ProfileSet();
  public static KnownForCastCrew knownForCastCrew = new KnownForCastCrew();

  public static async Task GetTrends()
  {
    ClearHeaders();

    // Get the latest Movie Trends, for the last week
    // https://api.themoviedb.org/3/trending/all/day?api_key=<<api_key>>
    HttpResponseMessage response = await client.GetAsync(
        "https://api.themoviedb.org/3/trending/movie/week?api_key=" + API_KEY);

    if (response.IsSuccessStatusCode)
    { // 200 OK
      Data = await response.Content.ReadAsStringAsync();
      // parse the JSON into C# classes
      posterSet = JsonSerializer.Deserialize<PosterSet>(Data);
    }
    else
    {
      Data = null;
    }
  } // GetTrends()

  public static async Task MovieSearch(string search)
  {
    ClearHeaders();

    // Search for a movie based on a query string
    // https://api.themoviedb.org/3/search/movie?api_key=d194eb72915bc79fac2eb1a70a71ddd3&language=en-US&page=1&include_adult=false&query=Ghost%20In%20The%20Shell
    HttpResponseMessage response = await client.GetAsync(
        "https://api.themoviedb.org/3/search/movie?api_key=" + API_KEY +
            "&language=en-US&page=1&include_adult=false&query=" + search);

    if (response.IsSuccessStatusCode)
    { // 200 OK
      Data = await response.Content.ReadAsStringAsync();
      // parse the JSON into C# classes
      resultSet = JsonSerializer.Deserialize<ResultSet>(Data);
    }
    else
    {
      Data = null;
    }
  } // MovieSearch()






  /////////////////////////////ActorDetails

  public static async Task ActorDetails(string _actorID)
  {
    ClearHeaders();

    //https://api.themoviedb.org/3/person/{person_id}?api_key=<<api_key>>&language=en-US
    HttpResponseMessage response = await client.GetAsync(
    "https://api.themoviedb.org/3/person/" + _actorID + "?api_key=" + API_KEY + "&language=en-US"
    );
    if (response.IsSuccessStatusCode)
    { // 200 OK
      Data = await response.Content.ReadAsStringAsync();
      // parse the JSON into C# classes
      actor = JsonSerializer.Deserialize<Actor>(Data);
    }
    else
    {
      Data = null;
    }
  }//ActorDetails

  ///////////////////////////////////////////////////////////////////////////Image Gallery
  public static async Task ActorImageGallery(string _actorID)
  {
    ClearHeaders();

    //https://api.themoviedb.org/3/person/{person_id}/images?api_key=<<api_key>>
    HttpResponseMessage response = await client.GetAsync(
    "https://api.themoviedb.org/3/person/" + _actorID + "/images?api_key=" + API_KEY
    );
    if (response.IsSuccessStatusCode)
    { // 200 OK
      Data = await response.Content.ReadAsStringAsync();
      // parse the JSON into C# classes
      profileSet = JsonSerializer.Deserialize<ProfileSet>(Data);
    }
    else
    {
      Data = null;
    }
  }//ActorImageGallery


  ///////MovieCredits
  public static async Task MovieCredits(string _actorID)
  {
    ClearHeaders();

    //https://api.themoviedb.org/3/person/{person_id}/movie_credits?api_key=<<api_key>>&language=en-US
    HttpResponseMessage response = await client.GetAsync(
    "https://api.themoviedb.org/3/person/" + _actorID + "/movie_credits?api_key=" + API_KEY
    );
    if (response.IsSuccessStatusCode)
    { // 200 OK
      Data = await response.Content.ReadAsStringAsync();
      // parse the JSON into C# classes
      knownForCastCrew = JsonSerializer.Deserialize<KnownForCastCrew>(Data);
    }
    else
    {
      Data = null;
    }
  }//MovieCredits








  public static async Task MovieDetails(string movieID)
  {
    ClearHeaders();

    // Get the details for a specific movie, based on the movieID
    // https://api.themoviedb.org/3/movie/505642?api_key=d194eb72915bc79fac2eb1a70a71ddd3&language=en-US
    HttpResponseMessage response = await client.GetAsync(
        "https://api.themoviedb.org/3/movie/" + movieID + "?api_key=" + API_KEY + "&language=en-US");

    if (response.IsSuccessStatusCode)
    { // 200 OK
      Data = await response.Content.ReadAsStringAsync();
      // parse the JSON into C# classes
      movie = JsonSerializer.Deserialize<Movie>(Data);
    }
    else
    {
      Data = null;
    }

    // Next get the cast members for the movie
    // https://api.themoviedb.org/3/movie/640146/credits?api_key=<<api_key>>&language=en-US
    response = await client.GetAsync(
        "https://api.themoviedb.org/3/movie/" + movieID + "/credits?api_key=" + API_KEY + "&language=en-US");

    if (response.IsSuccessStatusCode)
    { // 200 OK
      Data = await response.Content.ReadAsStringAsync();
      // parse the JSON into C# classes
      castCrew = JsonSerializer.Deserialize<CastCrew>(Data);
    }
    else
    {
      Data = null;
    }

    // Last but not least let's grab the YouTube videos
    // https://api.themoviedb.org/3/movie/{movie_id}/videos?api_key=<<api_key>>&language=en-US
    response = await client.GetAsync(
        "https://api.themoviedb.org/3/movie/" + movieID + "/videos?api_key=" + API_KEY + "&language=en-US");

    if (response.IsSuccessStatusCode)
    { // 200 OK
      Data = await response.Content.ReadAsStringAsync();
      // parse the JSON into C# classes
      videoSet = JsonSerializer.Deserialize<VideoSet>(Data);
    }
    else
    {
      Data = null;
    }
  } // MovieDetails()

  private static void ClearHeaders()
  {
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applicationException/json"));
  } // ClearHeader()

} // class