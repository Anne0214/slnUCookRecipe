using AutoMapper.Execution;
using Azure.Core;
using prjMSITUCookServices.Dto.Info;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCookServices.EFModels;
using prjMSITUCookServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Implement
{
    public class MemberService : IMemberService
    {
        private readonly UcookRecipeDatabaseContext _context;
        private readonly PasswordHasher _passwordHasher;
        private readonly DesCrypto _crypto;
        private readonly string _key = "54li28f9";
        private readonly string _iv = "7s9gew41";


        public MemberService(UcookRecipeDatabaseContext context) {
            _context = context;
            _passwordHasher = new PasswordHasher(_context);
            _crypto = new DesCrypto();
        }

        /// <summary>
        /// 該帳號已註冊過
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int CreateNewMember(MemberInfo info,string host)
        {
            
            if (_context.Member會員s.Any(x=>x.MemberEmail==info.Email)) {
                return 1;
            }
            
            var member = new Member會員()
            {
                MemberEmail = info.Email.Trim(),
                NickName暱稱 = info.NickName.Trim(),
                ProfilePhoto頭貼 = "https://i.imgur.com/1Ygj6Oc.png",
                SelfIntro自介 = $"這個人很懶 都不寫自我介紹> <"
            };
            _context.Add(member);
            _context.SaveChanges();

            //密碼加密
            member.MemberPassword = _passwordHasher.HashPassword(info.Password.Trim(), member.MemberId會員Pk);
                

            //產生驗證code，存在資料庫
            string code = Guid.NewGuid().ToString("D");
            MemberVerifyCode會員註冊驗證 record = new MemberVerifyCode會員註冊驗證()
            {
                MemberId會員Fk = member.MemberId會員Pk,
                VerifyCode = code,
                VerityExpirationData過期日 = DateTime.Now.AddDays(1)
            };


            //寄信
            string url = host+$"/home/Verification/?code=" + code;
            string subject = "【驗證信】加入UCook最後一步";
            string body = $"感謝您註冊UCook!<br>為了驗證您的信箱，請點擊以下連結:<br>{url}<br>如果你沒有註冊UCook，請忽略這封信。<br>這是自動發送的信件，請不要直接回覆這封信。<br>感謝您!";

            
            EmailSender sender = new EmailSender(subject,body,info.Email);

            bool sendSuccess = sender.Send();
            if (!sendSuccess) {
                _context.Remove(member);
                _context.SaveChanges();
                return 2;
            }
            _context.Add(record);
            _context.SaveChanges();
            return member.MemberId會員Pk;
        }

        public bool SendVerificationLetterAgain(string email,string host) {
            try
            {
                var member = _context.Member會員s.FirstOrDefault(x => x.MemberEmail == email);
                if (member == null) {
                    return false;
                }
                var id = member.MemberId會員Pk;
                var code = _context.MemberVerifyCode會員註冊驗證s.FirstOrDefault(x => x.MemberId會員Fk == id).VerifyCode;

                string url = host + $"/home/Verification/?code=" + code;
                string subject = "【驗證信】加入UCook最後一步";
                string body = $"感謝您註冊UCook!<br>為了驗證您的信箱，請點擊以下連結:<br>{url}<br>如果你沒有註冊UCook，請忽略這封信。<br>這是自動發送的信件，請不要直接回覆這封信。<br>感謝您!";

                //寄信
                EmailSender sender = new EmailSender(subject, body, email);
                bool sendSuccess = sender.Send();

                if (!sendSuccess)
                {
                    return false;
                }
                return true;
            }
            catch { return false; }
        }

        public bool VerifyRegisterEmail(string code)
        {
            //找到該code
            var record = _context.MemberVerifyCode會員註冊驗證s.FirstOrDefault(x => x.VerifyCode == code);
            if (record == null) {
                return false;
            }
            //確認是否有該id存在
            var member = _context.Member會員s
                                .FirstOrDefault(x=>x.MemberId會員Pk == record.MemberId會員Fk);
            if (member == null) { return false;}

            //找到該名用戶，修改該名會員資料，表示註冊成功
            member.RegisterTime註冊時間 = DateTime.Now;
            //刪除驗證碼
            _context.Remove(record);
            _context.SaveChanges();

            return true;
        }

        public (MemberResultModel,string) VerifyLogin(LoginInfo info) {
            try
            {
                //找到該用戶
                var member = _context.Member會員s
                    .SingleOrDefault(x => x.MemberEmail == info.Email);
            if (member == null)
            {
                return (null,"不存在該筆帳號");
            }

            if (member.RegisterTime註冊時間 == null)
            {
                if (_context.MemberVerifyCode會員註冊驗證s.Any(x => x.MemberId會員Fk == member.MemberId會員Pk)){
                    return (null,"尚未驗證完成");
                }
                else {
                    
                    return (null,"註冊流程出錯，請重新註冊");
                }
            }
            //確認密碼是否正確

                bool isPasswordCorrect = _passwordHasher.CheckPassword(member.MemberId會員Pk, info.Password, member.MemberPassword);
                if (!isPasswordCorrect)
                {
                    return (null,"密碼錯誤");
                }
                //傳回memberResultModel
                var result = new MemberResultModel()
                {
                    Email = member.MemberEmail,
                    MemberId = member.MemberId會員Pk,
                    NickName = member.NickName暱稱,
                    ProfilePhoto = member.ProfilePhoto頭貼
                };

                return (result,"");
            }
            catch {
                return (null,"發生未知錯誤");
            }
        }
        public bool CheckSameName(string name) {
            return _context.Member會員s.Any(x => x.NickName暱稱 == name);
        }
        public bool CheckSameEmail(string email)
        {
            return _context.Member會員s.Any(x =>x.MemberEmail == email);
        }
        /// <summary>
        /// 確認該名用戶是否正在驗證中
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckVerifyingMember(string email) {
            try
            {
                var member = _context.Member會員s.FirstOrDefault(x => x.MemberEmail == email);
                if (member == null) {
                    return false;
                }
                var id = member.MemberId會員Pk;
                return _context.MemberVerifyCode會員註冊驗證s.Any(x => x.MemberId會員Fk == id);
            }
            catch { return false; }        
        }
    }
}
