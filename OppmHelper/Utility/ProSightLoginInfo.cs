using System;
using System.Reflection;
using System.Text;
using wsPortfoliosUser;

namespace OppmHelper.Utility
{
    /// <summary>
    /// This class holds ProSight login information for a specific user.
    /// </summary>
    public class ProSightLoginInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProSightLoginInfo"/> class.
        /// </summary>
        public ProSightLoginInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProSightLoginInfo"/> class.
        /// </summary>
        /// <param name="psUserInfo">The ps user info.</param>
        public ProSightLoginInfo(psPortfoliosUserInfo psUserInfo)
        {
            PsUserInfo = psUserInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProSightLoginInfo"/> class.
        /// </summary>
        /// <param name="loginToken">The login token.</param>
        public ProSightLoginInfo(String loginToken)
        {
            LoginToken = loginToken;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProSightLoginInfo"/> class.
        /// </summary>
        /// <param name="webUserId">The web user id.</param>
        public ProSightLoginInfo(Int32 webUserId)
        {
            WebUserId = webUserId;
        }

        /// <summary>
        /// Gets or sets the login token.
        /// </summary>
        /// <value>
        /// The login token.
        /// </value>
        public String LoginToken { get; set; }

        /// <summary>
        /// Gets or sets the login time.
        /// </summary>
        /// <value>
        /// The login time.
        /// </value>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// Gets or sets the web user ID.
        /// </summary>
        /// <value>
        /// The web user ID.
        /// </value>
        public Int32? WebUserId { get; set; }

        /// <summary>
        /// Gets or sets the ps user info.
        /// </summary>
        /// <value>
        /// The ps user info.
        /// </value>
        public psPortfoliosUserInfo PsUserInfo { get; set; }

        /// <summary>
        /// Haves the login token.
        /// </summary>
        /// <returns></returns>
        public Boolean HaveLoginToken()
        {
            return LoginToken.IsNotNullOrEmpty();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder("\n");
            Type reportSettings = GetType();
            PropertyInfo[] props = reportSettings.GetProperties();
            foreach (PropertyInfo p in props)
            {
                var pName = p.Name;
                var pValue = p.GetValue(this, null);
                sb.AppendFormat("{0}= {1}\n", pName, pValue);
            }
            return sb.ToString();
        }
    }
}