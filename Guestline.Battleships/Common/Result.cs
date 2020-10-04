namespace Guestline.Battleships.Common
{
    public class Result<T>
    {
        private Result(bool isSuccess, T value)
        {
            IsSuccess = isSuccess;
            Value = value;
        }

        public bool IsSuccess { get; }

        public T Value { get; }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value);
        }

        public static Result<T> Error()
        {
            return new Result<T>(false, default);
        }
    }
}
