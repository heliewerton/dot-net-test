using System;

namespace Domain {
    // The configuration class.
    // This class implements the pattern singleton, but not thread-safe.
    public class Config
    {
        // Holds an instance to singleton pattern.
        private static Config instance=null;

        // Holds the target url to all request on the api.
        public string TargetURL { get; }

        // Holds credentials to get the token out the code to better security.
        public string TokenCredentials { get; }

        // A private constructor to singleton.
        private Config() {
            // Gets the env variable to target url.
            TargetURL = Environment.GetEnvironmentVariable("TARGET_URL");

            // Checks if env var was defined.
            if (TargetURL == null) {
                // Throws an exception to force var defination.
                throw new Exception("Configure a var TARGET_URL=\"<URL>\"");
            }

            // Gets the env variable to target url.
            TokenCredentials = Environment.GetEnvironmentVariable("TOKEN_CREDENTIALS");

            // Checks if env var was defined.
            if (TokenCredentials == null) {
                // Throws an exception to force var defination.
                throw new Exception("Configure a var TOKEN_CREDENTIALS=\"<VALUE>\"");
            }
        }

        // Singleton pattern logic. See: https://msdn.microsoft.com/en-us/library/ff650316.aspx.
        public static Config Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new Config();
                }
                return instance;
            }
        }
    }
}