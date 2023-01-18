using System;
using System.IO;

namespace HelpersClasses.Commands.Jira
{
    [Serializable]
    public class Attachment : IComparable<Attachment>
    {
        #region Properties

        /// <summary>
        /// User name that uploaded the file.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Date in which the file was uploaded.
        /// </summary>
        public DateTime? CreatedDate { get; }

        /// <summary>
        /// File name.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Download url.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// File size.
        /// </summary>
        public long? Size { get; }

        #endregion

        public Attachment(string baseJiraUrl, Atlassian.Jira.Attachment jiraAttachment)
        {
            Author = jiraAttachment.Author;
            CreatedDate = jiraAttachment.CreatedDate;
            FileName = jiraAttachment.FileName;
            Url = Path.Combine(baseJiraUrl, "secure/attachment/", jiraAttachment.Id, jiraAttachment.FileName);
            Size = jiraAttachment.FileSize;
        }

        #region Public Methods

        public int CompareTo(Attachment other)
        {
            int comparisonResult = String.Compare(FileName, other.FileName, StringComparison.OrdinalIgnoreCase);

            if (comparisonResult == 0 && CreatedDate.HasValue)
                comparisonResult = CreatedDate.Value.CompareTo(other.CreatedDate.GetValueOrDefault(DateTime.MaxValue));

            return comparisonResult;
        }

        #endregion
    }
}
