namespace Mango.WEB.Models.Note.Request
{
    public class GetDietsRequest
    {
        public GetDietsRequest()
        {
            Prefix = string.Empty;
            CaseSensitive = false;
        }

        public string Prefix { get; set; }
        public bool CaseSensitive { get; set; }
    }
}
