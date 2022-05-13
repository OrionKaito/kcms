using AutoMapper;
using KCMS.Domain.Base;
using KCMS.Domain.User;
using KCMS.Domain.ViewModel;
using KCMS.Ultitlies;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KCMS.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtUlti;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, JwtService jwtUlti)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtUlti = jwtUlti;
        }

        public string Login(UserLoginModel model)
        {
            var user = _userRepository.Get(u => u.Username == model.Username, null, "").FirstOrDefault();

            if (user == null)
            {
                throw new Exception("Invalid Credentials");
            }

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                throw new Exception("Invalid Credentials");
            }

            var jwt = _jwtUlti.Generate(user.Id);

            return jwt;
        }

        public async Task<User> AddUser(UserInsertModel model)
        {
            var existedUsername = _userRepository.Get(u => u.Username == model.Username, null, "").FirstOrDefault();

            if (existedUsername != null)
            {
                throw new Exception("Username already existed");
            }

            var user = _mapper.Map<User>(model);
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            try
            {
                user.CreatedDate = DateTime.UtcNow;
                _userRepository.Insert(user);
                await _unitOfWork.CommitAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert Fail : " + ex);
            }
        }

        public User GetUser(long Id)
        {
            var user = _userRepository.GetByID(Id);
            if (user == null)
            {
                throw new Exception("NotFound");
            }
            return user;
        }
    }
}
