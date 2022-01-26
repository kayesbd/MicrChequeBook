namespace KBZ.Web.APIModels
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public long Id { get; set; }
        public object ModelValue { get; set; }
        public string ErrorMessage { get; set; }
    }
}