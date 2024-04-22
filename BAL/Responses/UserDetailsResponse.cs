using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Responses
{
    public class UserDetailsResponse
    {
        public Guid Id { get; set; }

        public int SequenceId { get; set; }

        public string? UserId { get; set; }

        public string? Password { get; set; }

        public string? UserType { get; set; }

        public string? Gender { get; set; }

        public string? Designation { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public bool? Isdelete { get; set; }

        public Guid? PersonId { get; set; }

        public string? ImageUrl { get; set; }

        public bool? Status { get; set; }

        public static UserDetailsResponse FromUser(User user)
        {
            return new UserDetailsResponse
            {
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                Designation = user.Designation,
                Gender= user.Gender,
                Id = user.Id,
                ImageUrl = user.ImageUrl,
                Isdelete = user.Isdelete,
                PersonId = user.PersonId,
                Password = user.Password,
                SequenceId = user.SequenceId,
                Status  = user.Status,
                UpdatedBy = user.UpdatedBy,
                UpdatedDate = user.UpdatedDate,
                UserId = user.UserId,
                UserType = user.UserType
            };
        }
    }
}
