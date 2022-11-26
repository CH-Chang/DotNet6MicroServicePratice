using Mango.Services.Email.Messages;

namespace Mango.Services.Email.Repository
{
    public interface IEmailRepository
    {

        public Task SendAndLogEmail(UpdatePaymentResultMessage message);
    }
}
