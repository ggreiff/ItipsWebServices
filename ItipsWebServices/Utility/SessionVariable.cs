// ***********************************************************************
// Assembly         : BMcD
// Author           : Gene Greiff
// Created          : 02-02-2015
//
// Last Modified By : Gene Greiff
// Last Modified On : 02-02-2015
// ***********************************************************************
// <copyright file="SessionVariable.cs" company="NetImpact Strategies Inc.">
//     Copyright (c) NetImpact Strategies Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Web;
using System.Web.SessionState;

namespace ItipsWebServices.Utility
{
    /// <summary>
    /// Wrapper class for HttpContext.Session.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SessionVariable<T>
    {
        /// <summary>
        /// The _initializer
        /// </summary>
        private readonly Func<T> _initializer;
        /// <summary>
        /// The _key
        /// </summary>
        private readonly string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionVariable&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <exception cref="System.ArgumentNullException">key</exception>
        public SessionVariable(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            _key = GetType() + key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionVariable&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="initializer">The initializer.</param>
        /// <exception cref="System.ArgumentNullException">initializer</exception>
        public SessionVariable(string key, Func<T> initializer)
            : this(key)
        {
            if (initializer == null)
                throw new ArgumentNullException("initializer");

            _initializer = initializer;
        }

        /// <summary>
        /// Gets the current session.
        /// </summary>
        /// <value>The current session.</value>
        /// <exception cref="System.InvalidOperationException">
        /// No HttpContext is not available.
        /// or
        /// No Session available on current HttpContext.
        /// </exception>
        public static HttpSessionState CurrentSession
        {
            get
            {
                var current = HttpContext.Current;

                if (current == null)
                    throw new InvalidOperationException("No HttpContext is not available.");

                var session = current.Session;
                if (session == null)
                    throw new InvalidOperationException("No Session available on current HttpContext.");
                return session;
            }
        }

        /// <summary>
        /// Indicates wether there is a value present or not.
        /// </summary>
        /// <value><c>true</c> if this instance has value; otherwise, <c>false</c>.</value>
        public bool HasValue
        {
            get { return GetInternalValue(false) != null; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// <exception cref="System.InvalidOperationException">The session does not contain any value for ‘ + _key + ‘.</exception>
        public T Value
        {
            get
            {
                object v = GetInternalValue(true);

                if (v == null)
                    throw new InvalidOperationException("The session does not contain any value for ‘" + _key + "‘.");
                return (T) v;
            }
            set { CurrentSession[_key] = value; }
        }

        /// <summary>
        /// Gets the value in the current session or if
        /// none is available <c>default(T)</c>.
        /// </summary>
        /// <value>The value or default.</value>
        public T ValueOrDefault
        {
            get
            {
                object v = GetInternalValue(true);
                if (v == null)
                    return default(T);

                return (T) v;
            }
        }

        /// <summary>
        /// Gets the internal value.
        /// </summary>
        /// <param name="initializeIfNessesary">if set to <c>true</c> [initialize if nessesary].</param>
        /// <returns>System.Object.</returns>
        private object GetInternalValue(bool initializeIfNessesary)
        {
            var session = CurrentSession;

            object value = session[_key];

            if (value == null && initializeIfNessesary
                && _initializer != null)
                session[_key] = value = _initializer();

            return value;
        }

        /// <summary>
        /// Clears the value in the current session.
        /// </summary>
        public void Clear()
        {
            CurrentSession.Remove(_key);
        }
    }
}