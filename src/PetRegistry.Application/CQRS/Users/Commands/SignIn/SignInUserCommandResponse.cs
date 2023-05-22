namespace PetRegistry.Application.CQRS.Users.Commands.SignIn
{
    public class SignInUserCommandResponse
    {
        public string? Token { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public SignInUserCommandResponse() { }
    }
}
