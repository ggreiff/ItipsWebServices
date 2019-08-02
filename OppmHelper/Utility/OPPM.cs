// ***********************************************************************
// Assembly         : OppmUtility
// Author           : ggreiff
// Created          : 08-27-2014
//
// Last Modified By : ggreiff
// Last Modified On : 02-09-2015
// ***********************************************************************
// <copyright file="OPPM.cs" company="NetImpact Strategies Inc.">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Security.Cryptography.X509Certificates;
using NLog;
using OppmHelper.OppmApi;

namespace OppmHelper.Utility
{
    /// <summary>
    /// Class Oppm.
    /// </summary>
    public class Oppm
    {
        /// <summary>
        /// Gets or sets the se security.
        /// </summary>
        /// <value>The se security.</value>
        public SePortfolioSecurity SeSecurity { get; set; }

        /// <summary>
        /// Gets or sets the se porfolio.
        /// </summary>
        /// <value>The se porfolio.</value>
        public SePortfoliosPortfolio SePorfolio { get; set; }

        /// <summary>
        /// Gets or sets the se item.
        /// </summary>
        /// <value>The se item.</value>
        public SePortfolioItem SeItem { get; set; }

        /// <summary>
        /// Gets or sets the se category.
        /// </summary>
        /// <value>The se category.</value>
        public SePortfolioCategory SeCategory { get; set; }

        /// <summary>
        /// Gets or sets the se value list.
        /// </summary>
        /// <value>The se value list.</value>
        public SePortfoliosValueList SeValueList { get; set; }

        /// <summary>
        /// Gets or sets the se cell.
        /// </summary>
        /// <value>The se cell.</value>
        public SePortfolioCell SeCell { get; set; }

        /// <summary>
        /// Gets or sets the se sub item.
        /// </summary>
        /// <value>The se sub item.</value>
        public SePortfolioSubItem SeSubItem { get; set; }

        /// <summary>
        /// Gets or sets the se user.
        /// </summary>
        /// <value>The se user.</value>
        public SePortfolioUser SeUser { get; set; }

        /// <summary>
        /// Gets or sets the certificate.
        /// </summary>
        /// <value>The certificate.</value>
        public X509Certificate Certificate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public String User { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public String Password { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public String Server { get; set; }

        /// <summary>
        /// Gets or sets the use SSL.
        /// </summary>
        /// <value>The use SSL.</value>
        public Boolean UseSsl { get; set; }

        /// <summary>
        /// Gets or sets the logged in.
        /// </summary>
        /// <value>The logged in.</value>
        public Boolean LoggedIn { get; set; }


        /// <summary>
        /// The ps logger
        /// </summary>
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Initializes a new instance of the <see cref="Oppm" /> class.
        /// </summary>
        public Oppm()
        {
            try
            {
                SeSecurity = new SePortfolioSecurity();
            }
            catch (Exception ex)
            {
                PsLogger.Error(ex.InnerException);
                PsLogger.Fatal(ex.Message);
            }
    }

        /// <summary>
        /// Initializes a new instance of the <see cref="Oppm"/> class.
        /// </summary>
        /// <param name="webServiceCredentials">The web service credentials.</param>
        public Oppm(WebServiceCredentials webServiceCredentials)
            : this(webServiceCredentials.UserName, webServiceCredentials.Password, webServiceCredentials.ServerName, webServiceCredentials.UseSsl, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Oppm" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="server">The server.</param>
        /// <param name="useSsl">The use SSL.</param>
        public Oppm(String username, String password, String server, Boolean useSsl)
            : this(username, password, server, useSsl, null)
        {


        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Oppm" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="server">The server.</param>
        public Oppm(String username, String password, String server)
            : this(username, password, server, false, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Oppm" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="server">The server.</param>
        /// <param name="useSsl">The use SSL.</param>
        /// <param name="certificate">The certificate.</param>
        public Oppm(String username, String password, String server, Boolean useSsl, X509Certificate certificate)
            : this()
        {
            try
            {
                User = username;
                Password = password;
                Server = server;
                UseSsl = useSsl;

                SeSecurity.User = User;
                SeSecurity.Password = Password;
                SeSecurity.ProSightServer = Server;
                if (certificate != null) SeSecurity.PortfoliosSecurityWs.ClientCertificates.Add(certificate);
                SeSecurity.ProSightServerUseSsl = UseSsl;
                SePorfolio = new SePortfoliosPortfolio(SeSecurity);
                SeItem = new SePortfolioItem(SeSecurity);
                SeCategory = new SePortfolioCategory(SeSecurity);
                SeValueList = new SePortfoliosValueList(SeSecurity);
                SeCell = new SePortfolioCell(SeSecurity);
                SeSubItem = new SePortfolioSubItem(SeSecurity);
                SeUser = new SePortfolioUser(SeSecurity);
            }
            catch (Exception ex)
            {
                PsLogger.Fatal(ex.InnerException);
                PsLogger.Fatal(ex.Message);
            }
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns>Boolean.</returns>
        public Boolean Login()
        {
            try
            {
                LoggedIn = SeSecurity.Login();
                if (LoggedIn) return LoggedIn;
                PsLogger.Fatal("Unable to login to {0} with {1}", SeSecurity.ProSightServer, SeSecurity.User);
                return false;
            }
            catch (Exception ex)
            {
                PsLogger.Fatal(ex.Message);
                PsLogger.Error(ex.InnerException);
            }
            return false;
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns>Boolean.</returns>
        public Boolean Logout()
        {
            return Logout(SeSecurity.SecurityToken);
        }

        /// <summary>
        /// Logouts the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Boolean.</returns>
        public Boolean Logout(String token)
        {
            LoggedIn = SeSecurity.Logout(token);
            return !LoggedIn;
        }

        /// <summary>
        /// Gets the web service credentials.
        /// </summary>
        /// <returns>WebServiceCredentials.</returns>
        public WebServiceCredentials GetWebServiceCredentials()
        {
            return new WebServiceCredentials { UserName = User, Password = Password, ServerName = Server, UseSsl = UseSsl };
        }

        /// <summary>
        /// Determines whether [is login session valid].
        /// </summary>
        public Boolean IsLoginSessionValid()
        {
            try
            {
                SeUser.PsUser.GetUserInfo(1);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
