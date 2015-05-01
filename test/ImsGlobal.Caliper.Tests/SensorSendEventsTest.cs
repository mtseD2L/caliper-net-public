﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace ImsGlobal.Caliper.Tests {
	using ImsGlobal.Caliper.Tests.SimpleHelpers;
	using ImsGlobal.Caliper.Events;
	using ImsGlobal.Caliper.Events.Reading;
	using ImsGlobal.Caliper.Protocol;

	public class SensorSendEventsTest {

		[Fact]
		public void CaliperMessage_MatchesReferenceJson() {

			var navigationEvent = new NavigationEvent {
				EdApp = TestEntities.Readium,
				Group = TestEntities.AmRev101_Group001,
				Actor = TestEntities.Student554433,
				Object = TestEntities.EpubVolume43,
				Target = TestEntities.EpubSubChap431_Frame,
				FromResource = TestEntities.AmRev101LandingPage,
				StartedAt = TestEntities.DefaultStartedAtTime
			};

			var caliperMessage = new CaliperMessage<Event> {
				SensorId = "http://learning-app.some-university.edu/sensor",
				SendTime = NodaTime.Instant.FromUtc( 2015, 09, 15, 11, 05, 01 ),
				Data = new [] {navigationEvent}
			};

			JsonAssertions.AssertSameObjectJson( caliperMessage, "eventStorePayload" );
		}

	}

}
