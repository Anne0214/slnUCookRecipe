using AutoMapper.Execution;
using prjMSITUCookServices.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Implement
{
    public class PasswordHasher
    {
        private readonly int _saltLength = 10;
        private readonly UcookRecipeDatabaseContext _context;

        public PasswordHasher(UcookRecipeDatabaseContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 將密碼加密
        /// </summary>
        /// <param name="password">欲加密的密碼</param>
        /// <returns>加密結果</returns>
        public string HashPassword(string password, int? memberId)
        {
            //產生隨機值作為鹽
            var rnd = Guid.NewGuid().ToString("D");

            //有給我用戶Id，就儲存鹽
            if (memberId != null && memberId != 0)
            {
                StoreSalt(rnd, (int)memberId);
            }
            //將鹽傳回另一個function，做合併加密處理

            return BuildHash(rnd, password);
        }

        /// <summary>
        /// 儲存鹽
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="memberId"></param>
        private void StoreSalt(string rnd, int memberId)
        {
            //確認是否有該會員紀錄存在，沒有的話就用新增的
            var record = _context
                            .PasswordSalt密碼鹽s
                            .FirstOrDefault(x => x.MemberId會員Pk == memberId);
            if (record == null)
            {
                //新增
                PasswordSalt密碼鹽 data = new PasswordSalt密碼鹽()
                {
                    MemberId會員Pk = memberId,
                    Salt = rnd,
                };
                _context.Add(data);
            }
            else
            {
                //修改
                record.Salt = rnd;
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// 確認用戶密碼是否正確
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <param name="corr"></param>
        /// <returns></returns>
        public bool CheckPassword(int id,string password,string corr) {
            var salt = _context.PasswordSalt密碼鹽s
                    .SingleOrDefault(x => x.MemberId會員Pk == id)
                    .Salt;
            if (string.IsNullOrEmpty(salt)) {
                return false;
            }
            return corr == BuildHash(salt,password);

        }

        private string BuildHash(string salt, string password)
        {
            //組合密碼及鹽
            string combine = salt + "|" + password;

            //使用另一個function加密
            return Hash(combine);
        }
        private string Hash(string input)
        {
            //使用sha256加密
            using (SHA256 mySHA256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashValue = mySHA256.ComputeHash(bytes);
                //傳回加密結果
                return Convert.ToBase64String(bytes);
            }
        }
        /// <summary>
        /// 確認用戶輸入的密碼是否正確
        /// </summary>
        /// <param name="password">用戶輸入的密碼</param>
        /// <param name="memberId">用戶帳號</param>
        /// <returns></returns>
        public bool CheckPassword(string password, int memberId)
        {
            //找到該會員的鹽
            var record = _context.PasswordSalt密碼鹽s.Where(x => x.MemberId會員Pk == memberId).FirstOrDefault();
            if (record == null)
            {
                return false;
            }
            var salt = record.Salt;

            //找到該用戶的密碼
            var member = _context.Member會員s.Where(x => x.MemberId會員Pk == memberId).FirstOrDefault();
            if (member == null)
            {
                return false;
            }
            var corr = member.MemberPassword;

            //將用戶輸入的密碼加密
            var HashedPassword = HashPassword(password, null);

            //return 加密結果與資料庫中的是否相同
            return HashedPassword == corr;
        }
    }
}
