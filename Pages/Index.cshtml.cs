
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieStars.API;

namespace MovieStars.Pages;

public class IndexModel : PageModel
{
  public int overview_cutoff = 80;
  public string imagePath = "https://image.tmdb.org/t/p/w500";
  public List<string> movieTitles = new List<string>();
  public List<string> overviews = new List<string>();
  public List<string> posterURLs = new List<string>();
  public List<string> movieIDs = new List<string>();

  public async Task OnPostSearch(string search)
  {
    await Fetch.MovieSearch(search);

    foreach (Result result in Fetch.resultSet.results)
    {
      movieTitles.Add(result.title);
      if (result.overview.Length >= overview_cutoff)
      {
        overviews.Add(result.overview.Substring(0, overview_cutoff) + "...");
      }
      else
      {
        overview_cutoff = result.overview.Length;
        overviews.Add(result.overview.Substring(0, overview_cutoff) + "...");
      }
      string path = imagePath + result.poster_path;
      posterURLs.Add(path);
      movieIDs.Add(result.id.ToString());
    }
  } // OnPostSearch()

  public async Task OnGet()
  {
    await Fetch.GetTrends();

    foreach (Poster poster in Fetch.posterSet.results)
    {
      movieTitles.Add(poster.title);
      if (poster.overview.Length >= overview_cutoff)
      {
        overviews.Add(poster.overview.Substring(0, overview_cutoff) + "...");
      }
      else
      {
        overview_cutoff = poster.overview.Length;
        overviews.Add(poster.overview.Substring(0, overview_cutoff) + "...");
      }
      string path = imagePath + poster.poster_path;
      posterURLs.Add(path);
      movieIDs.Add(poster.id.ToString());
    }
  } // OnGet()



  public void OnPostDetails(string movieID)
  {
    // redirect to the movie page
    // pass along the movieID
    Response.Redirect("./Movie?_id=" + movieID);
  } // OnPostDetails()
} // class