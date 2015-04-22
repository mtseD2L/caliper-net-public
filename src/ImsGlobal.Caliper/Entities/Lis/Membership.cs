﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Lis {
	using ImsGlobal.Caliper.Entities.W3c;

	/// <summary>
	/// A Caliper Membership is used to define the relationship between
	/// objects that can have members and objects that can be members.
	/// Objects recognized as having members are CourseOffering,
	/// CourseSection and Group, all of which implement the IOrganization
	/// marker interface. Any Agent entity can be a member.
	/// </summary>
	public class Membership : Entity, IMembership {

		public Membership( string id )
			: base( id ) {
			this.Type = EntityType.Membership.Uri;
			this.Roles = new List<IRole>();
		}

		[JsonProperty( "member", Order = 21 )]
		public string MemberId { get; set; }

		[JsonProperty( "organization", Order = 22 )]
		public string OrganizationId { get; set; }

		[JsonProperty( "roles", Order = 23 )]
		public IList<IRole> Roles { get; set; }

		[JsonProperty( "status", Order = 24 )]
		public IStatus Status { get; set; }

	}

}
