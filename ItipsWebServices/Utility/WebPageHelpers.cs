// ***********************************************************************
// Assembly         : BMcD
// Author           : Gene Greiff
// Created          : 02-02-2015
//
// Last Modified By : Gene Greiff
// Last Modified On : 02-04-2015
// ***********************************************************************
// <copyright file="WebPageHelpers.cs" company="NetImpact Strategies Inc.">
//     Copyright (c) NetImpact Strategies Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using OppmHelper.Utility;

namespace ItipsWebServices.Utility
{
    /// <summary>
    /// Class WebPageHelpers.
    /// </summary>
    public class WebPageHelpers
    {
        /// <summary>
        /// The global theme
        /// </summary>
        public static readonly ApplicationVariable<String> GlobalTheme = new ApplicationVariable<String>("GlobalTheme");


        /// <summary>
        /// Getmaxes the length of the request.
        /// </summary>
        /// <returns>Int32.</returns>
        public static Int32 GetmaxRequestLength()
        {
            // Set the maximum file size for uploads in bytes.
            var section = ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
            // return length converted to kbytes or return default value as specified
            return (Int32)Math.Round((decimal)(section != null ? (double)section.MaxRequestLength * 1024 / 1000 : 5.120), 2);
        }

        /// <summary>
        /// Gets the page theme.
        /// </summary>
        /// <returns>String.</returns>
        public static String GetPageTheme()
        {
            if (GlobalTheme.HasValue) return GlobalTheme.Value;

            //
            // Check the web.config
            //
            if (!(ConfigurationManager.GetSection("system.web/pages") is PagesSection section) || section.Theme.IsNullOrEmpty()) return String.Empty;
            return section.Theme;
        }

        /// <summary>
        /// Includes the CSS.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="cssfile">The cssfile.</param>
        public static void IncludeCss(Page page, String cssfile)
        {
            var child = new HtmlGenericControl("link");
            child.Attributes.Add("rel", "stylesheet");
            child.Attributes.Add("href", cssfile);
            child.Attributes.Add("type", "text/css");
            page.Header.Controls.AddAt(0, child);
        }

        /// <summary>
        /// Includes the js file.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="jsfile">The jsfile.</param>
        public static void IncludeJsFile(Page page, String jsfile)
        {
            var child = new HtmlGenericControl("script");
            child.Attributes.Add("type", "text/javascript");
            child.Attributes.Add("src", jsfile);
            page.Header.Controls.Add(child);
        }

        /// <summary>
        /// Includes the js script.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="script">The script.</param>
        public static void IncludeJsScript(Page page, String script)
        {
            var child = new HtmlGenericControl("script");
            child.Attributes.Add("type", "text/javascript");
            child.InnerHtml = script;
            page.Header.Controls.Add(child);
        }

        /// <summary>
        /// Includes the page js script.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="script">The script.</param>
        public static void IncludePageJsScript(Page page, String script)
        {
            var child = new HtmlGenericControl("script");
            child.Attributes.Add("type", "text/javascript");
            child.InnerHtml = script;
            page.Controls.Add(child);
        }
    }
}