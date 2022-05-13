using KCMS.Domain.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCMS.Domain.User
{
    [Table("User")]
    public class User : AuditEntity
    {
        [MaxLength(100)]
        public string Username { get; set; }
        [MaxLength(100)]
        [JsonIgnore]
        public string Password { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
