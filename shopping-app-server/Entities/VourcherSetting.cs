using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
        public class VoucherSetting : BaseEntity
    {

        public string VoucherName { get; set; }
        public long VoucherLength { get; set; }
        public long VoucherNextNumber { get; set; }
        public string VoucherPreFix { get; set; }
        public long VoucherPreFixYear { get; set; }
        public long VoucherPreFixMonth { get; set; }
        public long VoucherPreFixDay { get; set; }
        public string VoucherPostFix { get; set; }
        public string VoucherSize { get; set; }
        public long VoucherMethod { get; set; }
        public long PrintonSave { get; set; }
        public long NoOfPrintCopy { get; set; }
        public string FormatView { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
