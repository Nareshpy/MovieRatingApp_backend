using MovieRatingApp.DTO;
using MovieRatingApp.Interfaces;
using MovieRatingApp.Models;
using System.Reflection;
using System.Drawing;

namespace MovieRatingApp.Helper
{
    public class MappingProfiles:AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<Movie, MovieDTO>();
            //.ForMember(dest => dest.MoviePoster,
            //    opt => opt.MapFrom(src => ConvertByteArrayToFormFile(src.MoviePoster)
            //    )
            //    );
            CreateMap<MovieDTO, Movie>();
                //.ForMember(dest => dest.MoviePoster, opt => opt.MapFrom(src => GetByteArrayFromIFormFile(src.MoviePoster)));
            CreateMap<Reviewer, ReviewerDTO>();
            CreateMap<ReviewerDTO, Reviewer>();
            CreateMap<ReviewDTO, Reviews>();
            CreateMap<Reviews, ReviewDTO>();
        }
        private byte[] GetByteArrayFromIFormFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
       
        //private static Image GetImageFromByteArray(Byte[] bytes)
        //{
        //    if (bytes == null || bytes.Length == 0)
        //    {
        //        return null;
        //    }
        //    using (MemoryStream memoryStream = new MemoryStream(bytes))
        //    {
        //       Image image = Image.FromStream(memoryStream);
        //        return image;
        //    }
        //}
        private IFormFile ConvertByteArrayToFormFile(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream(bytes))
            {
                var file = new FormFile(memoryStream, 0, bytes.Length, "MoviePoster", "MoviePoster.jpg") 
                { 
                    Headers=new HeaderDictionary(),
                    ContentType = "image/jpeg",
            
                };
                //Image.FromStream(memoryStream);
                //System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition()
                //{
                //    FileName = file.Name,
                //};
                //file.ContentDisposition=cd.ToString();
                return file;
            }

           
        }
       
    }
}
