using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRatingApp.DTO;
using MovieRatingApp.Models;
using MovieRatingApp.Repository;

namespace MovieRatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly ReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;
        public ReviewerController(ReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDTO>>(_reviewerRepository.GetReviewers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewers);
        }
        [HttpGet("{reviewerId}")]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            var reviewer=_mapper.Map<ReviewerDTO>(_reviewerRepository.GetReviewer(reviewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewer);
        }
        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            var reviews=_mapper.Map<List<Reviews>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDTO newReviewer)
        {
            if (newReviewer == null)
            {
                return BadRequest(ModelState);
            }
            var reviewer = _reviewerRepository.GetReviewers().Where(reviewer => reviewer.Email.Trim().ToUpper() == newReviewer.Email.Trim().ToUpper()).FirstOrDefault();
            if (reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer Already Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewerMap = _mapper.Map<Reviewer>(newReviewer);
            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDTO updatedReviewer)
        {
            if (updatedReviewer == null)
            {
                return BadRequest(ModelState);
            }
            if (reviewerId != updatedReviewer.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);
            if (!_reviewerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(400, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewerRepository.DeleteReviewer(reviewerId))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(400, ModelState);
            }
            return NoContent();
        }
    }
}
