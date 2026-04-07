namespace E_Commerce.Common.GeneralResult
{
    public class Error
    {
        public Error() { }
        public string ErrorCode { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}