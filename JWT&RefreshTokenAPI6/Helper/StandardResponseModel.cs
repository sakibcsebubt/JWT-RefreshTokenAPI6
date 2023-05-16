namespace JWT_RefreshTokenAPI6.Helper
{
    public class StandardResponseModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string? Massage { get; set; }
        public object? Data { get; set; }
    }
}
