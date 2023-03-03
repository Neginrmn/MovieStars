
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieStars.API;

namespace MovieStars.Pages;

public class ActorModel : PageModel
{

  public static int CalculateAge(DateTime birthDate)
  {
    DateTime now = DateTime.Today;
    int age = now.Year - birthDate.Year;
    if (now < birthDate.AddYears(age))
    {
      age--;
    }
    return age;
  }
  public string actorIMG = "";
  public string actorName = "";
  public string actorBirthyear = "";
  public string actorBirthplace = "";
  public string actorBio = "";
  public string knownFor = "";


  public string profile_path = "https://image.tmdb.org/t/p/w500";
  public string file_path = "https://image.tmdb.org/t/p/w500";
  public string poster_path = "https://image.tmdb.org/t/p/w500";
  public List<string> imageGallery = new List<string>();
  public List<string> knownForGallery = new List<string>();
  public List<string> knownForMovies = new List<string>();


  public async Task OnGet(string _actorID)
  {

    await Fetch.ActorDetails(_actorID);
    actorBirthyear = Fetch.actor.birthday;
    actorBio = Fetch.actor.biography;
    actorName = Fetch.actor.name;
    actorBirthplace = Fetch.actor.place_of_birth;
    knownFor = Fetch.actor.known_for_department;

    profile_path += Fetch.actor.profile_path;
    await Fetch.ActorImageGallery(_actorID);

    // Fetching actor's image gallery
    for (int i = 0; i < Fetch.profileSet.profiles.Count; i++)
    {
      if (Fetch.profileSet.profiles[i].file_path != null)
      {
        imageGallery.Add(file_path + Fetch.profileSet.profiles[i].file_path);
      }
      else
      {
        imageGallery.Add("./images/chubby-rabbit.png");
      }
    }
    // Also known for posters

    await Fetch.MovieCredits(_actorID);
    for (int i = 0; i < Fetch.knownForCastCrew.cast.Count; i++)
    {
      knownForGallery.Add(poster_path + Fetch.knownForCastCrew.cast[i].poster_path);


    }

  }





}//ActorModel



