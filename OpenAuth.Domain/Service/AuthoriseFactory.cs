using OpenAuth.Domain.Interface;

namespace OpenAuth.Domain.Service
{
    /// <summary>
    ///  Ȩ�޷��乤���������Ƿ��ǿ������˺Ŵ���
    /// </summary>
    public class AuthoriseFactory
    {
        private IUnitWork _unitWork;

        public AuthoriseFactory(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public AuthoriseService Create(string loginuser)
        {
            if (loginuser == "System")
            {
                return new SystemAuthService(_unitWork);
            }
            else
            {
                return  new AuthoriseService(_unitWork)
                {
                    User = _unitWork.FindSingle<User>(u =>u.Account == loginuser)
                };
            }
        }
    }
}