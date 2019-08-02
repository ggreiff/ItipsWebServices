using System;
using System.Web;

namespace ItipsWebServices.Utility
{
    public class ApplicationVariable<T>
    {
        private readonly Func<T> _initializer;
        private readonly String _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationVariable&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public ApplicationVariable(String key)
        {
            if (String.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            _key = GetType() + key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationVariable&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="initializer">The initializer.</param>
        public ApplicationVariable(String key, Func<T> initializer)
            : this(key)
        {
            _initializer = initializer ?? throw new ArgumentNullException(nameof(initializer));
        }

        /// <summary>
        /// Gets the current application.
        /// </summary>
        public static HttpApplicationState CurrentApplication
        {
            get
            {
                var current = HttpContext.Current;
                if (current == null) throw new InvalidOperationException("No HttpContext is not available.");

                var application = current.Application;
                if (application == null) throw new InvalidOperationException("No application available on current HttpContext.");
                return application;
            }
        }

        /// <summary>
        /// Indicates wether there is a value present or not.
        /// </summary>
        public bool HasValue => GetInternalValue(false) != null;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value
        {
            get
            {
                var v = GetInternalValue(true);

                if (v == null) throw new InvalidOperationException("The application does not contain any value for ‘" + _key + "‘.");
                return (T)v;
            }
            set => CurrentApplication[_key] = value;
        }

        /// <summary>
        /// Gets the value in the current session or if
        /// none is available <c>default(T)</c>.
        /// </summary>
        public T ValueOrDefault
        {
            get
            {
                var v = GetInternalValue(true);
                if (v == null) return default(T); 
                return (T)v;
            }
        }

        private Object GetInternalValue(bool initializeIfNessesary)
        {
            var applicationState = CurrentApplication;

            var value = applicationState[_key];

            if (value == null && initializeIfNessesary
                && _initializer != null)
                applicationState[_key] = value = _initializer();

            return value;
        }

        /// <summary>
        /// Clears the value in the current application.
        /// </summary>
        public void Clear()
        {
            CurrentApplication.Remove(_key);
        }
    }
}