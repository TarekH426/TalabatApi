namespace TalabatAPIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode,string? message =null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string GetDefaultMessageForStatusCode(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "A bad request,you have made",
                401 => "Authorized ,you are not",
                404 => "Resourse was not found",
                500 => "Errors are the path to the dark side.Error lead to anger.Anger leads to hate.Hate leads to career change",
                 _ => null
            };
        }
    }
}
