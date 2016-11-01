﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Visual Studio via: 
//     Edit > Paste Special > Paste JSON as Classes
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inedo.BuildMasterExtensions.TFS.VisualStudioOnline.Model
{
    public class Sourceversiondisplayuri
    {
        public string href { get; set; }
    }

    public class Pool
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class _Links
    {
        public Href self { get; set; }
        public Href web { get; set; }
        public Href timeline { get; set; }
        public Href html { get; set; }
    }

    public class Href
    {
        public string href { get; set; }
    }

    public class Definition
    {
        public string type { get; set; }
        public int revision { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Project project { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
    }

    public class Project1
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
    }

    public class Queue
    {
        public object pool { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Requestedfor
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class Requestedby
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class Lastchangedby
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class Orchestrationplan
    {
        public string planId { get; set; }
    }

    public class Logs
    {
        public int id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Repository
    {
        public string id { get; set; }
        public string type { get; set; }
        public object clean { get; set; }
        public bool checkoutSubmodules { get; set; }
    }
}