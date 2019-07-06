using System;
using System.Runtime.CompilerServices;

namespace HelmClientWrapper
{
    public class ListCommand : HelmCommand<ListCommandOutput>
    {
        public string Filter { get; set; }
        [Flag]
        public bool All { get; set; }
        [Flag]
        public string ChartName { get; set; }
        [Flag]
        public uint ColWidth { get; set; } = 60;
        [Flag]
        public bool Date { get; set; }
        [Flag]
        public bool Deleted { get; set; }
        [Flag]
        public bool Deleting { get; set; }
        [Flag]
        public bool Deployed { get; set; }
        [Flag]
        public bool Failed { get; set; }
        [Flag]
        public int Max { get; set; } = 256;
        [Flag]
        public string Namespace { get; set; }
        [Flag]
        public string Offset { get; set; }
        [Flag]
        public bool Pending { get; set; }
        [Flag]
        public bool Reverse { get; set; }
        [Flag]
        public bool Short { get; set; }
        [Flag]
        public bool Tls { get; set; }
        [Flag]
        public string TlsCaCert { get; set; }
        [Flag]
        public string TlsCert { get; set; }
        [Flag]
        public string TlsKey { get; set; }
        [Flag]
        public bool TlsVerify { get; set; }

        public ListCommand()
            : base("list")
        {
        }

        public override string ToString() => $"{CommandName} {GetFlagsString()} {Filter}";
    }

    public class ListCommandOutput
    {
        public string Next { get; set; }
        public Release[] Releases { get; set; }
    }

    public class Release
    {
        public string Name { get; set; }
        public int Revision { get; set; }
        public string Updated { get; set; }
        public string Status { get; set; }
        public Version AppVersion { get; set; }
        public string Namespace { get; set; }
    }
}