using Domain.Interfaces;
using Entities.DTOs.UserDTO;
using Microsoft.AspNetCore.Mvc;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("Users/v1/Users")]
    public class UserController : ControllerBase
    {
        public readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("GetAllViews")]
        public async Task<IActionResult> GetAllUserViews()
        {
            try
            {
                IEnumerable<UserView> users = await _userRepository.GetAllUsers();

                return Ok(users);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUser request)
        {
            try
            {
                UserView user = await _userRepository.AddUser(request);

                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                bool resutl = await _userRepository.Login(request);
                return Ok(resutl);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }
    }
}
