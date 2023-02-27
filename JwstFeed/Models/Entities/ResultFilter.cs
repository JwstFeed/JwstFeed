using Newtonsoft.Json;
using System.ComponentModel;

namespace JwstFeed.Models.Entities;

public class ResultFilter
{
    [DisplayName("Nasa Blogs")]
    public bool IsToShowNasaBlogs { get; set; } = true;

    [DisplayName("WebbTelescope.org Articles")]
    public bool IsToShowWebbTelescopeArticles { get; set; } = true;

    [DisplayName("Arxiv")]
    public bool IsToShowArxiv { get; set; } = true;

    [DisplayName("STScI MAST Raw Images (MIRI)")]
    public bool IsToShowStsciRawMiri { get; set; } = true;

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

    [DisplayName("Mastodon")]
    public bool IsToShowMastodon { get; set; } = true;

    [DisplayName("Astrobin")]
    public bool IsToShowAstrobin { get; set; } = true;

    [DisplayName("Twitter")]
    public bool IsToShowTwitter { get; set; } = true;

    [DisplayName("Twitter Raw Photo Bot")]
    public bool IsToShowTwitterRawBot { get; set; } = true;

    [DisplayName("STScI MAST Raw Images & Data (NIRISS)")]
    public bool IsToShowStsciRawNiriss { get; set; } = true;

    [DisplayName("WebbTelescope.org")]
    public bool IsToShowWebbOrgImages { get; set; } = true;

    [DisplayName("STScI MAST Raw Images (NIRCam)")]
    public bool IsToShowStsciRawNircam { get; set; } = true;

    [DisplayName("STScI MAST Raw Data (NIRSpec)")]
    public bool IsToShowStsciRawNirspec { get; set; } = false;

    [DisplayName("Early Releases: CEERS, PDRs4All, GOALS, Giant Planet Systems, WR DustERS, Cosmic Spring, PEARLS, UNCOVER & upcoming releases")]
    public bool IsToShowEarlyReleases { get; set; } = true;

    [DisplayName("")]
    public bool IsToShowStsciRawFilteredOut { get; set; } = false;

    [DisplayName("Phys.org")]
    public bool IsToShowPhysOrg { get; set; } = true;

    [DisplayName("Space.com")]
    public bool IsToShowSpaceCom { get; set; } = true;

    [DisplayName("IFLScience")]
    public bool IsToShowIFLScience { get; set; } = true;

    public override string ToString()
        =>
        JsonConvert.SerializeObject(this);
}