namespace Brokerless.DTOs.User
{
    public class PropertyAnalyticsResultDTO
    {
        public int TotalViews { get; set; }
        public List<PropertyAnalyticsDTO> Last7DaysAnalytics { get; set; }

    }
}
