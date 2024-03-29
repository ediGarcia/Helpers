﻿using System;
using System.IO;
using System.Net;
using HelperMethods.Enums;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class BytesMethods
{
    #region Public Methods

    #region Convert
    /// <summary>
    /// Converts bytes units.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="currentUnit"></param>
    /// <param name="targetUnit"></param>
    /// <param name="useBase10">Indicates if the calculations should use the decimal (1000)(true) or binary (1024)(false) value for one kilobyte.</param>
    /// <returns></returns>
    public static double Convert(double value, FileSizeUnit currentUnit, FileSizeUnit targetUnit, bool useBase10 = false)
    {
        double baseNumber = useBase10 ? 1000d : 1024d;

        if (currentUnit > targetUnit)
            while (currentUnit > targetUnit)
            {
                value *= baseNumber;
                currentUnit--;
            }
        else
            while (currentUnit < targetUnit)
            {
                value /= baseNumber;
                currentUnit++;
            }

        return value;
    }
    #endregion

    #region GenerateString*

    #region GenerateString(double, FileSizeUnit, [int])
    /// <summary>
    /// Generates string for a file size value.
    /// </summary>
    /// <param name="valor"></param>
    /// <param name="unit"></param>
    /// <param name="decimalPlaces"></param>
    /// <returns></returns>
    public static string GenerateString(double valor, FileSizeUnit unit, int decimalPlaces = 0)
    {
        string unitSign = unit switch
        {
            FileSizeUnit.Kilo => "KB",
            FileSizeUnit.Mega => "MB",
            FileSizeUnit.Giga => "GB",
            FileSizeUnit.Tera => "TB",
            FileSizeUnit.Peta => "PB",
            FileSizeUnit.Exa => "EB",
            FileSizeUnit.Zetta => "ZB",
            FileSizeUnit.Yotta => "YB",
            FileSizeUnit.Ronna => "RB",
            FileSizeUnit.Quetta => "QB",
            _ => "B"
        };

        return $"{valor.ToString("0.".PadRight(decimalPlaces + 2, '#'))} {unitSign}";
    }
    #endregion

    #region GenerateString(double, [int], [bool], [FileSizeUnit])
    /// <summary>
    /// Returns a string representation of the bytes in the best unit.
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="decimalPlaces"></param>
    /// <param name = "useBase10">Indicates if the calculations should use the decimal (1000)[true] or binary(1024)[false] value for one kilobyte.</param>
    /// <param name="maxUnit">Determines the highest possible size unit.</param>
    /// <returns></returns>
    public static string GenerateString(double bytes, int decimalPlaces = 0, bool useBase10 = false, FileSizeUnit maxUnit = FileSizeUnit.Quetta)
    {
        FileSizeUnit unit = FileSizeUnit.Byte;
        double baseValueInt = useBase10 ? 1000d : 1024d;

        while (bytes >= baseValueInt && unit < maxUnit)
        {
            bytes /= baseValueInt;
            unit++;
        }

        return GenerateString(bytes, unit, decimalPlaces);
    }
    #endregion

    #endregion

    #region GetFileSize*

    #region GetFileSize(string)
    /// <summary>
    /// Retrieves a file size in bytes.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="System.Security.SecurityException"/>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="UnauthorizedAccessException"/>
    /// <exception cref="PathTooLongException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="UriFormatException"/>
    /// <exception cref="InvalidOperationException"/>
    /// <exception cref="WebException"/>
    public static long GetFileSize(string url) =>
        GetFileSize(url, null, null);
    #endregion

    #region GetFileSize(string, string, string)
    /// <summary>
    /// Retrieves a file size in bytes.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="System.Security.SecurityException"/>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="UnauthorizedAccessException"/>
    /// <exception cref="PathTooLongException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="UriFormatException"/>
    /// <exception cref="InvalidOperationException"/>
    /// <exception cref="WebException"/>
    public static long GetFileSize(string url, string user, string password)
    {
        bool hasCredentials = !String.IsNullOrEmpty(user) || !String.IsNullOrEmpty(password);

        //Validates local file.-------------
        Uri uri = new(url, UriKind.RelativeOrAbsolute);

        if (!uri.IsAbsoluteUri || uri.IsFile)
            return SystemMethods.GetSize(url);
        //----------------------------------

        //FTP files.
        if (url.StartsWith("ftp://") || url.StartsWith("ftp."))
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);
                ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;

                if (hasCredentials)
                    ftpRequest.Credentials = new NetworkCredential(user, password);

                return ftpRequest.GetResponse().ContentLength;
            }
            catch
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                if (hasCredentials)
                    ftpRequest.Credentials = new NetworkCredential(user, password);

                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                string[] campos = new StreamReader(response.GetResponseStream()).ReadToEnd()
                    .Split([' '], StringSplitOptions.RemoveEmptyEntries);

                return campos.Length >= 5 && Int64.TryParse(campos[4], out long result) ? result : -1;
            }

        //Http files.-------------
        WebRequest request = WebRequest.Create(url);
        request.Method = "HEAD";

        if (hasCredentials)
            request.Credentials = new NetworkCredential(user, password);

        using (WebResponse response = request.GetResponse())
            return response.ContentLength;
        //------------------------
    }
    #endregion

    #endregion

    #endregion
}