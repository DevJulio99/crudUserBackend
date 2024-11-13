
namespace crudTest.Domain.Dto
{
    public class ApiResult
    {
        public ApiResult()
        {
            this.Success = true;
            this.validations = [];
            this.code = "";
        }

        public bool Success { get; set; }
        public string code { get; set; }

        public List<string> Messages { get; set; } = null!;
        public List<ApiValidation> validations { get; set; }
        public object? Data { get; set; }
    }
}
