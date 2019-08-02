using Microsoft.Win32;

namespace OppmHelper.Utility
{
    public class psRegistry
    {
        private const string REGKEY_ROOT_COMPANYNAME = "Oracle";

        private const string REGKEY_ROOT_PRODUCTNAME = "Primavera Portfolio Management";

        private const string REGKEY_ROOT_PLATFORMNAME = "Portfolios";

        private const string REGKEY_ROOT_ADDONNAME = "AddOn";

        private const string REGKEY_ROOT_PREFIX = "Software\\Oracle\\Primavera Portfolio Management\\Portfolios";

        private psRegistry()
        {
        }

        private static RegistryKey getKey(string iRegPath, bool iWritable)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(iRegPath, iWritable) ?? Registry.LocalMachine.CreateSubKey(iRegPath);
            return registryKey;
        }

        public static RegistryKey getRoot(bool iWritable)
        {
            return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios", iWritable);
        }

        public class AddOn
        {
            public const string REGKEY_ROOT_PATH = "Software\\Oracle\\Primavera Portfolio Management\\AddOn";

            public const string ADDON_MODULEID = "AddOn Module ID";

            public const string ADDON_MODULENAME = "AddOn Module Name";

            public const string ADDON_MODULEDESCRIPTION = "AddOn Module Description";

            public const string ADDON_MODULEGUID = "AddOn Module GUID";

            public const string ADDON_MODULEBUTTONNAME = "AddOn Module Button Name";

            public const string ADDON_MODULEBUTTONIMAGESPREFIX = "AddOn Module Button Images Prefix";

            public const string ADDON_MODULECOMMAND = "AddOn Module Command";

            public const string ADDON_MODULEMENUXMLFILENAME = "AddOn Module Menu XML Filename";

            public const string ADDON_MODULEVIRTUALDIRECTORY = "AddOn Virtual Directory";

            public const string ADDON_MODULEINSTALLPATH = "AddOn Install Path";

            public const string ADDON_EXEFORCHANGEDB = "AddOn ExeForChangeDB";

            public const string ADDON_VERSION = "AddOn Version";

            public const string ADDON_LINKICONPATH = "AddOn Link Icon Path";

            public const string ADDON_LINKHEADERPATH = "AddOn Link Header Path";

            public const string PM_BRIDGE_ADJUST_COST = "PM Bridge Adjust Cost";

            public const string PM_BRIDGE_ADJUST_COST_DEFAULT_VALUE = "0";

            public const string PM_BRIDGE_SHOW_ALL_P6_BUILT_IN_FIELDS = "PM Bridge Show All P6 Built In Fields";

            public const string PM_BRIDGE_SHOW_ALL_P6_BUILT_IN_FIELDS_DEFAULT_VALUE = "0";

            public const string PM_BRIDGE_MAX_JSON_LENGTH = "PM Bridge Max Json Length";

            public const string PM_BRIDGE_MAX_JSON_LENGTH_DEFAULT_VALUE = "2097152";

            public AddOn()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\AddOn", iWritable);
            }
        }

        public class Database
        {
            public const string ENTRY_DEFAULT_DSN = "DefaultDSN";

            public const string DEFAULT_DEFAULT_DSN = "";

            public const string ENTRY_CONNECTION_SUBTYPE = "Connection Subtype";

            public const string DEFAULT_CONNECTION_SUBTYPE = "tns";

            public const string ENTRY_LOCK_TIMEOUT = "SQLServer Lock Timeout";

            public const int DEFAULT_LOCK_TIMEOUT = 60000;

            public const string ENTRY_TRANSACTION_TIMEOUT = "Transaction Timeout";

            public const int DEFAULT_TRANSACTION_TIMEOUT = 3600;

            public const string ENTRY_ODP_ASSEMBLY_QUILIFIED_NAME = "ODP assembly quilified name";

            public const string ENTRY_SERVER_TYPE = "server type";

            public const string ENTRY_SERVER_VERSION = "server version";

            public const string ENTRY_SERVER = "Server";

            public const string ENTRY_SERVICE = "Service";

            public const string ENTRY_DATABASE = "Database";

            public const string ENTRY_LOGIN = "Login";

            public const string DEFAULT_SQL_CONNECTION_LIFETIME = "0";

            public const string DEFAULT_SQL_CONNECTION_TIMEOUT = "15";

            public const string DEFAULT_SQL_MAX_POOL_SIZE = "100";

            public const string DEFAULT_SQL_MIN_POOL_SIZE = "0";

            public const string DEFAULT_SQL_POOLING = "true";

            public const string DEFAULT_ORACLE_CONNECTION_LIFETIME = "0";

            public const string DEFAULT_ORACLE_CONNECTION_TIMEOUT = "15";

            public const string DEFAULT_ORACLE_DECR_POOL_SIZE = "1";

            public const string DEFAULT_ORACLE_INCR_POOL_SIZE = "5";

            public const string DEFAULT_ORACLE_MAX_POOL_SIZE = "100";

            public const string DEFAULT_ORACLE_MIN_POOL_SIZE = "0";

            public const string DEFAULT_ORACLE_POOLING = "true";

            public const string DEFAULT_VALIDATE_CONNECTION = "0";

            public const string DEFAULT_IS_RAC = "0";

            public const string ENTRY_CONNECTION_LIFETIME = "Connection Lifetime";

            public const string ENTRY_CONNECTION_TIMEOUT = "Connection Timeout";

            public const string ENTRY_DECR_POOL_SIZE = "Decr Pool Size";

            public const string ENTRY_INCR_POOL_SIZE = "Incr Pool Size";

            public const string ENTRY_MAX_POOL_SIZE = "Max Pool Size";

            public const string ENTRY_MIN_POOL_SIZE = "Min Pool Size";

            public const string ENTRY_POOLING = "Pooling";

            public const string ENTRY_VALIDATE_CONNECTION = "Validate Connection";

            public const string ENTRY_IS_RAC = "Is Oracle RAC";

            public const int DEFAULT_SQL_INLIST_BATCH_SIZE = 1000;

            public const int DEFAULT_ORACLE_INLIST_BATCH_SIZE = 1000;

            public const string SQL_INLIST_BATCH_SIZE = "SQL Server InList Batch Size";

            public const string ORACLE_INLIST_BATCH_SIZE = "Oracle InList Batch Size";

            public const string ENTRY_DB_TIME_DIFF_INTERVAL_NINUTES = "Time Sync Interval minutes";

            public const string DEFAULT_DB_TIME_DIFF_INTERVAL_NINUTES = "60";

            public const string REGKEY_ROOT_PATH = "\\Server\\Database";

            public const string XML_INSERT_FROM_DATATABLE_BATCH_SIZE = "XML Insert From Datatable Batch Size";

            public const string DEFAULT_XML_INSERT_FROM_DATATABLE_BATCH_SIZE = "10000";

            public const string XML_INSERT_FROM_COLLECTION_BATCH_SIZE = "XML Insert From Collection Batch Size";

            public const string DEFAULT_XML_INSERT_FROM_COLLECTION_BATCH_SIZE = "100000";

            public const string TREAT_ORACLE_9_AS_ORACLE_10 = "Treat Oracle 9 as Oracle 10 for queries";

            public const string DEFAULT_TREAT_ORACLE_9_AS_ORACLE_10 = "0";

            public const string EXCLUDE_TABLES_FROM_RECALCULATE_DB_STATS_SQL = "Exclude Tables From Recalculate DB Stats For SQL";

            public const string DEFAULT_EXCLUDE_TABLES_FROM_RECALCULATE_DB_STATS_SQL = "PS_EVENT_PROP,PS_TEMP_EVENTS_SUBSCRIPTIONS";

            public Database()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Server\\Database", iWritable);
            }

            public class ConnectionStrings
            {
                public const string REGKEY_ROOT_PATH = "\\Server\\Database\\Connections Strings List";

                public ConnectionStrings()
                {
                }

                public static RegistryKey getRoot(bool iWritable)
                {
                    return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Server\\Database\\Connections Strings List", iWritable);
                }
            }
        }

        public class Debug
        {
            private const string REGKEY_ROOT_PATH = "\\Debug";

            public const string ENTRY_DEBUG_ON = "Debug";

            public const int DEFAULT_DEBUG_ON = 0;

            public const string ENTRY_MAX_LOGFILE_SIZE = "MaxLogFileSize";

            public const int DEFAULT_MAX_LOGFILE_SIZE = 20000000;

            public const string ENTRY_HEADER_CLASS_ON = "HeaderPrintClass";

            public const int DEFAULT_HEADER_CLASS_ON = 1;

            public const string ENTRY_HEADER_METHOD_ON = "HeaderPrintMethod";

            public const int DEFAULT_HEADER_METHOD_ON = 1;

            public const string ENTRY_HEADER_FLAG_ON = "HeaderPrintFlags";

            public const int DEFAULT_HEADER_FLAG_ON = 0;

            public const string ENTRY_HEADER_TIME_ON = "HeaderPrintTime";

            public const int DEFAULT_HEADER_TIME_ON = 1;

            public const string ENTRY_HEADER_THREAD_ON = "HeaderPrintThread";

            public const int DEFAULT_HEADER_THREAD_ON = 0;

            public const string ENTRY_HEADER_PROCESS_ON = "HeaderPrintProcess";

            public const int DEFAULT_HEADER_PROCESS_ON = 1;

            public const string ENTRY_HEADER_CLIENT_ON = "HeaderPrintClient";

            public const int DEFAULT_HEADER_CLIENT_ON = 1;

            public const string ENTRY_HEADER_CONTEXT_ON = "HeaderPrintContext";

            public const int DEFAULT_HEADER_CONTEXT_ON = 1;

            public const string ENTRY_PRINT_STACK_TRACE_ON = "PrintStackTrace";

            public const int DEFAULT_PRINT_STACK_TRACE_ON = 0;

            public const string ENTRY_WAIT_BEFORE_FLAGS_REFRESH = "WaitBetweenFlagsRefresh";

            public const int DEFAULT_WAIT_BEFORE_FLAGS_REFRESH = 0;

            public const string ENTRY_LOG_FILES_DIRECTORY = "LogFilesDirectory";

            public const string ENTRY_PERFORMANCE_COLLECTOR_ON = "SavePerformanceStats";

            public const int DEFAULT_PERFORMANCE_COLLECTOR_ON = 1;

            public const string ENTRY_PERFORMANCE_COLLECTOR_INTERVAL_MINUTES = "SavePerformanceStatsInterval";

            public const int DEFAULT_PERFORMANCE_COLLECTOR_INTERVAL_MINUTES = 60;

            public const string ENTRY_PERFORMANCE_MONITORING_ON = "MonitorPerformanceStats";

            public const int DEFAULT_PERFORMANCE_MONITORING_ON = 0;

            public const string ENTRY_FUNCTION_ENGINE_PERFORMANCE = "Function Engine Performance";

            public const int DEFAULT_FUNCTION_ENGINE_PERFORMANCE = 0;

            public Debug()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Debug", iWritable);
            }

            public class Switches
            {
                public const string REGKEY_ROOT_PATH = "\\Debug\\Switches";

                public Switches()
                {
                }

                public static RegistryKey getRoot(bool iWritable)
                {
                    return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Debug\\Switches", iWritable);
                }
            }
        }

        public class EventHandler
        {
            public const string REGKEY_ROOT_PATH = "\\Server\\EventHandler";

            public const string BATCH_SIZE = "Event Handler Batch Size";

            public const int DEFAULT_BATCH_SIZE = 1000;

            public const string MAX_FETCH_COUNT = "Max Fetch Count";

            public const int DEFAULT_MAX_FETCH_COUNT = 20;

            public const string SECONDS_BETWEEN_FETCHS = "Seconds Between Fetchs";

            public const int DEFAULT_SECONDS_BETWEEN_FETCHS = 60;

            public const string ORACLE_FETCH_SIZE_K = "Oracle Fetch Size (K)";

            public const int DEFAULT_ORACLE_FETCH_SIZE_K = 64;

            public const string WAIT_INTERVAL = "Event Handler Wait Time";

            public const int DEFAULT_WAIT_INTERVAL = 5;

            public const string MULTIPLIER_WAIT_INTERVAL = "Event Multiplier Wait Time";

            public const int DEFAULT_MULTIPLIER_WAIT_INTERVAL = 5;

            public const string MULTIPLIER_BATCH_SIZE = "Event Multiplier Batch Size";

            public const int DEFAULT_MULTIPLIER_BATCH_SIZE = 1000;

            public const string SECONDS_BETWEEN_EVENT_DELETIONS = "Seconds Between Events Deletions";

            public const int DEFAULT_SECONDS_BETWEEN_EVENT_DELETIONS = 60;

            public const string SECONDS_BETWEEN_FAILED_EVENTS_SUBS_DELETIONS = "Seconds Between Failed Events Subs Deletion";

            public const int DEFAULT_SECONDS_BETWEEN_FAILED_EVENTS_SUBS_DELETIONS = 3600;

            public EventHandler()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Server\\EventHandler", iWritable);
            }
        }

        public class FunctionEngine
        {
            public const string USE_TODAY_OPTIMIZATION = "Use Today Optimization";

            public const int DEFAULT_USE_TODAY_OPTIMIZATION = 1;

            public const string USE_TODAY_OPTIMIZATION_FOR_ALL_ACTIONS = "Use Today Optimization For All Actions";

            public const int DEFAULT_USE_TODAY_OPTIMIZATION_FOR_ALL_ACTIONS = 0;

            public const string FUNCTION_COMPILER_GENERATE_RANDOM_FILES = "Function Engine Generate Random Files";

            public const int DEFAULT_FUNCTION_COMPILER_GENERATE_RANDOM_FILES = 0;

            public const string SCRIPT_EXECUTER_LOAD_IN_SEPARATE_APP_DOMAIN = "Load Script Engine In Separate App Domain";

            public const int DEFAULT_SCRIPT_EXECUTER_LOAD_IN_SEPARATE_APP_DOMAIN = 0;

            public const string PROSIGHT_SERVICE_PRIORITY = "ProsightService Priority";

            public const int DEFAULT_PROSIGHT_SERVICE_PRIORITY = 3;

            public const string REGKEY_ROOT_PATH = "\\Server\\FunctionEngine";

            public FunctionEngine()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Server\\FunctionEngine", iWritable);
            }

            public class ActionDispatcher
            {
                public const string ACTION_BUFFER_SIZE = "Actions Buffer Size";

                public const int DEFAULT_ACTION_BUFFER_SIZE = 5000;

                public const string ACTION_ITERATOR_SIZE = "Actions Iterator Size";

                public const int DEFAULT_ACTION_ITERATOR_SIZE = 20001;

                public const string SYNC_PUSH_THRESHOLD = "Sync Push Threshold";

                public const int DEFAULT_SYNC_PUSH_THRESHOLD = 20000;

                public const string MAX_FN_QUEUE_INSERT_SIZE = "Max Fn Queue Insert Size";

                public const int DEFAULT_MAX_FN_QUEUE_INSERT_SIZE = 1000000;

                public const string FORCE_GARBAGE_COLLECTION_THRESHOLD = "Force Garbage Collection Threshold";

                public const int DEFAULT_FORCE_GARBAGE_COLLECTION_THRESHOLD = 10000000;

                public const string ACTION_RESTORE_INTERVAL = "Immediate Action Restore Interval";

                public const int DEFAULT_ACTION_RESTORE_INTERVAL = 60;

                public const string ACTION_DISPATCHER_SLEEP_INTERVAL = "Actions Disptcher Sleep Interval";

                public const int DEFAULT_ACTION_DISPATCHER_SLEEP_INTERVAL = 60;

                public const string ROOTS_THRESHOLD_FOR_PRIVATE_GRAPHS = "Roots Threshold For Private Graphs";

                public const int DEFAULT_ROOTS_THRESHOLD_FOR_PRIVATE_GRAPHS = 100;

                public const string IGNORE_PERSISTENCE = "Ignore Persistence";

                public const int DEFAULT_IGNORE_PERSISTENCE = 0;

                public const string RANKS_DICTIONARY_INTERRUPT_THRESHOLD = "Ranks Dictionary Interrupt Threshold";

                public const int DEFAULT_RANKS_DICTIONARY_INTERRUPT_THRESHOLD = 4;

                public const string REGKEY_ROOT_PATH = "\\Server\\FunctionEngine\\ActionDispatcher";

                public ActionDispatcher()
                {
                }

                public static RegistryKey getRoot(bool iWritable)
                {
                    return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Server\\FunctionEngine\\ActionDispatcher", iWritable);
                }
            }

            public class FunctionDispatcher
            {
                public const string NUM_FUNCTION_EXECUTERS = "Number of Function Executers";

                public const int DEFAULT_NUM_FUNCTION_EXECUTERS = 1;

                public const string FN_DISPATCHER_BUFFER_SIZE = "Function Dispatcher Buffer Size";

                public const int DEFAULT_FN_DISPATCHER_BUFFER_SIZE = 1000;

                public const string FN_DISPATCHER_MAX_READ_SIZE = "Function Dispatcher Max Read Size";

                public const int DEFAULT_FN_DISPATCHER_MAX_READ_SIZE = 100000;

                public const string ORACLE_FETCH_SIZE_K = "Oracle Fetch Size (K)";

                public const int DEFAULT_ORACLE_FETCH_SIZE_K = 1024;

                public const string NUM_OF_SLAVE_THREADS = "Number Of Slave Threads";

                public const int DEFAULT_NUM_OF_SLAVE_THREADS = 1;

                public const string SLAVES_IDLE_THRESHOLD_IN_SECS = "Slaves Idle Time Threshold";

                public const int DEFAULT_SLAVES_IDLE_THRESHOLD_IN_SECS = 600;

                public const string MASTER_WAIT_FOR_SLAVES_TIME = "Master Wait Time For Slaves";

                public const int DEFAULT_MASTER_WAIT_FOR_SLAVES_TIME = 5000;

                public const string SLAVE_SLEEP_TIME = "Slave Sleep Time";

                public const int DEFAULT_SLAVE_SLEEP_TIME = 5000;

                public const string SLAVE_PING_DB_THRESHOLD = "Slave Ping DB Threshold";

                public const int DEFAULT_SLAVE_PING_DB_THRESHOLD = 30;

                public const string TRANSFER_QUEUE_THRESHOLD = "Transfer Queue Threshold";

                public const int DEFAULT_TRANSFER_QUEUE_THRESHOLD = 60000;

                public const string WAIT_FOR_TEMP_TABLES_THRESHOLD = "Wait For Temp Tables Threshold";

                public const int DEFAULT_WAIT_FOR_TEMP_TABLES_THRESHOLD = 5000;

                public const string CLEAN_ALLOC_TABLE_THRESHOLD = "Clean Allocation Table Threshold";

                public const int DEFAULT_CLEAN_ALLOC_TABLE_THRESHOLD = 600;

                public const string REGKEY_ROOT_PATH = "\\Server\\FunctionEngine\\FunctionDispatcher";

                public FunctionDispatcher()
                {
                }

                public static RegistryKey getRoot(bool iWritable)
                {
                    return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Server\\FunctionEngine\\FunctionDispatcher", iWritable);
                }
            }
        }

        public class Install
        {
            public const string REGKEY_ROOT_PATH = "\\Install";

            public const string ENTRY_HANDLER = "handler";

            public const string ENTRY_COMAPNY_NAME = "Company Name";

            public const string ENTRY_USER_NAME = "User Name";

            public const string ENTRY_SERIAL_KEY = "Serial Key";

            public const string ENTRY_ORIGINAL_SERIAL_KEY = "Original Serial Key";

            public const string ENTRY_ROOT_DIR = "RootDir";

            public const string ENTRY_SPOOL_DIR = "SpoolDir";

            public const string ENTRY_TEMP_DIR = "TempDir";

            public const string ENTRY_VERSION = "Version";

            public const string ENTRY_PATH = "PortfoliosPath";

            public const string ENTRY_BASE_PATH = "Path";

            public const string DEFAULT_PATH = "C:\\Program Files\\Oracle\\Primavera Portfolio Management\\Portfolios";

            public const string ENTRY_SYSTEM_PATH = "System Path";

            public const string DEFAULT_SYSTEM_PATH = "C:\\WINNT\\system32\\ProSight";

            public const string ENTRY_SERVERNAME = "Server Name";

            public const string ENTRY_INTRANET_PORTFOLIOS_ACCESS = "Intranet Portfolios Access";

            public const string ENTRY_EXTRANET_PORTFOLIOS_ACCESS = "Extranet Portfolios Access";

            public const string ENTRY_DEFAULT_SERVER_LOCALE = "Default Server Locale";

            public Install()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Install", iWritable);
            }

            public class Components
            {
                public const string REGKEY_ROOT_PATH = "\\Install\\Components";

                public const string ENTRY_FRONTEND_INSTALLED = "FrontEndInstalled";

                public const string ENTRY_FUNCTIONENGINE_INSTALLED = "PrimaryFunctionEngineInstalled";

                public const string ENTRY_SECONDARYFUNCTIONENGINE_INSTALLED = "SecondaryFunctionEngineInstalled";

                public const string ENTRY_PROSIGHTSERVICE_INSTALLED = "ProSightServiceInstalled";

                public const string ENTRY_WATCHDOG_INSTALLED = "WatchdogInstalled";

                public const string ENTRY_WORKFLOW_INSTALLED = "WorkflowEngineInstalled";

                public const string ENTRY_LOCALIZATION_INSTALLED = "LocalizationInstalled";

                public const string ENTRY_UPKINTEGRATION_INSTALLED = "UPKIntegrationInstalled";

                public Components()
                {
                }

                public static RegistryKey getRoot(bool iWritable)
                {
                    return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Install\\Components", iWritable);
                }
            }
        }

        public class OpenAPI
        {
            public const string REGKEY_ROOT_PATH = "\\OpenAPI";

            public const string OPENAPI_CELLASOFFORMAT = "Cell AsOf Format";

            public OpenAPI()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\OpenAPI", iWritable);
            }
        }

        public class Server
        {
            public const string REGKEY_ROOT_PATH = "\\Server";

            public Server()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Server", iWritable);
            }
        }

        public class Service
        {
            private const string REGKEY_SERVICE_PREFIX = "SYSTEM\\CurrentControlSet\\Services\\ppmService";

            public const string ENTRY_START = "Start";

            public const int DEFAULT_START = 2;

            public Service()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("SYSTEM\\CurrentControlSet\\Services\\ppmService", iWritable);
            }
        }

        public class UI
        {
            public const string ENTRY_SSL_MODE = "SSLMode";

            public const int DEFAULT_SSL_MODE = 0;

            public const string ENTRY_SKIN_FILE_NAME = "Skin File Name";

            public const string DEFAULT_SKIN_FILE_NAME = "";

            public const string ENTRY_SSO_AUTH_TYPE_HTTP_HEADER_NAME = "SSO Authentication Type HTTP Header Name";

            public const string DEFAULT_SSO_AUTH_TYPE_HTTP_HEADER_NAME = "AUTH_TYPE";

            public const string ENTRY_SSO_AUTH_TYPE_HTTP_HEADER_VALUE = "SSO Authentication Type HTTP Header Value";

            public const string DEFAULT_SSO_AUTH_TYPE_HTTP_HEADER_VALUE = "Negotiate";

            public const string ENTRY_SSO_AUTH_USER_HTTP_HEADER_NAME = "SSO Authenticated User HTTP Header Name";

            public const string DEFAULT_SSO_AUTH_USER_HTTP_HEADER_NAME = "LOGON_USER";

            public const string ENTRY_PROSIGHT_WEBSITE = "WebSite";

            public const string DEFAULT_PROSIGHT_WEBSITE = "1";

            public const string ENTRY_SMTP_MODE = "SMTP Mode";

            public const int DEFAULT_SMTP_MODE = 1;

            public const string ENTRY_SMTP_SERVER_NAME = "SMTP Server Name";

            public const string DEFAULT_SMTP_SERVER_NAME = "localhost";

            public const string ENTRY_SMTP_SERVER_PORT = "SMTP Server Port";

            public const int DEFAULT_SMTP_SERVER_PORT = 25;

            public const string ENTRY_SMTP_AUTHENTICATION = "SMTP Authentication";

            public const int DEFAULT_SMTP_AUTHENTICATION = 0;

            public const string ENTRY_SMTP_USERNAME = "SMTP Username";

            public const string DEFAULT_SMTP_USERNAME = "";

            public const string ENTRY_SMTP_PASSWORD = "SMTP Password";

            public const string DEFAULT_SMTP_PASSWORD = "";

            public const string ENTRY_SMTP_SERVER_PICKUP_DIRECTORY = "SMTP Server Pickup Directory";

            public const string DEFAULT_SMTP_SERVER_PICKUP_DIRECTORY = "\\INetPub\\mailroot\\pickup";

            public const string ENTRY_SMTP_CONNECTION_TIMEOUT = "SMTP Connection Timeout";

            public const int DEFAULT_SMTP_CONNECTION_TIMEOUT = 60;

            public const string ENTRY_SMTP_USE_SSL = "SMTP Use SSL";

            public const int DEFAULT_SMTP_USE_SSL = 0;

            public const string MAIL_FORMAT = "Mail Format";

            public const string DEFAULT_MAIL_FORMAT = "UTF-8";

            public const string ENTRY_DUNDAS_ENTERPRISE_LICENSE = "Dundas Enterprise License";

            public const int DEFAULT_ENTRY_DUNDAS_ENTERPRISE_LICENSE = 1;

            public const string ENTRY_HASH_VERSION = "Hash Version";

            public const int DEFAULT_HASH_VERSION = 4;

            public const string SCATTER_BUBBLES_IN_VALUELIST_BUCKETS = "Scatter Bubbles In ValueList Buckets";

            public const int DEFAULT_SCATTER_BUBBLES_IN_VALUELIST_BUCKETS = 1;

            public const string ENTRY_UPK_URL = "UPK URL";

            public const string DEFAULT_UPK_URL = "";

            public const string ENTRY_UPK_CONTEXT = "UPK Context";

            public const string DEFAULT_UPK_CONTEXT = "module";

            public const string ENTRY_UPK_ADDITIONALPARAMS = "UPK Additional Parameters";

            public const string DEFAULT_UPK_ADDITIONALPARAMS = "";

            public const string ENTRY_LDAP_AUTHENTICATION_TYPE = "LDAP Authentication Type";

            public const int DEFAULT_LDAP_AUTHENTICATION_TYPE = 0;

            public const string ENTRY_LDAP_USE_LDAPS = "LDAP Use LDAPS";

            public const int DEFAULT_LDAP_USE_LDAPS = 0;

            public const string SHOW_STACK_TRACE_IN_ERROR_DIALOGUE = "Show Stack Trace In Error Dialogue";

            public const int DEFAULT_SHOW_STACK_TRACE_IN_ERROR_DIALOGUE = 0;

            public const string ALLOWED_FILE_EXTENSIONS = "Allowed File Extensions";

            public const string DEFAULT_ALLOWED_FILE_EXTENSIONS = ".txt;.doc;.docx;.xls;.xlsx;.pdf;.odt;.ods;.xps;.ppt;.pptx;.dot;.ott;.rtf;.pps;.jpeg;.gif;.tif;.png";

            public const string SEESION_COOKIE_NAME = "Session Cookie Name";

            public const string DEFAULT_SEESION_COOKIE_NAME = "ASP.NET_SessionId";

            public const string REGKEY_ROOT_PATH = "\\Server\\UI";

            public UI()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Server\\UI", iWritable);
            }
        }

        public class Watchdog
        {
            public const string INTERVAL = "Interval";

            public const string START_TIME = "Start Time";

            public const string NUMBER_OF_RETIRES = "Number Of Retries";

            public const string TIMEOUT = "Default Timout in Miliseconds";

            public const string WAIT_BETWEEN_STOP_AND_START = "Wait Between Stop And Start in Miliseconds";

            public const string REGKEY_ROOT_PATH = "\\Server\\Watchdog";

            public const string RESTART_TIME = "Restart time";

            public const string DEFAULT_RESTART_TIME = "11:55:00 PM";

            public const string RESTART_SLEEP_TIME = "Restart sleep time (mili-seconds)";

            public const int DEFAULT_RESTART_SLEEP_TIME = 120000;

            public const string CHECKPSSERVICE_INTERVAL = "Check psService Interval (minutes)";

            public const int DEFAULT_CHECKPSSERVICE_INTERVAL = 5;

            public const string SERVICE_RESTART_PERIOD = "Service restart period (minutes)";

            public const int DEFAULT_SERVICE_RESTART_INTERVAL = 240;

            public const string SERVICE_RESTART_SLEEP_TIME = "Service restart period sleep time (mili-seconds)";

            public const int DEFAULT_SERVICE_RESTART_SLEEP_TIME = 10000;

            public const string SERVICE_RESTART_FOR_ORACLE = "Service restart for oracle";

            public const int DEFAULT_SERVICE_RESTART_FOR_ORACLE = 1;

            public const string SERVICE_RESTART_FOR_SQL = "Service restart for SQL Server";

            public const int DEFAULT_SERVICE_RESTART_FOR_SQL = 0;

            public const string CHECKIISAPP_INTERVAL = "Check IIS App Interval (minutes)";

            public const int DEFAULT_CHECKIISAPP_INTERVAL = 1;

            public const string CHECKIISAPP_RETRIES = "Retry Check If IIS App Alive (times)";

            public const int DEFAULT_CHECKIISAPP_RETRIES = 4;

            public const string IISAPP_RESTART_SLEEP_TIME = "IIS App period sleep time (milli-seconds)";

            public const int DEFAULT_IISAPP_RESTART_SLEEP_TIME = 10000;

            public const string IISAPP_DETECTIONTHREAD_TIMEOUT = "IIS App detection thread timeout (milli-seconds)";

            public const int DEFAULT_IISAPP_DETECTIONTHREAD_TIMEOUT = 60000;

            public const string IISAPP_KILLERTHREAD_TIMEOUT = "IIS App killer thread timeout (milli-seconds)";

            public const int DEFAULT_IISAPP_KILLERTHREAD_TIMEOUT = 60000;

            public const string CAN_RESTART_SERVICE_INTERVAL = "Interval";

            public const int DEFAULT_CAN_RESTART_FUNCTION_SERVICE_INTERVAL = 5;

            public const int DEFAULT_CAN_RESTART_PROSIGHT_SERVICE_INTERVAL = 5;

            public const string FORCE_SERVICE_RESTART_AFTER_X_HOURS = "Force Service Restart After X Hours";

            public const int DEFAULT_FORCE_FUNCTION_SERVICE_RESTART_AFTER_X_HOURS = 336;

            public const int DEFAULT_FORCE_PROSIGHT_SERVICE_RESTART_AFTER_X_HOURS = 336;

            public const string CONSIDER_SERVICE_RESTART_AFTER_X_HOURS = "Consider Service Restart After X Hours";

            public const int DEFAULT_CONSIDER_FUNCTION_SERVICE_RESTART_AFTER_X_HOURS = 168;

            public const int DEFAULT_CONSIDER_PROSIGHT_SERVICE_RESTART_AFTER_X_HOURS = 168;

            public Watchdog()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Server\\Watchdog", iWritable);
            }
        }

        public class WatchdogService
        {
            private const string REGKEY_WATCHDOG_PREFIX = "SYSTEM\\CurrentControlSet\\Services\\ppmWatchdog";

            public const string ENTRY_START = "Start";

            public const int DEFAULT_START = 2;

            public WatchdogService()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("SYSTEM\\CurrentControlSet\\Services\\ppmWatchdog", iWritable);
            }
        }

        public class WorkflowEngine
        {
            public const string REGKEY_ROOT_PATH = "\\Server\\WorkflowEngine";

            public const string CONDITION_STEP_SLEEP_SECONDS = "Condition Step Sleep Seconds";

            public const int DEFAULT_CONDITION_STEP_SLEEP_SECONDS = 5;

            public const string MAX_WORKFLOW_INSTANCE_DEPTH_ALLOWED = "Max Workflow Instance Depth Allowed";

            public const int DEFAULT_MAX_WORKFLOW_INSTANCE_DEPTH_ALLOWED = 10;

            public const string WAIT_INTERVAL_IN_SECONDS = "Wait Interval";

            public const int DEFAULT_WAIT_INTERVAL_IN_SECONDS = 60;

            public const string THREADS_NUM = "Threads Number";

            public const int DEFAULT_THREADS_NUM = 5;

            public const string UPDATE_CELL_BATCH_SIZE = "Update Cell Batch Size";

            public const int DEFAULT_UPDATE_CELL_BATCH_SIZE = 50;

            public WorkflowEngine()
            {
            }

            public static RegistryKey getRoot(bool iWritable)
            {
                return psRegistry.getKey("Software\\Oracle\\Primavera Portfolio Management\\Portfolios\\Server\\WorkflowEngine", iWritable);
            }
        }
    }
}