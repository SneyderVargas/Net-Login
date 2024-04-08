using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using SmartFishLogin.Resx;
using System.ComponentModel;

namespace SmartFishLogin.Helpers
{
    public class ResponseErrorApi
    {
        public string Message { get; set; }

        public string Detail { get; set; }
        public int codigoError { get; set; }
        public Array AditionalInformation { get; set; }
        public List <ErrorsListDto> Errors { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string StackTrace { get; set; }

        public ResponseErrorApi()
        {
        }

        public ResponseErrorApi(string message, List<ErrorsListDto> errors)
        {
            Message = message;
            Errors = errors;
        }

        public ResponseErrorApi(ModelStateDictionary modelState)
        {
            Message = SecurityMsg.RequiredDefault;
            Detail = modelState
              .FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors
              .FirstOrDefault().ErrorMessage;
            codigoError = 1;
        }    
    }
}
