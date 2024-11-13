using crudTest.Domain.Dto;

public interface IFunctionsApi
{
   List<UserDTO> getUsers();

   Task<Boolean> createUser(UserDTO userDto);

   Task<Boolean> updateUser(UserDTO userDto);

   Task<Boolean> deleteUser(string? numberDoc);
}