﻿using System;
using System.Collections.Generic;

namespace BnanApi.Models
{
    public partial class CrMasSupCarFuel
    {
        public CrMasSupCarFuel()
        {
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
        }

        public string CrMasSupCarFuelCode { get; set; } = null!;
        public string? CrMasSupCarFuelArName { get; set; }
        public string? CrMasSupCarFuelEnName { get; set; }
        public string? CrMasSupCarFuelImage { get; set; }
        public string? CrMasSupCarFuelStatus { get; set; }
        public string? CrMasSupCarFuelReasons { get; set; }

        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
    }
}
