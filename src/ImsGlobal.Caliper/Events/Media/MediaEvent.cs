﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImsGlobal.Caliper.Events.Media {

	/// <summary>
	/// Event raised when an actor interacts with a media resource.
	/// </summary>
	public class MediaEvent : Event {

		public MediaEvent( Action action ) {
			this.Context = EventContext.Media.Uri;
			this.Type = EventType.Media.Uri;
			this.Action = action;
		}

	}

}
