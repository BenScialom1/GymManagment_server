using GymManagment_server.Models;

namespace GymManagment_server.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }  // Ensure this is included
        public int GymId { get; set; }
        public int Rank { get; set; }
        public string Description { get; set; }
        public DateTime CommentDate { get; set; }

        public CommentDTO() { }

        public CommentDTO(Comment comment)
        {
            CommentId = comment.CommentId;
            UserId = comment.UserId;
            GymId = comment.GymId;
            Rank = comment.Rank;
            Description = comment.Description;
            CommentDate = comment.Date;
        }
    }
}

