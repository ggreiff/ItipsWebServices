// ***********************************************************************
// Assembly         : BMcD
// Author           : Gene Greiff
// Created          : 05-17-2015
//
// Last Modified By : Gene Greiff
// Last Modified On : 05-17-2015
// ***********************************************************************
// <copyright file="HelperProSightEmail.cs" company="NetImpact Strategies Inc.">
//     Copyright (c) NetImpact Strategies Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using NLog;
using OppmHelper.Utility;

namespace ItipsWebServices.Utility
{
    /// <summary>
    /// Class HelperProSightEmail.
    /// </summary>
    public class HelperProSightEmail
    {

        /// <summary>
        /// The ps logger
        /// </summary>
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The scheduler
        /// </summary>
        /// <value>The SMTP mode.</value>
        public Int32 SmtpMode { get; set; }

        /// <summary>
        /// Gets or sets the name of the SMTP server.
        /// </summary>
        /// <value>The name of the SMTP server.</value>
        public String SmtpServerName { get; set; }

        /// <summary>
        /// Gets or sets the SMTP port.
        /// </summary>
        /// <value>The SMTP port.</value>
        public Int32 SmtpPort { get; set; }

        /// <summary>
        /// Gets or sets the SMTP authentication.
        /// </summary>
        /// <value>The SMTP authentication.</value>
        public Int32 SmtpAuthentication { get; set; }

        /// <summary>
        /// Gets or sets the name of the SMTP user.
        /// </summary>
        /// <value>The name of the SMTP user.</value>
        public String SmtpUserName { get; set; }

        /// <summary>
        /// Gets or sets the SMTP password.
        /// </summary>
        /// <value>The SMTP password.</value>
        public String SmtpPassword { get; set; }

        /// <summary>
        /// Gets or sets the SMTP pickup directory.
        /// </summary>
        /// <value>The SMTP pickup directory.</value>
        public String SmtpPickupDirectory { get; set; }

        /// <summary>
        /// Gets or sets the SMTP connection timeout.
        /// </summary>
        /// <value>The SMTP connection timeout.</value>
        public Int32 SmtpConnectionTimeout { get; set; }

        /// <summary>
        /// Gets or sets the SMTP use SSL.
        /// </summary>
        /// <value>The SMTP use SSL.</value>
        public Boolean SmtpUseSsl { get; set; }

        /// <summary>
        /// Gets or sets the SMTP from email.
        /// </summary>
        /// <value>The SMTP from email.</value>
        public String SmtpFromEmail { get; set; }

        /// <summary>
        /// Gets or sets the name of the STMP from.
        /// </summary>
        /// <value>The name of the STMP from.</value>
        public String StmpFromName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HelperProSightEmail" /> class.
        /// </summary>
        public HelperProSightEmail()
        {
            SmtpMode = 1;
            SmtpServerName = "localhost";
            SmtpPort = 25;
            SmtpAuthentication = 0;
            SmtpUserName = String.Empty;
            SmtpPassword = String.Empty;
            SmtpPickupDirectory = @"c:\inetpub\mailroot\pickup";
            SmtpConnectionTimeout = 60;
            SmtpUseSsl = false;
            SmtpFromEmail = "admin@prosight.com";
            StmpFromName = "ProSight Administrator";
        }

        /// <summary>
        /// News the helper pro sight email.
        /// </summary>
        /// <returns>HelperProSightEmail.</returns>
        public static HelperProSightEmail NewHelperProSightEmail()
        {
            return new HelperProSightEmail();
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="toEmail">To email.</param>
        /// <param name="toName">To name.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="emailBody">The email body.</param>
        /// <param name="attachments">The attachments.</param>
        /// <returns>Boolean.</returns>
        public Boolean SendEmail(String toEmail, String toName, String subject, String emailBody, List<String> attachments)
        {
            return SendEmail(SmtpFromEmail, StmpFromName, toEmail, toName, subject, emailBody, false, attachments, SmtpServerName, SmtpPort, SmtpUseSsl, SmtpUserName, SmtpPassword);
        }

        /// <summary>
        /// method to send email using SMTP server
        /// </summary>
        /// <param name="fromEmail">From email address</param>
        /// <param name="fromName">From name</param>
        /// <param name="toEmail">To email address</param>
        /// <param name="toName">To name</param>
        /// <param name="subject">Email subject</param>
        /// <param name="emailBody">Email message body</param>
        /// <param name="isBodyHtml">Is email message body HTML</param>
        /// <param name="attachments">Email message file attachments</param>
        /// <param name="emailServer">SMTP email server address</param>
        /// <param name="port">The port.</param>
        /// <param name="enableSsl">if set to <c>true</c> [enable SSL].</param>
        /// <param name="loginName">SMTP email server login name</param>
        /// <param name="loginPassword">SMTP email server login password</param>
        /// <returns>TRUE if the email sent successfully, FALSE otherwise</returns>
        public Boolean SendEmail(String fromEmail, String fromName, String toEmail, String toName, String subject, String emailBody,
            Boolean isBodyHtml, List<String> attachments, String emailServer, Int32 port, Boolean enableSsl, String loginName, String loginPassword)
        {
            try
            {
                using (var smtpClient = new SmtpClient {Host = emailServer, DeliveryMethod = SmtpDeliveryMethod.Network, Port = port, EnableSsl = enableSsl})
                {
                    if (SmtpMode == 1)
                    {
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtpClient.PickupDirectoryLocation = SmtpPickupDirectory;
                    }
                    using (var mailMessage = new MailMessage {From = new MailAddress(fromEmail, fromName)})
                    {
                        mailMessage.To.Add(new MailAddress(toEmail, toName));
                        mailMessage.Subject = subject;
                        mailMessage.Body = emailBody;
                        mailMessage.IsBodyHtml = isBodyHtml;

                        foreach (var attachment in attachments)
                            mailMessage.Attachments.Add(new Attachment(attachment));


                        if (loginName.IsNotNullOrEmpty() && loginPassword.IsNotNullOrEmpty())
                        {
                            var networkCredential = new NetworkCredential(loginName, loginPassword);
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = networkCredential;
                        }

                        smtpClient.Send(mailMessage);
                        mailMessage.Attachments.ToList().ForEach(a => a.Dispose());
                    }
                }
            }
            catch (Exception ex)
            {
                PsLogger.Error(ex.InnerException);
                return false;
            }
            return true;
        }
    }
}