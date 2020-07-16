namespace Mango.WEB.Models.Base.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
            ErrorMessage = string.Empty;
        }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
