namespace SmartFishLogin.Helpers
{
    public class ResponseApi
    {
        public int Size { get; set; }
        public object[] Value { get; set; }

        // Variables de respuestas para Json
        public bool error { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }


        //*---------------respuestas estandarizada body -------------------------//
        public static ResponseApi Response(bool error, object value, string message, string details)
        {
            return new ResponseApi
            {
                error = error, // entrga de estado de respuesta
                Message = message,
                Value = new object[] { value },
                Details = details,
            };
        }
        //---------------respuestas estandarizada body -------------------------*//

        //*---------------respuestas estandarizada list -------------------------//
        public static ResponseApi ResponseList(bool error, object[] value, string message, string details)
        {
            return new ResponseApi
            {
                error = error, // entrga de estado de respuesta
                Message = message,
                Value = value,
                Size = value.Length,
                Details = details,
            };
        }
    }
}
