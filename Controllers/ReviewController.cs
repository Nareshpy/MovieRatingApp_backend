using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRatingApp.DTO;
using MovieRatingApp.Interfaces;
using MovieRatingApp.Models;


namespace MovieRatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController:Controller
    {
        private readonly IMapper _mapper;
        private readonly IReview _reviewRepository;
        private readonly IMovie _movieRepository;
        private readonly IReviewer _reviewerRepository;
        public ReviewController(IMapper mapper,IReview reviewRepository, IMovie movieRepository,IReviewer reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _movieRepository = movieRepository;
            _reviewerRepository = reviewerRepository;

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviews>))]
        public IActionResult GetReviews() 
        {
            var reviews= _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviews());
            //var reviews=_reviewRepository.GetReviews();
            if(!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Reviews))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = _mapper.Map<ReviewDTO>(_reviewRepository.GetReview(reviewId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);

        }
        [HttpGet("movie/{movieId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviews>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfAMovie(int movieId)
        {
            var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviewsOfAMovie(movieId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int movieId, [FromBody] ReviewDTO newReview)
        {
            if (newReview == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewMap = _mapper.Map<Reviews>(newReview);
            reviewMap.Movie= _movieRepository.GetMovieById(movieId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("successfully created");
        }
        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId,[FromQuery] int reviewerId, [FromQuery] int movieId ,[FromBody] ReviewDTO updatedReview)
        {
            if (updatedReview == null)
            {
                return BadRequest(ModelState);
            }
            if (reviewId != updatedReview.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewMap = _mapper.Map<Reviews>(updatedReview);
            //var review=_reviewRepository.GetReview(reviewId);
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                ModelState.AddModelError("", "Reviewer doesn't exist");
                return StatusCode(500, ModelState);
            }
            if (!_movieRepository.MovieExists(movieId))
            {
                ModelState.AddModelError("", "Movie doesn't exist");
                return StatusCode(500, ModelState);
            }
            reviewMap.Reviewer=_reviewerRepository.GetReviewer(reviewId);
            reviewMap.Movie=_movieRepository.GetMovieById(movieId);
            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var reviewToDelete = _reviewRepository.GetReview(reviewId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
            }
            return NoContent();

        }

    }
}
