using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sendo.Api.Data.Models
{
    [Table("session_token", Schema = "user_data")]
    public class SessionToken
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("token")]
        public string Token { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
