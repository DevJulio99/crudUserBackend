using crudTest.Domain.Dto;
using crudTest.Domain.Model;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace crudTest.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IFunctionsApi _functionsApi;
        
        public UserController(IFunctionsApi functionsApi)
        {
            this._functionsApi = functionsApi;
        }

        [HttpGet("list-users")]
        public ActionResult listUsers()
        {
            try
            {

                var response = _functionsApi.getUsers();
                var apiResult = new ApiResult { Success = true, code = ConstantsCrud.Success.code2000, Messages = [ConstantsCrud.Success.Message2000], Data = response };

                // if (respuesta.Data.Count == 0)
                // {
                //     apiResult = new ApiResult { Success = false, code = ConstantsCrud.Success.code2002, Messages = ["No se encontraron usuarios"], Data = respuesta.Data };
                // }

                // HttpContext.Response.Headers.Add("Total-Count", respuesta.Data.Count > 0 ? respuesta.Data[0].ROW_TOTAL.ToString() :"0");
                return Ok(apiResult);
            }
            catch (Exception ex)
            {
                var messageObject = ex.Message.Contains("{");
                var response = messageObject ? JsonConvert.DeserializeObject<ResponseValidation>(ex.Message) ?? new ResponseValidation { code = "", Messages = "", validations = [] } : new ResponseValidation { code = "", Messages = ex.Message, validations = [] };
                if (messageObject && response.codeError == 500)
                {
                    response.code = ConstantsCrud.ErrorInterno.code5000;
                    response.Messages = ConstantsCrud.ErrorInterno.Message5000;
                }
                if (!messageObject)
                {
                    response.code = ConstantsCrud.ErrorInterno.code5000;
                    response.Messages = ConstantsCrud.ErrorInterno.Message5000;
                }
                var apiResult = new ApiResult { Success = false, code = response.code, Messages = [response.Messages], validations = response.validations, Data = null };

                switch (ex)
                {
                    case UnauthorizedAccessException _:
                        return Unauthorized(apiResult);
                    case ArgumentException _:
                        return BadRequest(apiResult);
                    default:
                        return StatusCode(response.codeError, apiResult);
                }
            }
        }

        [HttpPost("create-user")]
        public async Task<ActionResult> CreateUser(UserDTO userDto)
        {
            try
            {

                var response = await _functionsApi.createUser(userDto);
                var apiResult = new ApiResult { Success = true, code = ConstantsCrud.Success.code2001, Messages = [ConstantsCrud.Success.Message2001], Data = response };
                return Ok(apiResult);
            }
            catch (Exception ex)
            {
                var messageObject = ex.Message.Contains("{");
                var response = messageObject ? JsonConvert.DeserializeObject<ResponseValidation>(ex.Message) ?? new ResponseValidation { code = "", Messages = "", validations = [] } : new ResponseValidation { code = "", Messages = ex.Message, validations = [] };
                if (messageObject && response.codeError == 500)
                {
                    response.code = ConstantsCrud.ErrorInterno.code5000;
                    response.Messages = ConstantsCrud.ErrorInterno.Message5000;
                }
                if (!messageObject)
                {
                    response.code = ConstantsCrud.ErrorInterno.code5000;
                    response.Messages = ConstantsCrud.ErrorInterno.Message5000;
                }
                var apiResult = new ApiResult { Success = false, code = response.code, Messages = [response.Messages], validations = response.validations, Data = null };

                switch (ex)
                {
                    case UnauthorizedAccessException _:
                        return Unauthorized(apiResult);
                    case ArgumentException _:
                        return BadRequest(apiResult);
                    default:
                        return StatusCode(response.codeError, apiResult);
                }
            }
        }

        [HttpPost("update-user")]
        public async Task<ActionResult> UpdateUser(UserDTO userDto)
        {
            try
            {

                var response = await _functionsApi.updateUser(userDto);
                var apiResult = new ApiResult { Success = true, code = ConstantsCrud.Success.code2002, Messages = [ConstantsCrud.Success.Message2002], Data = response };
                return Ok(apiResult);
            }
            catch (Exception ex)
            {
                var messageObject = ex.Message.Contains("{");
                var response = messageObject ? JsonConvert.DeserializeObject<ResponseValidation>(ex.Message) ?? new ResponseValidation { code = "", Messages = "", validations = [] } : new ResponseValidation { code = "", Messages = ex.Message, validations = [] };
                if (messageObject && response.codeError == 500)
                {
                    response.code = ConstantsCrud.ErrorInterno.code5000;
                    response.Messages = ConstantsCrud.ErrorInterno.Message5000;
                }
                if (!messageObject)
                {
                    response.code = ConstantsCrud.ErrorInterno.code5000;
                    response.Messages = ConstantsCrud.ErrorInterno.Message5000;
                }
                var apiResult = new ApiResult { Success = false, code = response.code, Messages = [response.Messages], validations = response.validations, Data = null };

                switch (ex)
                {
                    case UnauthorizedAccessException _:
                        return Unauthorized(apiResult);
                    case ArgumentException _:
                        return BadRequest(apiResult);
                    default:
                        return StatusCode(response.codeError, apiResult);
                }
            }
        }

        [HttpPost("delete-user/{numberDoc}")]
        public async Task<ActionResult> DeleteUser(string? numberDoc)
        {
            try
            {

                var response = await _functionsApi.deleteUser(numberDoc);
                var apiResult = new ApiResult { Success = true, code = ConstantsCrud.Success.code2003, Messages = [ConstantsCrud.Success.Message2003], Data = response };
                return Ok(apiResult);
            }
            catch (Exception ex)
            {
                var messageObject = ex.Message.Contains("{");
                var response = messageObject ? JsonConvert.DeserializeObject<ResponseValidation>(ex.Message) ?? new ResponseValidation { code = "", Messages = "", validations = [] } : new ResponseValidation { code = "", Messages = ex.Message, validations = [] };
                if (messageObject && response.codeError == 500)
                {
                    response.code = ConstantsCrud.ErrorInterno.code5000;
                    response.Messages = ConstantsCrud.ErrorInterno.Message5000;
                }
                if (!messageObject)
                {
                    response.code = ConstantsCrud.ErrorInterno.code5000;
                    response.Messages = ConstantsCrud.ErrorInterno.Message5000;
                }
                var apiResult = new ApiResult { Success = false, code = response.code, Messages = [response.Messages], validations = response.validations, Data = null };

                switch (ex)
                {
                    case UnauthorizedAccessException _:
                        return Unauthorized(apiResult);
                    case ArgumentException _:
                        return BadRequest(apiResult);
                    default:
                        return StatusCode(response.codeError, apiResult);
                }
            }
        }
    }
}

