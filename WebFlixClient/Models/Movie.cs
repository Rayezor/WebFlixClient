using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace WebFlix.Models
{
	public class Movie
	{
		public enum Certification
		{
			G,Cert12A,Cert15A,Cert18,
		}

		public enum MovieGenre
		{
			action, adventure, animation, comedy, crime, drama, fantasy, family, horror, scifi, thriller
		}

		[Key]
		[Required]
		public string MovieID { get; set; }
		[Required]
		public string Title { get; set; }
		public MovieGenre Genre { get; set; }
		[Required]
		public Certification Cert { get; set; }
		public DateTime ReleaseDate { get; set; }
		private int averageRating;
		public int AverageRating
		{
			get { return averageRating; }
			set
			{
				if (1 <= value && value <= 10)
				{
					averageRating = value;
				}
				else
				{
					throw new ArgumentException("Average Raring must not be between 1 and 10");
				}
			}
		}

		/*public Movie(string _movieID, string _title, MovieGenre _genre, Certification _cert, DateTime _releaseDate, int _averageRating)
		{
			if (String.IsNullOrEmpty(_movieID) || String.IsNullOrEmpty(_title))
			{
				throw new ArgumentException("Make, Model and Registration values cannot be empty!");
			}
			this.MovieID = _movieID;
			this.Title = _title;
			this.Genre = _genre;
			this.Cert = _cert;
			this.ReleaseDate = _releaseDate;
			if (1 <= _averageRating && _averageRating <= 10)
			{
				this.AverageRating = _averageRating;
			}
		}*/
	}
}
