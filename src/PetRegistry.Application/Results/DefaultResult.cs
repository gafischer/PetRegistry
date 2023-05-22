namespace PetRegistry.Application.Results
{
    public class DefaultResult<T>
    {
        public bool Success { get; }
        public T? Data { get; }
        public IEnumerable<string>? Errors { get; }

        public DefaultResult()
        {
        }

        public DefaultResult(bool success)
        {
            Success = success;
        }

        public DefaultResult(T data)
        {
            Success = true;
            Data = data;
        }

        public DefaultResult(string error)
        {
            Success = false;
            Errors = new List<string> { error };
        }

        public DefaultResult(IEnumerable<string> errors)
        {
            Success = false;
            Errors = errors;
        }
    }
}
