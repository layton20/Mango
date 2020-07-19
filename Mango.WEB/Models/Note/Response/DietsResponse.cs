using Mango.WEB.Models.Base.Response;
using System.Collections.Generic;

namespace Mango.WEB.Models.Note.Response
{
    public class DietsResponse : BaseResponse
    {
        public IList<DietResponse> Diets { get; set; }
    }
}
