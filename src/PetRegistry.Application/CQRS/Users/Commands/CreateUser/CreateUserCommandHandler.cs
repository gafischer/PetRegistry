using AutoMapper;
using MediatR;
using PetRegistry.Application.Common.Interfaces;
using PetRegistry.Application.Results;
using PetRegistry.Domain.Entities;
using PetRegistry.Domain.Interfaces;

namespace PetRegistry.Application.CQRS.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, DefaultResult<CreateUserCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ISecurityService _securityService;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IEmailService emailService, ISecurityService securityService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
            _securityService = securityService;
        }

        public async Task<DefaultResult<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetAsync(u =>
                (u.Username == request.Username && u.Username != null) ||
                (u.Email == request.Email && u.Email != null));

                if (user?.Username == request.Username)
                {
                    if (!user.EmailConfirmed)
                    {
                        return new DefaultResult<CreateUserCommandResponse>(
                            "This email address is not confirmed. Please check your email and confirm it or request a new confirmation link");
                    }

                    return new DefaultResult<CreateUserCommandResponse>(
                        "The provided username is already in use. Please choose a different username or try to recover your account");
                }

                if (user?.Email == request.Email)
                {
                    if (!user.EmailConfirmed)
                    {
                        return new DefaultResult<CreateUserCommandResponse>(
                            "This email address is not confirmed. Please check your email and confirm it or request a new confirmation link");
                    }

                    return new DefaultResult<CreateUserCommandResponse>(
                        "The provided email is already in use. Please use another email or try to recover your account");
                }

                string verifyCode = _securityService.GenerateVerifyCode();

                var userEntity = _mapper.Map<User>(request);

                var hashedPassword = _securityService.GenerateHashedPassword(request.Password);

                userEntity.PasswordHash = hashedPassword;
                userEntity.EmailConfirmed = false;
                userEntity.VerifyCode = _securityService.GenerateSha256(verifyCode);

                var newUser = await _userRepository.AddAsync(userEntity);

                await _emailService.SendRegisterEmailAsync(userEntity, verifyCode);

                var userResponse = _mapper.Map<CreateUserCommandResponse>(newUser);

                return new DefaultResult<CreateUserCommandResponse>(userResponse);
            }
            catch (Exception ex)
            {
                return new DefaultResult<CreateUserCommandResponse>(ex.Message);
            }
        }
    }
}
