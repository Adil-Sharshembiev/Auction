namespace Auction.Dto
{
    public class ResponseDto
    {
        public int Code { get; set; }
        public String Message { get; set; }
        public ResponseDto(int code, string message) { Code = code; Message = message; }
    }
    public class ResponseDto<T> : ResponseDto
    {
        public ResponseDto(int code, string message, T? data) : base(code, message)
        {
            Data = data;
        }
        public T? Data { get; set; }
    }
}
