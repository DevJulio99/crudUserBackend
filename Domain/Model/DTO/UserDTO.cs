
namespace crudTest.Domain.Dto
{
    public class UserDTO
    {
        public UserDTO()
        {
            this.numeroDocumento = "";
            this.nombre = "";
            this.celular = "";
        }

        public string? numeroDocumento { get; set; }
        public string? nombre { get; set; }
        public string? celular { get; set; }
    }
}
