namespace SmartFishLogin.Helpers
{
    public class ErrorsListDto
    {
        public ErrorsListDto(string error, string message)
        {
            Error = error;
            Message = message;
        }

        public string Error { get; set; }
        public string Message { get; set; }
    }
}
