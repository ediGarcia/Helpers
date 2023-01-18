using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlassian.Jira;
using HelperExtensions;
using HelpersClasses.Classes;

namespace HelpersClasses.Commands.Jira
{
    public class JiraCommands
    {
        #region Properties

        /// <summary>
        /// User login.
        /// </summary>
        public string Login { get; private set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Jira url.
        /// </summary>
        public string Url { get; private set; }

        #endregion

        private Atlassian.Jira.Jira _client; //Jira rest client.

        public JiraCommands(string url, string login, string password) =>
            SetCredentials(url, login, password);

        #region Public Methods

        #region GetTicket
        /// <summary>
        /// Retrieves ticket info.
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <returns></returns>
        public Ticket GetTicket(string ticketNumber)
        {
            Issue issue = GetIssue(ticketNumber);
            string market = "ALL";
            string country = "All";
            string[] relatedIncidents = GetRelatedIncidents(issue);

            if (issue.CustomFields["Market"] is CustomFieldValue mercado)
            {
                market = mercado.Values[0];

                if (mercado.Values.Length > 1)
                    country = ValidateCountry(mercado.Values[1]);
            }

            //Lists attachment from all related incidents.
            List<Attachment> attachments = (
                    from relatedIssue
                        in _client.Issues.GetIssuesAsync(relatedIncidents).Result.Values
                    from jiraAttachment in relatedIssue.GetAttachmentsAsync().Result
                    select new Attachment(Url, jiraAttachment))
                .ToList();
            attachments.Sort();

            return new Ticket(issue.Key.Value, issue.Summary, GetTicketUrl(ticketNumber), market, country, issue.CustomFields, relatedIncidents, attachments.ToArray());
        }
        #endregion

        #region GetTicketUrl
        /// <summary>
        /// Returns a ticket's url.
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <returns></returns>
        public string GetTicketUrl(string ticketNumber) =>
            $"{Url}/browse/{ticketNumber}";
        #endregion

        #region InsertComment
        /// <summary>
        /// Inserts comment into the selected ticket.
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <param name="text"></param>
        /// <param name="broadcast">If true, broadcasts the comment to each related incident.</param>
        // ReSharper disable once UnusedMember.Global
        public void InsertComment(string ticketNumber, string text, bool broadcast = false) =>
            InsertCommentBackground(ticketNumber, text, broadcast).ForEach(task => task.Wait());
        #endregion

        #region InsertCommentAsync
        /// <summary>
        /// Inserts comments into the selected ticket.
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task InsertCommentAsync(string ticketNumber, string text) =>
            await GetIssue(ticketNumber).AddCommentAsync(text);
        #endregion

        #region InsertCommentBackground
        /// <summary>
        /// Inserts comment into the selected ticket.
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <param name="text"></param>
        /// <param name="broadcast">If true, broadcasts the comment to each related incident.</param>
        /// <returns></returns>
        public Task[] InsertCommentBackground(string ticketNumber, string text, bool broadcast = false)
        {
            List<Task> tasks = new List<Task>();
            Issue issue = GetIssue(ticketNumber);

            if (broadcast)
                tasks.AddRange(GetRelatedIncidents(issue).Select(relatedTicket => InsertCommentAsync(relatedTicket, text)));
            else
                tasks.Add(InsertCommentAsync(ticketNumber, text));

            return tasks.ToArray();
        }
        #endregion

        #region SetCredentials
        /// <summary>
        /// Sets new credentials for Jira access.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public void SetCredentials(string url, string login, string password) =>
            _client = Atlassian.Jira.Jira.CreateRestClient(Url = url, Login = login, Password = password);
        #endregion

        #endregion

        #region Private Methods

        #region GetIssue
        /// <summary>
        /// Retrieves issue data.
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <returns></returns>
        private Issue GetIssue(string ticketNumber) =>
            _client.Issues.GetIssueAsync(ticketNumber).Result;
        #endregion

        #region GetRelatedIncidents

        /// <summary>
        /// Gets the selected issue's related incidents list.
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        private string[] GetRelatedIncidents(Issue issue) =>
            issue.CustomFields["Related Incidents:"]?.Values;
        #endregion;

        #region ValidateCountry
        /// <summary>
        /// Confirms country information.
        /// </summary>
        /// <param name="countryInfo"></param>
        /// <returns></returns>
        public static string ValidateCountry(string countryInfo)
        {
            //Null value.
            if (String.IsNullOrWhiteSpace(countryInfo))
                return "All";

            //Checks if country exists.
            if (Arrays.Countries.Any(country => country.ToLower() == countryInfo.ToLower()))
                return countryInfo;

            //Checks if country info contains a country name.
            if (Arrays.Countries.FirstOrDefault(country => countryInfo.ToLower().Contains(country.ToLower())) is string foundCountry1)
                return foundCountry1;

            //Checks if any country name contains the country info.
            if (Arrays.Countries.FirstOrDefault(country => country.ToLower().Contains(countryInfo.ToLower())) is string foundCountry2)
                return foundCountry2;

            return "All"; //No country found.
        }
        #endregion

        #endregion
    }
}
