using AutoMapper;
using Domain.Interfaces;
using Domain.Services;
using Entities.DTOs.UserDTO;
using Entities.Models;
using Infrastructure.Data;
using Infrastructure.Services.Token;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly UserContext _context;
        public readonly IMapper _mapper;
        public UserRepository(UserContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<UserView>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                return _mapper.Map<IEnumerable<UserView>>(users);

            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserView> GetUserViewById(int id)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));

                if (user == null)
                {
                    throw new Exception("Usuário não encontrado");
                }

                return _mapper.Map<UserView>(user);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));

                if (user == null)
                {
                    throw new Exception("Usuário não encontrado");
                }

                return user;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserView> AddUser(CreateUser requestAdd)
        {
            try
            {
                User user = _mapper.Map<User>(requestAdd);

                string salt = PasswordHasher.GenerateSalt();
                string passwordHash = PasswordHasher.GenerateHash(requestAdd.Password, salt);
                user.Salt = salt;
                user.PasswordHash = passwordHash;
                user.CreationDate = DateTime.Now;

                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                return _mapper.Map<UserView>(user);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserView> UpdateUser(int id, UpdateUser requestUpdate)
        {
            try
            {
                User user = await GetUserById(id);
                if (user == null)
                {
                    throw new Exception("Usuário, não encontrado");
                }

                user.Name = requestUpdate.Name;
                user.Email = requestUpdate.Email;
                user.Phone = requestUpdate.Phone;

                await _context.SaveChangesAsync();

                return _mapper.Map<UserView>(user);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                User user = await GetUserById(id);

                if (user == null)
                {
                    throw new Exception("Usuário não encontrado");
                }

                _context.Remove(user);
                await _context.SaveChangesAsync();

                return true;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Login(LoginRequest request)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToUpper().Equals(request.UserName.ToUpper()));

                if(user != null)
                {
                    string passwordRequestHash = PasswordHasher.GenerateHash(request.Password, user.Salt);

                    if (passwordRequestHash.Equals(user.PasswordHash))
                    {
                        string token = TokenService.GenerateToken(user);

                        return token;

                    } else { throw new Exception("Senha incorreta"); }
                } else
                {
                    throw new Exception("Usuário não encontrado");
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
