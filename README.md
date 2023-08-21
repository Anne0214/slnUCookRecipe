# UCook食譜平台
## 前情提要
此專案為作者在資展國際(原資策會)的就業養成班時期完成之作品。原專案有與另一名工程師合作，在本Repository中，僅包含作者個人實作部分。
## 專案簡介
UCook是一個食譜分享平台，優化食譜的搜尋與發表過程，讓料理變得更輕鬆。
## 上手指南
以下指南將幫助你快速在本機建立和運行本項目。
### 環境要求
- Visual Studio
- SQL Server
- Memurai  [此處下載，Developer Edition即可，安裝過程請確保Port為6379](https://www.memurai.com/get-memurai)
### 安裝步驟
1. Clone the repo
   ```
   git clone https://github.com/shaojintian/Best_README_template.git
   ```
1. Clone Api repo
   ```
   git clone https://github.com/shaojintian/Best_README_template.git
   ```
1. 在MSSQL Server Management Studio執行本Repo中的`ProjectUCookDatabase.sql`指令檔，建立資料庫
2. 詢問作者API Key後，將API Key填入 `appsetting.json`中的`ChatGPTAPIKey`的value
### 測試注意事項
1. 需先執行API專案後，複製該專案URL，再貼入本專案的`appsetting.json`中的`ApiHost`的value，否則無法測試追蹤、按讚等功能。
2. 測試時API專案及本專案必須同時在運作狀態。

## 專案說明
### 功能介紹
- 使用者可註冊、登入。
- 使用者可以發布、修改、刪除食譜。
- 創建食譜時，可以輸入三個關鍵字，讓AI幫忙完成符合該三個關鍵字的食譜簡述。(使用ChatGPT的API)
- 使用者可搜尋食譜，搜尋時可用該食譜的關鍵字、用到的食材名稱、製作時間、份量等資料做查詢，並以發佈時間、按讚數等作排序。
- 使用者可對食譜按讚。
- 使用者可追蹤他人，得到他人發布食譜的通知。
- 當使用者的發布的食譜收到讚或被他人追蹤時會收到通知。
- 若被通知者在線，會有toast出現，告知被通知者有新通知。 
- 可以查看前一日的最新菜價(每天早上八點會打API取得最新菜價，並存進Memurai中，加快存取速度)
### 使用技術
- 架構: ASP.NET Core MVC
- 資料庫:MSSQL
- 資料存取套件: Entity Framework Core, Dapper
- 即時Web功能: SignalR
- 排程: Coravel
- 身份驗證:Cookie
- 前端:HTML,CSS,JavaScript,jQuery
- API:RESTful API
- 版本控管: git,github
- 第三方API: Imgur、ChatGPT(Chat Completions API)、農業部資料開放平台
- 伺服器端快取: Memurai
### 作者
**Anne**
做了兩年多的PM，轉職當後端工程師，這是我的第一個專案，歡迎賜教!

