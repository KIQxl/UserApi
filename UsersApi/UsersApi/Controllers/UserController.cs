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

        [HttpGet]
        [Route("GetViewByUsername/{username}")]
        public async Task<IActionResult> GetByUserName([FromRoute] string userName)
        {
            try
            {
                UserView user = await _userRepository.GetUserViewByUserName(userName);
                return Ok(user);
            } catch (Exception ex)
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

                return Ok( new
                {
                    result = result,
                    message = "Usuário deletado com sucesso"
                });
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
                UserView user = await _userRepository.GetUserViewByUserName(request.UserName);
                return Ok( new
                {
                    user = user,
                    token = token
                });

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("ActivateUser/{id}")]
        public async Task<IActionResult> ActivateUser([FromRoute] int id)
        {
            try
            {
                bool result = await _userRepository.ActivateUser(id);
                return Ok(new
                {
                    result = result,
                    message = "Usuário Ativado com sucesso"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("DeativateUser/{id}")]
        public async Task<IActionResult> DeactivateUser([FromRoute] int id)
        {
            try
            {
                bool result = await _userRepository.DeactivateUser(id);
                return Ok(new
                {
                    result = result,
                    message = "Usuário Desativado com sucesso"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<IActionResult> Dapper([FromRoute] int id)
        //{
        //    try
        //    {
        //        UserView user = await _userRepository.Dapper(id);
        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
