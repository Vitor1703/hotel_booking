namespace Application.Common
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public ErrorCode? ErrorCode { get; set; }
    }

    public enum ErrorCode
    {
        NOT_FOUND,
        INVALID_PERSON_ID,
        MISSING_REQUIRED_INFORMATION,
        INVALID_EMAIL,
        COULD_NOT_STORE_DATA
    }
}
