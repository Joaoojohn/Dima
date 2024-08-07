﻿namespace Dima.Core
{
    public static class Configuration
    {
        #region Paged

        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 25;

        #endregion

        #region StatusCode

        public const int DefaultStatusCode = 200;

        #endregion

        #region connectionString
        public static string ConnectionString { get; set; } = string.Empty;

        #endregion

        public static string BackendUrl { get; set; } = string.Empty;
        public static string FrontendUrl { get; set; } = string.Empty;

        public static long PremiumPrice { get; set; } = 19999;
    }
}
