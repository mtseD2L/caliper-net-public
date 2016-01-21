using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImsGlobal.Caliper {

	/// <summary>
	/// Options for registering a Caliper endpoint with the Caliper sensor.
	/// </summary>
	public class CaliperEndpointOptions {

		private const int DEFAULT_TIMEOUT = 10000;

		public CaliperEndpointOptions( Uri host, int timeout = DEFAULT_TIMEOUT ) {
			if( host == null ) {
				throw new ArgumentNullException( "host" );
			}
			if( timeout < 1000 ) {
				throw new ArgumentOutOfRangeException( "timeout", "timeout must be at least 1000 milliseconds." );
			}

			this.Host = host;
			this.Timeout = timeout;
		}

		public CaliperEndpointOptions( Uri host, string apiKey, int timeout = DEFAULT_TIMEOUT ) : this(host, timeout) {
			this.ApiKey = apiKey;
		}

		public CaliperEndpointOptions( Uri host, string authScheme, string authToken, int timeout = DEFAULT_TIMEOUT ) : this( host, timeout ) {
			if( String.IsNullOrWhiteSpace( authScheme ) || String.IsNullOrWhiteSpace( authToken ) ) {
				throw new ArgumentException( "authScheme and authToken can not be null, empty, or whitespace." );
			}

			this.AuthScheme = authScheme;
			this.AuthToken = authToken;
		}

		/// <summary>
		/// The Caliper web API endpoint.
		/// </summary>
		public Uri Host { get; set; }

		/// <summary>
		/// The Caliper web API key.
		/// </summary>
		public string ApiKey { get; set; }

		/// <summary>
		/// The Caliper web API key.
		/// </summary>
		public string AuthScheme { get; private set; }

		/// <summary>
		/// The Caliper web API key.
		/// </summary>
		public string AuthToken { get; private set; }

		/// <summary>
		/// Connection timeout in milliseconds.
		/// </summary>
		public int Timeout { get; set; }

	}

}
