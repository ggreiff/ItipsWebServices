// ***********************************************************************
// Assembly         : OppmHelper
// Author           : ggreiff
// Created          : 02-04-2015
//
// Last Modified By : ggreiff
// Last Modified On : 02-08-2015
// ***********************************************************************
// <copyright file="WebServiceCredentials.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Xml.Serialization;

namespace OppmHelper.Utility
{
    /// <summary>
    /// Class WebServiceCredentials.
    /// </summary>
    public class WebServiceCredentials
    {
        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        public String ServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public String UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public String Password { get; set; }

        /// <summary>
        /// Gets or sets the use SSL.
        /// </summary>
        /// <value>The use SSL.</value>
        public Boolean UseSsl { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceCredentials" /> class.
        /// </summary>
        public WebServiceCredentials()
        {
            UseSsl = false;
        }

        /// <summary>
        /// News the credentials.
        /// </summary>
        /// <returns>WebServiceCredentials.</returns>
        public static WebServiceCredentials NewCredentials()
        {
            return new WebServiceCredentials();
        }

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns>String.</returns>
        public String Serialize()
        {
            String serializedData;
            var serializer = new XmlSerializer(GetType());
            using (var sw = new StringWriter())
            {
                serializer.Serialize(sw, this);
                serializedData = sw.ToString();
            }
            return serializedData;
        }


        /// <summary>
        /// Deserializes the specified serialized web service credentials.
        /// </summary>
        /// <param name="serializedWebServiceCredentials">The serialized web service credentials.</param>
        /// <returns>WebServiceCredentials.</returns>
        public static WebServiceCredentials Deserialize(String serializedWebServiceCredentials)
        {
            WebServiceCredentials webServiceCredentials;
            var deserializer = new XmlSerializer(typeof(WebServiceCredentials));
            using (TextReader tr = new StringReader(serializedWebServiceCredentials))
            {
                webServiceCredentials = (WebServiceCredentials)deserializer.Deserialize(tr);
            }
            return webServiceCredentials;
        }
    }
}
