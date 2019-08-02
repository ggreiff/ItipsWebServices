// ***********************************************************************
// Assembly         : DitprService
// Author           : ggreiff
// Created          : 12-31-2017
//
// Last Modified By : ggreiff
// Last Modified On : 01-01-2018
// ***********************************************************************
// <copyright file="GlobalVariables.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using OppmHelper.Utility;
using Quartz;

namespace ItipsWebServices.Utility
{
    /// <summary>
    /// Class GlobalVariables.
    /// </summary>
    public static class GlobalVariables
    {
        //public static MapperHelper Mapper { get; set; }

        /// <summary>
        /// Gets or sets the executing directory.
        /// </summary>
        /// <value>The executing directory.</value>
        public static String ExecutingDirectory { get; set; }

        /// <summary>
        /// Gets or sets the XML dump directory.
        /// </summary>
        /// <value>The XML dump directory.</value>
        public static String XmlDumpDirectory { get; set; }
        
        public static IScheduler Scheduler { get; set; }

        public static Schedules JobSchedules { get; set; }

        public static String ScratchPad { get; set; }
    }
}
