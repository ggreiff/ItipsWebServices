using System;
using System.Security.Cryptography.X509Certificates;
using NLog;
using OppmHelper.Utility;
using wsPortfoliosUser;


namespace OppmHelper.OppmApi
{
    public class SePortfolioUser
    {
        /// <summary>
        /// The ps logger
        /// </summary>
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets or sets the ps security login.
        /// </summary>
        /// <value>The ps security login.</value>
        public SePortfolioSecurity PsSecurityLogin { get; set; }

        /// <summary>
        /// Gets or sets the ps user.
        /// </summary>
        /// <value>The ps user.</value>
        public psPortfoliosUser PsUser { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="SePortfolioUser"/> class.
        /// </summary>
        /// <param name="securityLogin">The security login.</param>
        public SePortfolioUser(SePortfolioSecurity securityLogin)
        {
            PsSecurityLogin = securityLogin;
            PsUser = new psPortfoliosUser { CookieContainer = PsSecurityLogin.CookieContainer };
            PsUser.Url = PsSecurityLogin.GetWebServicesUrLbyType(PsUser);

            if (!securityLogin.PortfoliosSecurityWs.ClientCertificates.HasItems()) return;
            foreach (var clientCertificate in securityLogin.PortfoliosSecurityWs.ClientCertificates)
            {
                PsUser.ClientCertificates.Add(clientCertificate);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SePortfolioUser"/> class.
        /// </summary>
        /// <param name="securityLogin">The security login.</param>
        /// <param name="certificate">The certificate.</param>
        public SePortfolioUser(SePortfolioSecurity securityLogin, X509Certificate certificate)
            : this(securityLogin)
        {
            PsUser.ClientCertificates.Add(certificate);
        }

        public psPortfoliosUserInfo GetUserInfo(Int32 id)
        {
            try
            {
                return PsUser.GetUserInfo(id);
            }
            catch
            {
                return null;
            }
            
        }

        public psPortfoliosUserInfo GetUserInfoByLogin(String userToken)
        {
            return GetUserInfo(GetUserIdByLogin(userToken));
        }

        public Int32 GetUserIdByLogin(String userToken)
        {
            try
            {
                return PsUser.GetUserIDByLogin(userToken);
            }
            catch
            {
                return -1;
            }
        }
    }

}
