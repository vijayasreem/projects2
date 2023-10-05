namespace projects2
{
    public class ReportDeliveryConfigurationModel
    {
        public int Id { get; set; }
        public DestinationTypeModel DestinationType { get; set; }
        public string DestinationAddress { get; set; }
        public FrequencyTypeModel FrequencyType { get; set; }
        public string DayOfWeek { get; set; }
        public string DayOfMonth { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public List<string> EmailAddresses { get; set; }
        public string SubjectLine { get; set; }
        public string BodyText { get; set; }
        public string Template { get; set; }
        public string FtpUrl { get; set; }
        public string Password { get; set; }
        public string FilePath { get; set; }

        public void ValidateDestination()
        {
            // Validate the DestinationAddress based on the selected DestinationType
        }

        public void ValidateDeliveryConfiguration()
        {
            // Validate the selected FrequencyType, DayOfWeek, DayOfMonth, and DeliveryTime
        }
    }

    public enum DestinationTypeModel
    {
        Email,
        CloudStorage,
        InternalServer
    }

    public enum FrequencyTypeModel
    {
        Daily,
        Weekly,
        Monthly,
        Custom
    }
}