using System.Collections.Immutable;

namespace RoadDefectsService.Core.Application.Models
{
    /// <summary>Используется для возвращение результата выполнения метода.</summary>
    /// <remarks>
    /// Содержит словарь ошибок Errors, ключами которого являются краткие описания ошибок, а значениями их подробные описания.
    /// Также содержит флаг IsSuccess, который указывает успешное/неуспешное завершение метода, 
    /// и дополнительный статус код StatusCodeExecutionResult, который указывает на тип ошибки.
    /// </remarks>
    public class ExecutionResult
    {
        private ImmutableDictionary<string, List<string>> _errors = ImmutableDictionary<string, List<string>>.Empty;
        public ImmutableDictionary<string, List<string>> Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                IsSuccess = false;
            }
        }

        private bool _isSuccess = false;
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set
            {
                _isSuccess = value;
                if (IsSuccess) StatusCode = StatusCodeExecutionResult.Ok;
            }
        }

        public StatusCodeExecutionResult StatusCode { get; set; }

        public ExecutionResult() { }

        public ExecutionResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public ExecutionResult(StatusCodeExecutionResult statusCode, string keyError, params string[] error)
        {
            IsSuccess = false;
            _errors = _errors.Add(keyError, error.ToList());
            StatusCode = statusCode;
        }

        public ExecutionResult(StatusCodeExecutionResult statusCode, ImmutableDictionary<string, List<string>> errors)
        {
            IsSuccess = false;
            _errors = errors;
            StatusCode = statusCode;
        }
    }

    /// <summary> Наследуется от ExecutionResult и выполняет ту же задачу</summary>
    /// <remarks>
    /// Содержит дополнительное поле, в который записывается результат работы метода, если он завершился успешно
    /// </remarks>
    public class ExecutionResult<TSuccessResult> : ExecutionResult
    {
        public TSuccessResult? _result;
        public TSuccessResult? Result
        {
            get { return _result; }
            set
            {
                _result = value;
                IsSuccess = true;
            }
        }

        public ExecutionResult() { }
        public ExecutionResult(TSuccessResult result) { Result = result; }

        public ExecutionResult(StatusCodeExecutionResult statusCode, string keyError, params string[] error) : base(statusCode, keyError, error) { }

        public ExecutionResult(StatusCodeExecutionResult statusCode, ImmutableDictionary<string, List<string>> errors) : base(statusCode, errors) { }
    }
}
