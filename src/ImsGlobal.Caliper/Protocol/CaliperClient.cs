using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;

namespace ImsGlobal.Caliper.Protocol {
	using ImsGlobal.Caliper.Entities;
	using ImsGlobal.Caliper.Events;

	internal class CaliperClient {

		private readonly CaliperEndpointOptions _options;
		private readonly string _sensorId;
		private readonly JsonSerializerSettings _serializerSettings;
		private readonly bool _hasAuth;

		public CaliperClient( CaliperEndpointOptions options, string sensorId ) {
			_options = options;
			_sensorId = sensorId;
			_serializerSettings = new JsonSerializerSettings();
			_serializerSettings.ConfigureForNodaTime( DateTimeZoneProviders.Tzdb );

			_hasAuth = !String.IsNullOrWhiteSpace( options.AuthScheme );
		}

		public async Task<bool> Send( IEnumerable<Event> events ) {
			return await SendData( events );
		}

		public async Task<bool> Describe( IEnumerable<Entity> entities ) {
			return await SendData( entities );
		}

		public async Task<bool> SendData<T>( IEnumerable<T> data ) {

			var message = new CaliperMessage<T> {
				SensorId = _sensorId,
				SendTime = SystemClock.Instance.Now,
				Data = data
			};
			string json = JsonConvert.SerializeObject( message, _serializerSettings );
			var content = new StringContent( json, Encoding.UTF8, "application/json" );

			using( var client = new HttpClient() ) {

				if( _hasAuth ) {
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( _options.AuthScheme, _options.AuthToken );
				}

				client.BaseAddress = _options.Host;
				try {

					HttpResponseMessage response = await client.PostAsync( "", content );
					response.EnsureSuccessStatusCode();

				} catch( HttpRequestException ex ) {
					var msg = String.Format( "Failed to send data: {0}", ex.Message );
					Trace.WriteLine( msg );
					return false;
				}
			}

			return true;
		}

	}

}
