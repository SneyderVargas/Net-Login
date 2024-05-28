using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartFishLogin.Resx;

namespace SmartFishLogin.Helpers
{
    public class ResponseApi<T>
    {
        public int Size { get; set; }
        public object[] Value { get; set; }

        // Variables de respuestas para Json
        public string Message { get; set; }
        public string Details { get; set; }

        // variables respuesta lista
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<T> data {  get; set; }

        // variable register system
        public int id { get; set; }
        public string token { get; set; }

        // variable control de error
        public List<T> errors { get; set; }
        public string error { get; set; }

        //*---------------respuestas estandarizada body -------------------------//
        public static ResponseApi<T> Response(string error)
        {
            return new ResponseApi<T>
            {
                error = error
            };
        }
        //*-------------- single response CREATE GET SINGLE UPDATE ------------------------//
        public static ResponseApi<T> ResponseSingle(List<T> vdata)
        {
            return new ResponseApi<T>
            {
                data = vdata
            };
        }

        //*---------------respuestas estandarizada list GET -------------------------//
        public static ResponseApi<T> ResponseList(int vpage, int vper_page, int vtotal, int vtotal_pages, List<T>vdata)
        {
            return new ResponseApi<T>
            {
                page = vpage,
                per_page = vper_page,
                total = vtotal,
                total_pages = vtotal_pages,
                data = vdata,
            };
        }

        //*---------------respuestas estandarizada register -------------------------//
        public static ResponseApi<T> ResponseRegister(int vid, string vtoken)
        {
            return new ResponseApi<T>
            {
                id = vid,
                token = vtoken,
            };
        }

        //*---------------respuestas estandarizada error -------------------------//
        public static ResponseApi<T> ResponseValidacion(List<T> verrors)
        {
            return new ResponseApi<T>
            {
                errors = verrors
            };
        }

        //*---------------respuestas estandarizada validacion model esta -------------------------//
        public static ResponseApi<T> ResponseValidacion(ModelStateDictionary modelState)
        {
            return new ResponseApi<T>
            {
                Message = modelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage
            };
        }

        //*---------------respuestas Login exito -------------------------//
        public static ResponseApi<T> ResponseLogin(string vtoken)
        {
            return new ResponseApi<T>
            {
                token = vtoken,
            };
        }

    }
}
