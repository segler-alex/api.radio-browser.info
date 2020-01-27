private static string GetRadioBrowserApiUrl()
{
  // Get fastest ip of dns
  string baseUrl = @"all.api.radio-browser.info";
  var ips = Dns.GetHostAddresses(baseUrl);
  long lastRoundTripTime = long.MaxValue;
  string searchUrl = @"de1.api.radio-browser.info"; // Fallback
  foreach (IPAddress ipAddress in ips)
  {
    var reply = new Ping().Send(ipAddress);
    if (reply != null &&
        reply.RoundtripTime < lastRoundTripTime)
    {
      lastRoundTripTime = reply.RoundtripTime;
      searchUrl = ipAddress.ToString();
    }
  }

  // Get clean name
  IPHostEntry hostEntry = Dns.GetHostEntry(searchUrl);
  if (!string.IsNullOrEmpty(hostEntry.HostName))
  {
    searchUrl = hostEntry.HostName;
  }

  return searchUrl;
}

