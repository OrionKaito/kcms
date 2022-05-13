using KCMS.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KCMS.Domain.League
{
    [Table("Leagues")]
    public class League : AuditEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
