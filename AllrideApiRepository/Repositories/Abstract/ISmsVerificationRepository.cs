using AllrideApiCore.Entities;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface ISmsVerificationRepository
    {
        SmsVerification Add(SmsVerification smsVerification);
    }
}
