namespace crudTest.Domain.Dto
{
    public class ResponseValidation
    {

        public ResponseValidation()
        {
            this.validations = [];
            this.Messages = "";
            this.code = "";
            this.codeError = 500;
        }

        public string code { get; set; }
        public int codeError { get; set; }
        public string Messages { get; set; }
        public List<ApiValidation> validations { get; set; }
    }
}
