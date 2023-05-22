using AutoMapper;
using MediatR;
using PetRegistry.Application.Common.Interfaces;
using PetRegistry.Application.Results;
using PetRegistry.Domain.Interfaces;

namespace PetRegistry.Application.CQRS.Users.Commands.SignIn
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommandRequest, DefaultResult<SignInUserCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;
        private readonly IEmailService _emailService;

        public SignInUserCommandHandler(IUserRepository userRepository, IMapper mapper, ISecurityService securityService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _securityService = securityService;
            _emailService = emailService;
        }

        public async Task<DefaultResult<SignInUserCommandResponse>> Handle(SignInUserCommandRequest request, CancellationToken cancellationToken)
        {
            request.UsernameOrEmail = request.UsernameOrEmail.ToLower();

            var user = await _userRepository.GetUserByUsernameOrEmail(request.UsernameOrEmail);

            if (user == null)
            {
                return new DefaultResult<SignInUserCommandResponse>("Incorrect username or password");
            }

            if (!_securityService.ValidatePassword(request.Password, user.PasswordHash!))
            {
                user.AccessFailedCount++;

                if (user.AccessFailedCount >= 10)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(60);

                    await _emailService.SendLockoutEmailAsync(user);
                }

                await _userRepository.UpdateAsync(user);

                return new DefaultResult<SignInUserCommandResponse>("Incorrect username or password");
            }

            if (!user.EmailConfirmed)
            {
                return new DefaultResult<SignInUserCommandResponse>("E-mail awaiting confirmation");
            }

            if (user.LockoutEnabled && user.LockoutEnd > DateTime.UtcNow)
            {
                return new DefaultResult<SignInUserCommandResponse>($"SignIn locked until {user.LockoutEnd:dd/MM/yyyy HH:mm}");
            }

            user.LastSignIn = DateTime.UtcNow;
            user.AccessFailedCount = 0;
            user.LockoutEnabled = false;
            user.LockoutEnd = null;

            await _userRepository.UpdateAsync(user);

            var tokenExpirationDate = DateTime.UtcNow.AddDays(7);

            return new DefaultResult<SignInUserCommandResponse>(
                new SignInUserCommandResponse
                {
                    Token = _securityService.GenerateJwt(user, tokenExpirationDate),
                    ExpirationDate = tokenExpirationDate
                }
            );
        }
    }
}
