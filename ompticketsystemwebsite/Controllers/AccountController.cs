using MvcPaging;
using ompticketsystemwebsite.Filter;
using ompticketsystemwebsite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Text;
using System.Web.Mvc;
using TicketSystem.DbLib.Entities;

namespace ompticketsystemwebsite.Controllers
{
    public class AccountController : Controller
    {
        private Account Acc = new Account();
        private int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //[Route("/peopletest")]
        public JsonResult GetPeople(string test)
        {
            var people = new List<Person>()
            {
                new Person { Id = 1, FirstName = "John", LastName = test },
                new Person { Id = 1, FirstName = "Mary", LastName = "Jane" },
                new Person { Id = 1, FirstName = "Bob", LastName = "Parker" }
            };

            return Json(people);
        }

        [HttpPost]
        /// <summary>
        /// 驗證登入
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult Login(string username, string password, string captch)
        {

            if (captch != null && captch.ToUpper().Equals(Session["ValidateCode"].ToString().ToUpper()))
            {
                SessionData LoginSession = Acc.IsLogin(username, password);
                if (LoginSession.IsLogin == true)
                //成功導頁
                {
                    Session["LoginData"] = LoginSession;
                    ViewBag.Username = LoginSession.UserInfo.Username;
                    //改用 HttpContext--
                    HttpContext.Session.Add("LoginData", LoginSession);
                    return RedirectToAction("Index", "Home");

                }

                TempData["Msg"] = "輸入帳號密碼不正確";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Msg"] = "輸入驗證碼不正確";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            //清資料 
            Session["LoginData"] = null;

            return RedirectToAction("Index");
        }

        [PermissionCheck]
        public ActionResult AccountManager()
        {
            //取得資料
            AccountView viewModel = new AccountView();
            viewModel.TicketUserList = Acc.GetTicketUserList().ToPagedList(viewModel.Page > 0 ? viewModel.Page - 1 : 0, PageSize);
            return View(viewModel);
        }
        [HttpPost]
        [PermissionCheck]
        public ActionResult AccountManager(AccountView viewModel)
        {
            //取得資料
            viewModel.TicketUserList = Acc.GetTicketUserList().ToPagedList(viewModel.Page > 0 ? viewModel.Page - 1 : 0, PageSize);
            return View(viewModel);
        }

        [PermissionCheck]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="userdata"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(TicketUser Accdata)
        {
            //1.建立者 寫DB                
            SessionData SessionAcc = (SessionData)HttpContext.Session["LoginData"];
            Accdata.Creator = SessionAcc.UserInfo.Userid;


            bool resault = Acc.InsertUserdata(Accdata);
            //2.導回List頁
            if (resault) { return RedirectToAction("AccountManager"); }

            TempData["Msg"] = "輸入帳號資料重複";
            return View(Accdata);
        }

        [PermissionCheck]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketUser Userdata = Acc.GetTicketUser(id);
            return View(Userdata);
        }
        /// <summary>
        /// 使用者資料更新
        /// </summary>
        /// <param name="Userdata"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(TicketUser Userdata)
        {
            bool resulat = Acc.UPDateUserdata(Userdata);
            if (resulat) return RedirectToAction("AccountManager");

            TempData["Msg"] = "資料更新失敗";
            return View(Userdata);
        }
        [PermissionCheck]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketUser Userdata = Acc.GetTicketUser(id);
            if (Userdata == null)
            {
                return HttpNotFound();
            }
            TempData["Msg"] = "資料異常，刪除失敗";
            return View(Userdata);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Acc.DeleteUser(id);
            return RedirectToAction("AccountManager");

        }
        public ActionResult Deleteitem(string id)
        {
            Acc.DeleteUser(id);
            return RedirectToAction("AccountManager");

        }

        #region  產生驗證碼
        public void VerificationCode()
        {
            // 是否產生驗證碼
            bool isCreate = true;

            // Session["CreateTime"]: 建立驗證碼的時間
            if (Session["CreateTime"] == null)
            {
                Session["CreateTime"] = DateTime.Now;
            }
            else
            {
                DateTime startTime = Convert.ToDateTime(Session["CreateTime"]);
                DateTime endTime = Convert.ToDateTime(DateTime.Now);
                TimeSpan ts = endTime - startTime;


                // 重新產生驗證碼的間隔
                if (ts.Minutes > 15)
                {
                    isCreate = true;
                    Session["CreateTime"] = DateTime.Now;
                }
                else
                {
                    isCreate = false;
                }
            }

            Response.ContentType = "image/gif";
            //建立 Bitmap 物件和繪製
            Bitmap basemap = new Bitmap(200, 60);
            Graphics graph = Graphics.FromImage(basemap);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 60);
            Font font = new Font(FontFamily.GenericSerif, 48, FontStyle.Bold, GraphicsUnit.Pixel);
            Random random = new Random();
            // 英數
            string letters = "ABCDEFGHIJKLMNPQRSTUVWXYZ0123456789";
            // 天干地支生肖
            //string letters = "甲乙丙丁戊己庚辛壬癸子丑寅卯辰巳午未申酉戍亥鼠牛虎免龍蛇馬羊猴雞狗豬";
            string letter;
            StringBuilder sb = new StringBuilder();


            //if (isCreate)
            //{
            // 加入隨機二個字
            // 英文4 ~ 5字，中文2 ~ 3字
            for (int word = 0; word < 4; word++)
            {
                letter = letters.Substring(random.Next(0, letters.Length - 1), 1);
                sb.Append(letter);


                // 繪製字串 
                graph.DrawString(letter, font, new SolidBrush(Color.Black), word * 38, random.Next(0, 15));
            }
            //}
            //else
            //{
            //    // 使用先前的驗證碼來產生
            //    string currentCode = Session["ValidateCode"].ToString();
            //    sb.Append(currentCode);

            //    foreach (char item in currentCode)
            //    {
            //        letter = item.ToString();
            //        // 繪製字串
            //        graph.DrawString(letter, font, new SolidBrush(Color.Black), currentCode.IndexOf(item) * 38, random.Next(0, 15));
            //    }
            //}


            // 混亂背景
            //Pen linePen = new Pen(new SolidBrush(Color.Black), 2);
            //for (int x = 0; x < 10; x++)
            //{
            //    graph.DrawLine(linePen, new Point(random.Next(0, 199), random.Next(0, 59)), new Point(random.Next(0, 199), random.Next(0, 59)));
            //}

            // 儲存圖片並輸出至stream      
            basemap.Save(Response.OutputStream, ImageFormat.Gif);
            // 將產生字串儲存至 Sesssion
            Session["ValidateCode"] = sb.ToString();
            Response.End();
        }
        #endregion
    }
}