using TicketSystem.DbLib.Entities;

namespace ompticketsystemwebsite.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string UseAcc { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class SessionData
    {
        public bool IsLogin { get; set; }
        public TicketUser UserInfo = new TicketUser();

    }
}