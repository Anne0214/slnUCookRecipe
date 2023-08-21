using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class PasswordSalt密碼鹽
{
    public int PasswordSalt密碼鹽Id { get; set; }

    public int MemberId會員Pk { get; set; }

    public string Salt { get; set; } = null!;

    public virtual Member會員 MemberId會員PkNavigation { get; set; } = null!;
}
