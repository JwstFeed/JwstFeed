using Newtonsoft.Json;
using System.ComponentModel;

namespace JwstFeed.Models;

public class ResultFilter
{
    [DisplayName("Nasa Blogs")]
    public bool IsToShowNasaBlogs { get; set; } = true;

    [DisplayName("WebbTelescope.org Articles")]
    public bool IsToShowWebbTelescopeArticles { get; set; } = true;

    [DisplayName("Arxiv")]
    public bool IsToShowArxiv { get; set; } = true;

    [DisplayName("Stsci Raw Images (MIRI)")]
    public bool IsToShowStsciRawMiri { get; set; } = false;

    [DisplayName("Observation Schedules")]
    public bool IsToShowStsciSchedule { get; set; } = true;

    [DisplayName("Esa Webb")]
    public bool IsToShowEsaWebb { get; set; } = true;

    [DisplayName("JWST Flickr")]
    public bool IsToShowFlickr { get; set; } = true;

    [DisplayName("Reddit")]
    public bool IsToShowReddit { get; set; } = true;

    [DisplayName("YouTube")]
    public bool IsToShowYoutube { get; set; } = true;

    [DisplayName("Twitter")]
    public bool IsToShowTwitter { get; set; } = true;

    [DisplayName("Twitter Raw Image Bot")]
    public bool IsToShowTwitterRawBot { get; set; } = false;

    [DisplayName("Stsci Raw Images & Data (NIRISS)")]
    public bool IsToShowStsciRawNiriss { get; set; } = false;

    [DisplayName("WebbTelescope.org")]
    public bool IsToShowWebbOrgImages { get; set; } = true;

    [DisplayName("Stsci Raw Images (NIRCam)")]
    public bool IsToShowStsciRawNircam { get; set; } = false;

    [DisplayName("Stsci Raw Data (NIRSpec)")]
    public bool IsToShowStsciRawNirspec { get; set; } = false;

    [DisplayName("CEERS - Cosmic Evolution Early Release Science Survey")]
    public bool IsToShowCeers { get; set; } = false;

    public override string ToString()
        =>
        JsonConvert.SerializeObject(this);
}