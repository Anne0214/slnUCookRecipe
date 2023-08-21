using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using prjMSITUCook.Service;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCookServices.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Implement
{
    public class CommonService
    {
        private readonly UcookRecipeDatabaseContext _context;
        private readonly IMapper _mapper;   

        public CommonService(UcookRecipeDatabaseContext context) {
            _context = context;

            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<ServiceMapping>());

            this._mapper = config.CreateMapper();
        }
        public async Task<string> GetImgUrl(IFormFile img, string path)
        {
            var copyStream = new FileStream(path+img.Name, FileMode.Create);
            var copyImgTask = img.CopyToAsync(copyStream);


            //開始進行request準備
            HttpClient http = new HttpClient();

            //token
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "7ec7a433a47cfaacbd6de6bf907b7dda3670f892");


            //body設定:MultipartFormDataContent
            var content = new MultipartFormDataContent();

            //等圖片複製好後才能將檔案添加到content
            await copyImgTask;
            copyStream.Close();
            var stream = new FileStream(path+img.Name, FileMode.Open);
            var fileContent = new StreamContent(stream);
            content.Add(new StringContent("file"), "type");

            // 设定文件类型表单项，使用StreamContent存放文件流
            content.Add(new StreamContent(stream), "image");

            //發Post
            HttpResponseMessage result = http.PostAsync("https://api.imgur.com/3/image", content).Result;

            // 接受结果
            string responseResult = result.Content.ReadAsStringAsync().Result;
            http.Dispose();
            stream.Close();

            JObject objJObject = JObject.Parse(responseResult);
            JToken objToken = objJObject.SelectToken("data.link");
            if (objToken == null)
            {
                return "fail";
            }
            //處理json
            //var json = JObject.Parse(responseResult);

            return objToken.Value<string>();
        }

        public AuthorListItemResultModel TransferToAuthorListItem(Member會員 m,int id)
        {

            _context.Entry(m).Collection(x => x.Recipe食譜s).Load();
            _context.Entry(m).Collection(x => x.Follow追蹤FollowedMemberId被追蹤者FkNavigations).Load();
            _context.Entry(m).Collection(x => x.Follow追蹤FollowerMemberId追蹤者FkNavigations).Load();
            var item = this._mapper.Map<Member會員, AuthorListItemResultModel>(m);
            item.RecipeCount = m.Recipe食譜s.Count();
            item.FollowCount = m.Follow追蹤FollowerMemberId追蹤者FkNavigations.Count();
            item.FanCount = m.Follow追蹤FollowedMemberId被追蹤者FkNavigations.Count();
            item.AuthorIsFollowed = m.Follow追蹤FollowedMemberId被追蹤者FkNavigations.Any(x => x.FollowerMemberId追蹤者Fk == id);
            return item;
        }

    }
    /// <summary>
    /// 採用des加密
    /// </summary>
    /// <returns></returns>
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> CreateAsync(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        } 
    }

    /// <summary>
    /// Des加密
    /// </summary>
    public class DesCrypto {

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="original">要加密的資料</param>
        /// <param name="key">ASCII</param>
        /// <param name="iv">ASCII</param>
        /// <returns></returns>
        public string DesEncrypt(string original, string key,string iv) {
            string result = "";
            var des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = Encoding.ASCII.GetBytes(iv);

            byte[] dataByteOriginal = Encoding.UTF8.GetBytes(original);
            using (var ms = new MemoryStream()) {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteOriginal, 0, dataByteOriginal.Length);
                    cs.FlushFinalBlock();
                    result = Convert.ToBase64String(ms.ToArray());
                    return result;
                }
            }

        }


        /// <summary>   
        /// DES 解密字串   
        /// </summary>   
        /// <param  name="hexString">被加密字串</param>   
        /// <param  name="key">Key，長度必須為 8 個 ASCII 字元</param>   
        /// <param  name="iv">IV，長度必須為 8 個 ASCII 字元</param>   
        /// <returns>解密結果</returns>   
        public string DesDecrypt(string hexString, string key, string iv)
        {
            string decrypt = hexString;

                var des = new DESCryptoServiceProvider();
                des.Key = Encoding.ASCII.GetBytes(key);
                des.IV = Encoding.ASCII.GetBytes(iv);
                byte[] dataByteArray = Convert.FromBase64String(hexString);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataByteArray, 0, dataByteArray.Length);
                        cs.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(ms.ToArray());
                        return decrypt;
                    }
                }
        }


            
    }
}

