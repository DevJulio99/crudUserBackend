using System.Text.RegularExpressions;
using crudTest.Domain.Dto;
using crudTest.Domain.Model;
using Newtonsoft.Json;
using Npgsql;

namespace crudTest.Funciones
{
    public class FunctionsApi : IFunctionsApi
    {
        private readonly IConfiguration _configuration;
        Regex regexNumber = new Regex("^[0-9]+$");

        public FunctionsApi(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<UserDTO> getUsers()
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"]!;
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.users", connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            var listUsers = new List<UserDTO>([]);

            while (reader.Read())
            {
                listUsers.Add(new UserDTO
                {
                    numeroDocumento = reader["number_document"].ToString() ?? "",
                    nombre = reader["name_user"].ToString() ?? "",
                    celular = reader["cell_phone"].ToString() ?? "",
                });
            }

            return listUsers;
        }

        public List<UserDTO> getUserByDoc(string numberDoc)
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"]!;
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM public.users WHERE number_document = '{numberDoc}'", connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            var listUsers = new List<UserDTO>([]);

            while (reader.Read())
            {
                listUsers.Add(new UserDTO
                {
                    numeroDocumento = reader["number_document"].ToString() ?? "",
                    nombre = reader["name_user"].ToString() ?? "",
                    celular = reader["cell_phone"].ToString() ?? "",
                });
            }

            return listUsers;
        }

        public async Task<Boolean> createUser(UserDTO userDto)
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"]!;
            var response = new ResponseValidation();

            var existNumberDoc = !string.IsNullOrEmpty(userDto.numeroDocumento);
            var existCellPhone = !string.IsNullOrEmpty(userDto.celular);
            var existName = !string.IsNullOrEmpty(userDto.nombre);

            if (!existNumberDoc)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo numeroDocumento es obligatorio."],
                    field = "numeroDocumento"
                });
            }

            if (existNumberDoc && !regexNumber.IsMatch(userDto.numeroDocumento ?? ""))
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo numeroDocumento debe ser solo numeros."],
                    field = "numeroDocumento"
                });
            }

            if (existNumberDoc && regexNumber.IsMatch(userDto.numeroDocumento ?? "") && userDto.numeroDocumento.Length < 8 || userDto.numeroDocumento.Length > 8)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo numeroDocumento debe de tener 8 caracteres."],
                    field = "numeroDocumento"
                });
            }

            if (!existName)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo nombre es obligatorio."],
                    field = "nombre"
                });
            }

            if (existName && userDto.nombre.Length > 300)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo nombre solo debe tener un maximo de 300 caracteres."],
                    field = "nombre"
                });
            }

            if (!existCellPhone)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo celular es obligatorio."],
                    field = "celular"
                });
            }

            if (existCellPhone && !regexNumber.IsMatch(userDto.celular ?? ""))
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo celular debe ser solo numeros."],
                    field = "celular"
                });
            }

            if (existCellPhone && regexNumber.IsMatch(userDto.celular ?? "") && userDto.celular.Length < 9 || userDto.celular.Length > 9)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo celular debe de tener solo 9 caracteres."],
                    field = "celular"
                });
            }

            if (response.validations.Count > 0)
            {
                response.codeError = 400;
                response.code = ConstantsCrud.ErrorRequest.code4000;
                response.Messages = ConstantsCrud.ErrorRequest.Message4000;

                throw new Exception(JsonConvert.SerializeObject(response));
            }

            var responseUser = getUserByDoc(userDto.numeroDocumento);

            if (responseUser.Count > 0)
            {
                response.codeError = 400;
                response.code = ConstantsCrud.ErrorRequest.code4000;
                response.Messages = ConstantsCrud.ErrorRequest.Message4000;
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El usuario que intenta registrar ya existe."],
                    field = "numeroDocumento"
                });

                throw new Exception(JsonConvert.SerializeObject(response));
            }

            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            connection.Open();

            string commandText = $"INSERT INTO public.users (number_document, name_user, cell_phone) VALUES (@number_doc, @name, @cell_phone)";
            await using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("number_doc", userDto.numeroDocumento);
                cmd.Parameters.AddWithValue("name", userDto.nombre);
                cmd.Parameters.AddWithValue("cell_phone", userDto.celular);

                await cmd.ExecuteNonQueryAsync();
            }

            return true;
        }

        public async Task<Boolean> updateUser(UserDTO userDto)
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"]!;
            var response = new ResponseValidation();

            var existNumberDoc = !string.IsNullOrEmpty(userDto.numeroDocumento);
            var existCellPhone = !string.IsNullOrEmpty(userDto.celular);
            var existName = !string.IsNullOrEmpty(userDto.nombre);

            if (!existNumberDoc)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo numeroDocumento es obligatorio."],
                    field = "numeroDocumento"
                });
            }

            if (existNumberDoc && !regexNumber.IsMatch(userDto.numeroDocumento ?? ""))
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo numeroDocumento debe ser solo numeros."],
                    field = "numeroDocumento"
                });
            }

            if (existNumberDoc && regexNumber.IsMatch(userDto.numeroDocumento ?? "") && userDto.numeroDocumento.Length < 8 || userDto.numeroDocumento.Length > 8)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo numeroDocumento debe de tener 8 caracteres."],
                    field = "numeroDocumento"
                });
            }

            if (!existName)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo nombre es obligatorio."],
                    field = "nombre"
                });
            }

            if (existName && userDto.nombre.Length > 300)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo nombre solo debe tener un maximo de 300 caracteres."],
                    field = "nombre"
                });
            }

            if (!existCellPhone)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo celular es obligatorio."],
                    field = "celular"
                });
            }

            if (existCellPhone && !regexNumber.IsMatch(userDto.celular ?? ""))
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo celular debe ser solo numeros."],
                    field = "celular"
                });
            }

            if (existCellPhone && regexNumber.IsMatch(userDto.celular ?? "") && userDto.celular.Length < 9 || userDto.celular.Length > 9)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo celular debe de tener solo 9 caracteres."],
                    field = "celular"
                });
            }

            if (response.validations.Count > 0)
            {
                response.codeError = 400;
                response.code = ConstantsCrud.ErrorRequest.code4000;
                response.Messages = ConstantsCrud.ErrorRequest.Message4000;

                throw new Exception(JsonConvert.SerializeObject(response));
            }

            var responseUser = getUserByDoc(userDto.numeroDocumento);

            if (responseUser.Count == 0)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El usuario que intenta editar no existe."],
                    field = "numeroDocumento"
                });
            }

            if (response.validations.Count > 0)
            {
                response.codeError = 400;
                response.code = ConstantsCrud.ErrorRequest.code4000;
                response.Messages = ConstantsCrud.ErrorRequest.Message4000;

                throw new Exception(JsonConvert.SerializeObject(response));
            }

            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText = $@"UPDATE public.users SET number_document = @number_doc, name_user = @name, 
                                  cell_phone = @cell_phone WHERE number_document = @number_doc";
            await using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("number_doc", userDto.numeroDocumento);
                cmd.Parameters.AddWithValue("name", userDto.nombre);
                cmd.Parameters.AddWithValue("cell_phone", userDto.celular);

                await cmd.ExecuteNonQueryAsync();
            }

            return true;
        }

        public async Task<Boolean> deleteUser(string? numberDoc)
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"]!;
            var response = new ResponseValidation();

            var existNumberDoc = !string.IsNullOrEmpty(numberDoc);

            if (!existNumberDoc)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo numeroDocumento es obligatorio."],
                    field = "numeroDocumento"
                });
            }

            if (existNumberDoc && !regexNumber.IsMatch(numberDoc ?? ""))
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo numeroDocumento debe ser solo numeros."],
                    field = "numeroDocumento"
                });
            }

            if (existNumberDoc && regexNumber.IsMatch(numberDoc ?? "") && (numberDoc ?? "").Length < 8 || (numberDoc ?? "").Length > 8)
            {
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El campo numeroDocumento debe de tener 8 caracteres."],
                    field = "numeroDocumento"
                });
            }

            if (response.validations.Count > 0)
            {
                response.codeError = 400;
                response.code = ConstantsCrud.ErrorRequest.code4000;
                response.Messages = ConstantsCrud.ErrorRequest.Message4000;

                throw new Exception(JsonConvert.SerializeObject(response));
            }

            var responseUser = getUserByDoc(numberDoc);

            if (responseUser.Count == 0)
            {
                response.codeError = 400;
                response.code = ConstantsCrud.ErrorRequest.code4000;
                response.Messages = ConstantsCrud.ErrorRequest.Message4000;
                response.validations.Add(new ApiValidation
                {
                    Messages = ["El usuario que intenta eliminar no existe."],
                    field = "numeroDocumento"
                });

                throw new Exception(JsonConvert.SerializeObject(response));
            }

            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText = $"DELETE FROM public.users WHERE number_document=(@number_doc)";
            await using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("number_doc", numberDoc);
                await cmd.ExecuteNonQueryAsync();
            }

            return true;
        }
    }
}