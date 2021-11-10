using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppSDKGallery.DataModel
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleInfoDataItem
    {
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string BadgeString { get; set; }
        public string Content { get; set; }
        public bool IsNew { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsPreview { get; set; }
        public Type PageType { get; set; }

        public bool IncludedInBuild { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    public class SampleInfoDocLink
    {
        public SampleInfoDocLink(string title, string uri)
        {
            this.Title = title;
            this.Uri = uri;
        }
        public string Title { get; private set; }
        public string Uri { get; private set; }
    }


    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleInfoDataGroup
    {
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<SampleInfoDataItem> Items { get; private set; } = new List<SampleInfoDataItem>();

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    ///
    /// ControlInfoSource initializes with data read from a static json file included in the
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class SampleInfoDataSource
    {
        public static IList<SampleInfoDataGroup> Groups { get; private set; } = new List<SampleInfoDataGroup>
        {
            new SampleInfoDataGroup
            {
                Title = "Manage app windows",
                Items =
                {
                    new SampleInfoDataItem
                    {
                        Title = "Basic operations",
                        PageType = typeof(SamplePages.AppWindowSamples.BasicAppWindowSamples)
                    },

                    new SampleInfoDataItem
                    {
                        Title = "Custom titlebar"
                    }
                }
            },

            new SampleInfoDataGroup
            {
                Title = "App lifecycle",
                Items =
                {
                    new SampleInfoDataItem
                    {
                        Title = "Get activation info",
                        PageType = typeof(SamplePages.AppLifecycle.ActivationPage)
                    },

                    new SampleInfoDataItem
                    {
                        Title = "Manage instances",
                        PageType = typeof(SamplePages.AppLifecycle.ManageInstancesPage)
                    },

                    new SampleInfoDataItem
                    {
                        Title = "Power management",
                        PageType = typeof(SamplePages.AppLifecycle.PowerManagerPage)
                    }
                }
            },

            new SampleInfoDataGroup
            {
                Title = "Media",
                Items =
                {
                    new SampleInfoDataItem
                    {
                        Title = "Play videos",
                        PageType = typeof(SamplePages.MediaSamples.PlayVideosPage)
                    }
                }
            }
        };
    }
}
