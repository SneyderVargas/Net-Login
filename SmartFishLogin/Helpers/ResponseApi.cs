using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartFishLogin.Resx;

namespace SmartFishLogin.Helpers
{
    public class ResponseApi<T>
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public T Data { get; set; }
        public bool Error { get; set; }

        public ResponseApi()
        { }
        
        //*---------------respuestas estandarizada validacion model esta -------------------------//
        public ResponseApi(bool verror, ModelStateDictionary modelState,T vdata,string vdetalle)
        {
            Error = verror; // true, false
            Message = modelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage;
            Data = vdata;
            Details = vdetalle;
        }

        //---- estandar de respuesta --//
        public ResponseApi(bool verror, string vmessge,T vdata, string vdetalle)
        {
            Error = verror; // true, false
            Message = vmessge;
            Data = vdata;
            Details = vdetalle;
        }

    }
}
