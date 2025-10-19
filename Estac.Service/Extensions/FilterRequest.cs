namespace Estac.Service.Extensions
{
    public class FilterRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public decimal? OrganismoId { get; set; }

        public IList<FilterBody> Filter { get; set; }

        public IList<FilterBody> FilterOr { get; set; } = new List<FilterBody>();


        public FilterRequest GetFilter(FilterPropertRequest filterPropertRequest)
        {
            Page = 1;
            PageSize = 50;
            Filter = new List<FilterBody>
        {
            new FilterBody
            {
                Condition = FilterConditionType.Contenha,
                Property = filterPropertRequest.Property,
                ConditionStart = filterPropertRequest.ConditionStart,
                ConditionEnd = filterPropertRequest.ConditionEnd
            }
        };
            return this;
        }
    }
}
