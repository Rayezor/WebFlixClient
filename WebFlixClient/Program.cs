using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;
using WebFlix.Models;
using static WebFlix.Models.Movie;



namespace WebFlix
{
	public class Program
	{
		static void Main(string[] args)
		{
			DoWork().Wait();
			PostMovieAsync().Wait();
		}

		private static async Task PostMovieAsync()      // Post Method
		{
			try
			{
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://localhost:5006/");
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				Movie flm = new Movie()
				{
					MovieID = "12345699",
					Title = "Lord of the Rings, The Fellowship of the Ring",
					Genre = MovieGenre.adventure,
					Cert = Certification.Cert15A,
					ReleaseDate = Convert.ToDateTime("10/01/2011"),
					AverageRating = 8
				};

				HttpResponseMessage response = await client.PostAsJsonAsync("api/Movie", flm);

				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("New Movie Added");
					Console.WriteLine();
				}
				else
				{
					Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		static async Task DoWork()
		{
			try
			{
				//1)  create an instance of HttpClient
				using (HttpClient client = new HttpClient())
				{
					//2)  init the base address of the Webservice we are calling
					client.BaseAddress = new Uri("http://localhost:5006/");                             // base URL for API Controller i.e. RESTFul service

					// 3) Set the media types this client will accept (in this case, for a webservice,  JSON) so we add an Accept header for JSON
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					// 1
					// get info for ALL movies
					// GET ../api/Movie
					HttpResponseMessage response = await client.GetAsync("api/Movie");
					response.EnsureSuccessStatusCode();
					if (response.IsSuccessStatusCode)                                                   // 200.299
					{
						// read result 
						var movies = await response.Content.ReadAsAsync<IEnumerable<Movie>>();
						foreach (var m in movies)
						{
							Console.WriteLine
								("\n_____" + "Movie Details;\nMovieID: " + m.MovieID + "\nTitle: " + m.Title + "\nGenre: " + m.Genre + "\nCertification: " + m.Cert + "\nRelease Date: " + m.ReleaseDate + "\nAverage Rating: " + m.AverageRating + "\n_____");
						}
					}
					else
					{
						Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
					}

					// 2
					// get movie info for search movieID
					Movie movieInfo = new Movie();
					movieInfo = new Movie();
					response = await client.GetAsync("api/Movie/{id}");
					response.EnsureSuccessStatusCode();
					if (response.IsSuccessStatusCode)
					{
						// read result 
						movieInfo = await response.Content.ReadAsStringAsync();
						Console.WriteLine
							("\n_____" + "Movie Details;\nMovieID: " + movieInfo.MovieID +
							"\nTitle: " + movieInfo.Title +
							"\nGenre: " + movieInfo.Genre +
							"\nCertification: " + movieInfo.Cert +
							"\nRelease Date: " + movieInfo.ReleaseDate +
							"\nAverage Rating: " + movieInfo.AverageRating + "\n_____");
					}
					else
					{
						Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
					}

					// 3
					// get movie title by keyword search
					response = client.GetAsync("api/Movie/GetByKeyWord/{keyword}").Result;
					response.EnsureSuccessStatusCode();
					if (response.IsSuccessStatusCode)
					{
						// read result 
						var movieTitles = await response.Content.ReadAsStringAsync();
						foreach (Movie title in movieTitles)
						{
							Console.WriteLine(title);
						}
					}
					else
					{
						Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}
	}
}