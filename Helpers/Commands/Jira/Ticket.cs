using Atlassian.Jira;

namespace HelpersClasses.Commands.Jira
{
	public class Ticket
	{
		#region Properties

		public Attachment[] Attachments { get; }

		/// <summary>
		/// Ticket's country.
		/// </summary>
		public string Country { get; }

		/// <summary>
		/// Other fields.
		/// </summary>
		public CustomFieldValueCollection CustomFields { get; set; }

		/// <summary>
		/// Ticket's number.
		/// </summary>
		public string Number { get; }

		/// <summary>
		/// Ticket's market.
		/// </summary>
		public string Market { get; }

		/// <summary>
		/// Related ticket's numbers.
		/// </summary>
		public string[] RelatedIncidents { get; }

		/// <summary>
		/// Ticket's summary.
		/// </summary>
		public string Summary { get; }

		/// <summary>
		/// Ticket's url.
		/// </summary>
		public string Url { get; }

		#endregion

		public Ticket(string number, string summary, string url, string market, string country, CustomFieldValueCollection customFields, string[] relatedIncidents, Attachment[] attachments)
		{
			Number = number;
			Summary = summary;
			Url = url;
			Market = market;
			Country = country;
			CustomFields = customFields;
			RelatedIncidents = relatedIncidents;
			Attachments = attachments;
		}

		#region Public Methods

		#region ToString
		public override string ToString() =>
			$"{Number} - {Summary};";
		#endregion

		#endregion
	}
}
