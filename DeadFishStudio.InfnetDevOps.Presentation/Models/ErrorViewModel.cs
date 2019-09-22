namespace DeadFishStudio.InfnetDevOps.Presentation.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string ErrorDescription { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}