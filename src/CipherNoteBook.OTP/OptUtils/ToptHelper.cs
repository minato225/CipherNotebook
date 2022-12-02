using CipherNoteBook.Domain.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace CipherNoteBook.OTP.OptUtils;

public static class ToptHelper
{
    /// <summary>
    ///     Function that implements the Time-Based One-Time Password (TOTP) algorithm
    ///     Explanation: http://garbagecollected.org/2014/09/14/how-google-authenticator-works/
    ///     RFC: https://tools.ietf.org/rfc/rfc6238.txt
    /// </summary>
    /// <param name="secret">Shared secret</param>
    /// <param name="algorithm">HMAC algorithm</param>
    /// <param name="digits">Determines how long of a one-time passcode to display to the user. The default is 6</param>
    /// <param name="period">Defines a period that a TOTP code will be valid for, in seconds. The default value is 30</param>
    /// <returns></returns>
    public static uint TimeBasedOneTimePassword(string secret)
    {
        var period = 30;
        var digits = 6;

        if (string.IsNullOrWhiteSpace(secret))
            throw new ArgumentException("secret cannot be null or empty", nameof(secret));

        long unixTime = UnixTime() / period;

        byte[] secretBytes = Base32Encoding.ToBytes(secret.ToUpper());

        var hmac = new HMACSHA1(secretBytes);

        byte[] result = hmac.ComputeHash(BitConverter.IsLittleEndian
                ? BitConverter.GetBytes(unixTime).Reverse().ToArray()
                : BitConverter.GetBytes(unixTime).ToArray());

        // get a number between [0-F]
        int offset = result.Last() & 0x0F;

        // Generate a 4-byte array starting from the above offset
        byte[] fourBytes = new byte[4];
        Array.Copy(result, offset, fourBytes, 0, 4);

        fourBytes[0] = (byte)(fourBytes[0] & 0x7F);
        uint largeInteger =
            BitConverter.ToUInt32(BitConverter.IsLittleEndian ? fourBytes.Reverse().ToArray() : fourBytes.ToArray(), 0);

        uint smallInteger = largeInteger % ((uint)Math.Pow(10, digits));
        return smallInteger;
    }

    /// <summary>
    ///     Returns an int representing the current unix time.
    /// </summary>
    /// <returns>current unix time</returns>
    public static int UnixTime() =>
        (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
}
