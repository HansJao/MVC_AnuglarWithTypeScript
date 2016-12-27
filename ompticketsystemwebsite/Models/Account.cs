using MvcPaging;
using System;
using System.Collections.Generic;
using TicketSystem.DbLib.Entities;

namespace ompticketsystemwebsite.Models
{
    public class AccountView
    {
        public IPagedList<TicketUser> TicketUserList { get; set; }
        public int Page { get; set; }
        public AccountView()
        {
            Page = 0;
        }

    }
    public class Account
    {

        public SessionData IsLogin(string UserID, string Password)
        {
            SessionData LoginSession = new SessionData();


            TicketUser userdata = TicketUsers.GetTicketUser(UserID);

            string Md5password = Common.CalculateMD5Hash(Password);
            if (userdata != null)
                //成功導頁
                if (UserID == userdata.Userid && Md5password == userdata.Password)
                {
                    LoginSession.IsLogin = true;
                    LoginSession.UserInfo = userdata;
                }

            return LoginSession;
        }

        public TicketUser GetTicketUser(string userid)
        {

            return TicketUsers.GetTicketUser(userid);
        }


        /// <summary>
        /// 取得使用者清單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TicketUser> GetTicketUserList()
        {
            return TicketUsers.GetTicketUsers();

        }

        /// <summary>
        ///新增使用者
        /// </summary>
        /// <param name="Userdata"></param>
        /// <returns></returns>        
        public bool InsertUserdata(TicketUser Userdata)
        {
            bool resault = false;
            //確認是否已有ID 
            TicketUser userdata = TicketUsers.GetTicketUser(Userdata.Userid);
            if (userdata == null)
            {

                string password = Userdata.Password;

                Userdata.Status = 1;
                Userdata.Createtime = DateTime.Now;
                //加密
                Userdata.Password = Common.CalculateMD5Hash(password);

                resault = TicketUsers.InsertTicketUser(Userdata);
            }

            return resault;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Userdata"></param>
        /// <returns></returns>
        public bool UPDateUserdata(TicketUser Userdata)
        {
            //修改資料不改密碼故不重新加密
            //Userdata.Password = Common.CalculateMD5Hash(Userdata.Password);
            if (TicketUsers.UpdateTicketUser(Userdata) == true)
            {
                return true;
            }

            return false;
        }

        public bool DeleteUser(string userid)
        {
            return TicketUsers.DeleteTicketUser(userid);

        }

    }
}