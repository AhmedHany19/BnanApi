﻿using System;
using System.Collections.Generic;

namespace BnanApi.Models
{
    public partial class CrCasRenterContractAdditional
    {
        public string CrCasRenterContractAdditionalNo { get; set; } = null!;
        public string CrCasRenterContractAdditionalCode { get; set; } = null!;
        public decimal? CrCasContractAdditionalValue { get; set; }

        public virtual CrMasSupContractAdditional CrCasRenterContractAdditionalCodeNavigation { get; set; } = null!;
    }
}
