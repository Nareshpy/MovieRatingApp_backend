using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRatingApp.DTO;
using MovieRatingApp.Interfaces;
using MovieRatingApp.Models;

namespace MovieRatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController:Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovie _movieRepository;
        private readonly IReview _reviewRepository;
        public MovieController(IMapper mapper,IMovie movieRepository,IReview reviewRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository; 
            _reviewRepository = reviewRepository;
        }
        [HttpGet]
        public IActionResult GetMovies() 
        {
            try { 
            var movies = _mapper.Map<List<MovieDTO>>(_movieRepository.GetMovies());
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(movies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("getMovieById/{movieId}")]
        public IActionResult GetMovieById(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }
            var movie = _mapper.Map<MovieDTO>(_movieRepository.GetMovieById(movieId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(movie);
        }
        [HttpGet("getMovieByName/{movieName}")]
        public IActionResult GetMoviesByName(string movieName)
        {
            var e = _movieRepository.GetMoviesByName(movieName);

            var movies = _mapper.Map<List<MovieDTO>>(e);
            //CreateMovie(movies[0]);
            if (movies.Count <= 0)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(movies);
        }
        [HttpGet("{movieId}/rating")]
        public IActionResult GetMovieRating(int movieId)
        { 
  
            if (!_movieRepository.MovieExists(movieId))
            { 
                return NotFound(); 
            }
            decimal rating=_movieRepository.GetMovieRating(movieId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rating);
        }
        [HttpGet("{category}")]
        public IActionResult GetMoviesByCategory(string category) 
        {
            var movies=_mapper.Map<List<MovieDTO>>(_movieRepository.GetMoviesByCategory(category));
            if (movies.Count <= 0)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(movies);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovie([FromBody] MovieDTO newMovie)
            {
                if (newMovie == null)
                {
                    return BadRequest(ModelState);
                }
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var movieMap = _mapper.Map<Movie>(newMovie);
                //Console.WriteLine(newMovie);
                if(newMovie.MoviePoster!=null && newMovie.MoviePoster.Length > 0)
            
                {
                //using (var memoryStream = new MemoryStream())
                //{
                //    newMovie.MoviePoster.CopyTo(memoryStream);
                //    movieMap.MoviePoster = memoryStream.ToArray();
                //}

                }
                if (!_movieRepository.CreateMovie(movieMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }
                return Ok("New movie successfully added");
            }
        [HttpPut("{movieId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovie(int movieId,[FromBody] MovieDTO updatedMovie)
        {
            if(updatedMovie == null)
            {
                return BadRequest(ModelState);
            }
            if (movieId != updatedMovie.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var movieMap=_mapper.Map<Movie>(updatedMovie);
            if(!_movieRepository.UpdateMovie(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(400, ModelState);
            }
            return NoContent();
            
        }
        [HttpDelete("{movieId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMovie(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewsToDelete=_reviewRepository.GetReviewsOfAMovie(movieId).ToList();
            if (reviewsToDelete.Count > 0)
            {
                if (!_reviewRepository.DeleteReviews(reviewsToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting reviews");
                    return StatusCode(400, ModelState);
                }
            }
            if(!_movieRepository.DeleteMovie(movieId))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(400, ModelState);
            }
            return NoContent();
        }


    }
}
