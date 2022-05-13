using KCMS.Domain.Base;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace KCMS.Domain.ViewModel
{
    public class AdvertisingListViewModel
    {
        public IEnumerable<Advertising.Advertising> Results { get; set; }
        public int TotalPages { get; set; }
    }

    public class AdvertisingInsertModel
    {
        public int Priority { get; set; }
        public int Options { get; set; }
        public string Type { get; set; }
        public string Position { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public AdvertisingStatus Status { get; set; }
    }

    public class AdvertisingUpdateModel : AdvertisingInsertModel
    {
        public long Id { get; set; }
    }
}
