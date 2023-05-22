using AutoMapper;
using MediatR;
using PetRegistry.Application.Results;
using PetRegistry.Domain.Interfaces;

namespace PetRegistry.Application.CQRS.Users.Commands.VerifyCode
{
    public class VerifyCodeUserCommandHandler : IRequestHandler<VerifyCodeUserCommandRequest, DefaultResult<VerifyCodeUserCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;

        public VerifyCodeUserCommandHandler(IUserRepository userRepository, IMapper mapper, ISecurityService securityService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _securityService = securityService;
        }

        public async Task<DefaultResult<VerifyCodeUserCommandResponse>> Handle(VerifyCodeUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return new DefaultResult<VerifyCodeUserCommandResponse>($"E-mail: {request.Email} not found");
            }

            if (user.EmailConfirmed)
            {
                return new DefaultResult<VerifyCodeUserCommandResponse>($"E-mail: {request.Email} already confirmed");
            }

            if (user.VerifyCode != _securityService.GenerateSha256(request.Code!))
            {
                return new DefaultResult<VerifyCodeUserCommandResponse>("Invalid code");
            }

            user.VerifyCode = null;
            user.EmailConfirmed = true;

            await _userRepository.UpdateAsync(user);

            return new DefaultResult<VerifyCodeUserCommandResponse>(true);
        }
    }
}
