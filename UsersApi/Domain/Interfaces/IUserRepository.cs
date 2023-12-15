using Entities.DTOs.UserDTO;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserView>> GetAllUsers();
        public Task<UserView> GetUserViewById(int id);
        public Task<UserView> AddUser(CreateUser requestAdd);
        public Task<UserView> UpdateUser(int id, UpdateUser requestUpdate);
        public Task<bool> DeleteUser(int id);
        public Task<bool> Login(LoginRequest request);
    }
}
