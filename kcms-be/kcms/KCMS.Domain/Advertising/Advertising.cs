using KCMS.Domain.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCMS.Domain.Advertising
{
    public class Advertising : AuditEntity
    {
        public string Type { get; set; }
        public string Position { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
        public int Options { get; set; }
        public AdvertisingStatus Status { get; set; }
    }
}
