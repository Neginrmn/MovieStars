
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieStars.API;

namespace MovieStars.Pages;

public class MovieModel : PageModel
{
  // Movie objects
  public string backdrop_path = "https://image.tmdb.org/t/p/w1920_and_h800_multi_faces";
  public string poster_path = "https://image.tmdb.org/t/p/w500";
  public string title = "";
  public string overview = "";
  public string yearReleased = "";
  public string revenue = "";
  public string tagline = "";
  public List<string> genres = new List<string>();

  // Cast objects
  public const int MAX_CAST = 5;
  public string profile_path = "https://image.tmdb.org/t/p/w500";
  public List<string> castIMGs = new List<string>();
  public List<string> castNames = new List<string>();
  public List<string> castIDs = new List<string>();

  // Video objects
  public List<string> videoNames = new List<string>();
  public List<string> videoKeys = new List<string>();

  public async Task OnGet(string _id)
  {
    await Fetch.MovieDetails(_id);

    backdrop_path += Fetch.movie.backdrop_path;
    poster_path += poster_path + Fetch.movie.poster_path;
    title = Fetch.movie.title;
    overview = Fetch.movie.overview;
    yearReleased = Fetch.movie.release_date.Substring(0, 4);
    revenue = Fetch.movie.revenue.ToString("C0");
    tagline = Fetch.movie.tagline;
    // add the genres
    for (int i = 0; i < Fetch.movie.genres.Count; i++)
    {
      genres.Add(Fetch.movie.genres[i].name);
    }

    // loop through the cast and populate our cast objects
    for (int i = 0; i < MAX_CAST; i++)
    {
      if (Fetch.castCrew.cast[i].profile_path != null)
      {
        castIMGs.Add(profile_path + Fetch.castCrew.cast[i].profile_path);
      }
      else
      {
        castIMGs.Add("./images/chubby-rabbit.png");
      }
      castNames.Add(Fetch.castCrew.cast[i].name);
      castIDs.Add(Fetch.castCrew.cast[i].id.ToString());
    } // cast for

    // loop through the videos and populate our video objects
    for (int i = 0; i < Fetch.videoSet.results.Count; i++)
    {
      if (Fetch.videoSet.results[i].site.ToLower() == "youtube")
      {
        videoNames.Add(Fetch.videoSet.results[i].name);
        videoKeys.Add(Fetch.videoSet.results[i].key);
      }
    } // video for
  } // OnGet()

  public void OnPostGetActor(string actorID)
  {
    Response.Redirect("./Actor?_actorID=" + actorID);
  } // OnPostGetActor()
}