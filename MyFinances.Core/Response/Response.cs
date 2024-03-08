using System.Collections.Generic;
using System.Linq;

namespace MyFinances.Core.Response
{
    public class Response
    {
        public Response()
        {
            Errors = new List<Error>();
        }

        public List<Error> Errors { get; set; }

        public bool IsSuccess => Errors == null || !Errors.Any();

        //To to samo
        //public bool IsSuccess 
        //{ 
        //    get
        //    {
        //        return Errors == null || !Errors.Any();
        //    }

        //}








    }
}
