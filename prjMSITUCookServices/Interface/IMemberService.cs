using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using prjMSITUCookServices.Dto.Info;
using prjMSITUCookServices.Dto.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Interface
{
    public interface IMemberService
    {
        int CreateNewMember(MemberInfo info,string host);

        bool VerifyRegisterEmail(string code);
        (MemberResultModel, string) VerifyLogin(LoginInfo info);

        bool CheckSameName(string name);
        bool CheckSameEmail(string email);

        bool SendVerificationLetterAgain(string email, string host);

        bool CheckVerifyingMember(string email);
    }
}
