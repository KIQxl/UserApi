using Domain.Interfaces;
using Entities.DTOs.UserDTO;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
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

        [HttpGet]
        [Route("GetViewById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetViewById([FromRoute] int id)
        {
            try
            {
                UserView user = await _userRepository.GetUserViewById(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser request)
        {
            try
            {
                UserView user = await _userRepository.AddUser(request);

                return Created($"Users/v1/Users/GetViewById/{user.Id}", user);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("UpdateUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUser request)
        {
            try
            {
                UserView user = await _userRepository.UpdateUser(id, request);

                return Created($"Users/v1/Users/GetViewById/{user.Id}", user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                bool result = await _userRepository.DeleteUser(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                string token = await _userRepository.Login(request);
                return Ok(token);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }
    }
}
